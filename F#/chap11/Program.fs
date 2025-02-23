open System
open System.Collections.Generic

printfn "Chapter 11"

type ResultBuilder() =
    member this.Return(x) = Ok x
    member this.Bind(x, f) = Result.bind f x

let result = ResultBuilder()

module C1103 =
    type MyInputType = Undefined
    type MyOutputType = Undefined
    type Workflow = MyInputType -> MyOutputType

    type JsonString = string

    type MyInputDto = Undefined
    type DeserializeInputDto = JsonString -> MyInputDto
    type InputDtoToDomain = MyInputDto -> MyInputType

    type MyOutputDto = Undefined
    type OutputDtoFromDomain = MyOutputType -> MyOutputDto
    type SerializeOutputDto = MyOutputDto -> JsonString

    let workflow _ = failwith "Not impl"
    let deserializeInputDto _ = failwith "Not impl"
    let inputDtoToDomain _ = failwith "Not impl"
    let outputDtoFromDomain _ = failwith "Not impl"
    let serializeOutputDto _ = failwith "Not impl"

    let workflowWithSerialization jsonString =
        jsonString
        |> deserializeInputDto
        |> inputDtoToDomain
        |> workflow
        |> outputDtoFromDomain
        |> serializeOutputDto

module C1104 =
    type String50 = String50 of string

    module String50 =
        let create str = Ok(String50 str)
        let value (String50 v) = v

    type Birthdate = Birthdate of DateTime

    module Birthdate =
        let create bd = Ok(Birthdate bd)
        let value (Birthdate v) = v

    module Domain =
        type Person =
            { First: String50
              Last: String50
              Birthdate: Birthdate }

    module Dto =
        type Person =
            { First: string
              Last: string
              Birthdate: DateTime }

        module Person =
            let fromDomain (person: Domain.Person) : Person =
                let first = person.First |> String50.value
                let last = person.Last |> String50.value
                let birthdate = person.Birthdate |> Birthdate.value

                { First = first
                  Last = last
                  Birthdate = birthdate }

            let toDomain (person: Person) : Result<Domain.Person, string> =
                result {
                    let! first = person.First |> String50.create
                    let! last = person.Last |> String50.create
                    let! birthdate = person.Birthdate |> Birthdate.create

                    return
                        { First = first
                          Last = last
                          Birthdate = birthdate }
                }

    module Json =
        open Newtonsoft.Json
        let serialize obj = JsonConvert.SerializeObject obj

        let deserialize<'a> str =
            try
                JsonConvert.DeserializeObject<'a> str |> Result.Ok
            with ex ->
                Result.Error ex

    let jsonFromDomain (person: Domain.Person) =
        person |> Dto.Person.fromDomain |> Json.serialize

    let person: Domain.Person =
        { First = String50 "Alex"
          Last = String50 "Adams"
          Birthdate = Birthdate(DateTime(1980, 1, 1)) }

    jsonFromDomain person |> printfn "%A"

    type DtoError =
        | ValidationError of string
        | DeserializationException of exn

    let jsonToDomain jsonString : Result<Domain.Person, DtoError> =
        result {
            let! deserializedValue = jsonString |> Json.deserialize |> Result.mapError DeserializationException

            let! domainValue = deserializedValue |> Dto.Person.toDomain |> Result.mapError ValidationError

            return domainValue
        }

    let jsonPerson =
        """{"First":"Alex","Last":"Adams","Birthdate":"1980-01-01T00:00:00"}"""

    jsonToDomain jsonPerson |> printfn "%A"

    let jsonPersonWithErrors =
        """{"First""Alex","Last":"Adams","Birthdate":"1980-01-01T00:00:00"}"""

    jsonToDomain jsonPersonWithErrors |> printfn "%A"

module C1105 =
    // C110501
    type ProductCode = ProductCode of string

    // C110503
    type OrderLineId = OrderLineId of int
    type OrderLineQty = OrderLineQty of int

    type OrderLine =
        { OrderLineId: OrderLineId
          ProductCode: ProductCode
          Quantity: OrderLineQty option
          Description: string option }

    type OrderLineDto =
        { OrderLineId: int
          ProductCode: string
          Quantity: Nullable<int>
          Description: string }

    // C110504
    type Order = { Lines: OrderLine list }

    type OrderDto = { Lines: OrderLineDto[] }

    type Price = Price of decimal
    type PriceLookup = Map<ProductCode, Price>

    type PriceLookupPair = { Key: string; Value: decimal }
    type PriceLookupDto = { KVPairs: PriceLookupPair[] }

    type PriceLookupDto' = { Keys: string[]; Values: string[] }

    // C110505
    type Color =
        | Red
        | Green
        | Blue

    type ColorDto =
        | Red = 1
        | Green = 2
        | Blue = 3

    let toDomain dto : Result<Color, _> =
        match dto with
        | ColorDto.Red -> Ok Color.Red
        | ColorDto.Green -> Ok Color.Green
        | ColorDto.Blue -> Ok Color.Blue
        | _ -> Error $"Color {dto} is not one of Red, Green, Blue"

    // C110506
    type Suit =
        | Heart
        | Spade
        | Diamond
        | Club

    type Rank =
        | Ace
        | Two
        | Queen
        | King

    type Card = Suit * Rank

    type SuitDto =
        | Heart = 1
        | Spade = 2
        | Diamond = 3
        | Club = 4

    type RankDto =
        | Ace = 1
        | Two = 2
        | Queen = 12
        | King = 13

    type CardDto = { Suit: SuitDto; Rank: RankDto }

    // C110507
    type String50 = String50 of string

    module String50 =
        let create str = Ok(String50 str)
        let value (String50 v) = v

    type Name = { First: String50; Last: String50 }

    type Example =
        | A
        | B of int
        | C of string list
        | D of Name

    type NameDto = { First: string; Last: string }

    type ExampleDto =
        { Tag: string
          BData: Nullable<int>
          CData: string[]
          DData: NameDto }

    let nameDtoFromDomain (name: Name) : NameDto =
        let first = name.First |> String50.value
        let last = name.Last |> String50.value
        { First = first; Last = last }

    let fromDomain (domainObj: Example) : ExampleDto =
        let nullBData = Nullable()
        let nullCdata = null
        let nullDData = Unchecked.defaultof<NameDto>

        match domainObj with
        | A ->
            { Tag = "A"
              BData = nullBData
              CData = nullCdata
              DData = nullDData }
        | B i ->
            let bData = Nullable i

            { Tag = "B"
              BData = bData
              CData = nullCdata
              DData = nullDData }
        | C strList ->
            let cData = strList |> List.toArray

            { Tag = "C"
              BData = nullBData
              CData = cData
              DData = nullDData }
        | D name ->
            let dData = name |> nameDtoFromDomain

            { Tag = "A"
              BData = nullBData
              CData = nullCdata
              DData = dData }

    let nameDtoToDomain (nameDto: NameDto) : Result<Name, string> =
        result {
            let! first = nameDto.First |> String50.create
            let! last = nameDto.Last |> String50.create
            return { First = first; Last = last }
        }

    let toDomain' dto : Result<Example, string> =
        match dto.Tag with
        | "A" -> Ok A
        | "B" ->
            if dto.BData.HasValue then
                dto.BData.Value |> B |> Ok
            else
                Error "B data not expected to be null"
        | "C" ->
            match dto.CData with
            | null -> Error "C data not expected to be null"
            | _ -> dto.CData |> Array.toList |> C |> Ok
        | "D" ->
            match box dto.DData with
            | null -> Error "D data not expected to be null"
            | _ -> dto.DData |> nameDtoToDomain |> Result.map D
        | _ ->
            let msg = $"Tag %s{dto.Tag} not recognized"
            Error msg

    // C110508
    let nameDtoFromDomain' (name: Name) : IDictionary<string, obj> =
        let first = name.First |> String50.value :> obj
        let last = name.Last |> String50.value :> obj
        [ ("First", first); ("Last", last) ] |> dict

    let fromDomain' (domainObj: Example) : IDictionary<string, obj> =
        match domainObj with
        | A -> [ "A", null ] |> dict
        | B i ->
            let bData = Nullable i :> obj
            [ ("B", bData) ] |> dict
        | C strList ->
            let cData = strList |> List.toArray :> obj
            [ ("C", cData) ] |> dict
        | D name ->
            let dData = name |> nameDtoFromDomain :> obj
            [ ("D", dData) ] |> dict

    let getValue key (dict: IDictionary<string, obj>) : Result<'a, string> =
        match dict.TryGetValue key with
        | true, value ->
            try
                (value :?> 'a) |> Ok
            with :? InvalidCastException ->
                let typeName = typeof<'a>.Name
                let msg = $"Value could not be cast to %s{typeName}"
                Error msg
        | false, _ ->
            let msg = $"Key '%s{key}' not found"
            Error msg

    let nameDtoToDomain' (nameDto: IDictionary<string, obj>) : Result<Name, string> =
        result {
            let! firstStr = nameDto |> getValue "First"
            let! first = firstStr |> String50.create
            let! lastStr = nameDto |> getValue "Last"
            let! last = lastStr |> String50.create
            return { First = first; Last = last }
        }

    let toDomain'' (dto: IDictionary<string, obj>) : Result<Example, string> =
        if dto.ContainsKey "A" then
            Ok A
        elif dto.ContainsKey "B" then
            result {
                let! bData = dto |> getValue "B"
                return B bData
            }
        elif dto.ContainsKey "C" then
            result {
                let! cData = dto |> getValue "C"
                return cData |> Array.toList |> C
            }
        elif dto.ContainsKey "D" then
            result {
                let! dData = dto |> getValue "D"
                let! name = dData |> nameDtoToDomain'
                return name |> D
            }
        else
            let msg = sprintf "No union case recognized"
            Error msg

    // C110509
    type ResultDto<'OkData, 'ErrorData when 'OkData: null and 'ErrorData: null> =
        { IsError: bool
          OkData: 'OkData
          ErrorData: 'ErrorData }

    type PlaceOrderEventDto = Undefined
    type PlaceOrderErrorDto = Undefined

    type PlaceOrderResultDto =
        { IsError: bool
          OkData: PlaceOrderEventDto[]
          ErrorData: PlaceOrderErrorDto }

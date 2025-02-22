open System

printfn "Chapter 11"

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

    type ResultBuilder() =
        member this.Return(x) = Ok x
        member this.Bind(x, f) = Result.bind f x

    let result = ResultBuilder()

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
    
    let jsonPersonWithErrors = """{"First""Alex","Last":"Adams","Birthdate":"1980-01-01T00:00:00"}"""
    jsonToDomain jsonPersonWithErrors |> printfn "%A"

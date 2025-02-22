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

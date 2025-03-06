open System
open System.Collections.Generic

printfn "Chapter 13"

type Undefined = Undefined of string
let undefined _ = failwith "Not Impl"

type ValidatedOrder = { ShippingAddress: ShippingAddress }
and ShippingAddress = { State: string; Country: string }

module C1301 =
    let calculateShippingCost validatedOrder =
        let shippingAddress = validatedOrder.ShippingAddress

        if shippingAddress.Country = "US" then
            match shippingAddress.State with
            | "CA"
            | "OR"
            | "AZ"
            | "NV" -> 5.0
            | _ -> 20.0
        else
            20.0

module C130101 =
    let (|UsLocalState|UsRemoteState|International|) address =
        if address.Country = "US" then
            match address.State with
            | "CA"
            | "OR"
            | "AZ"
            | "NV" -> UsLocalState
            | _ -> UsRemoteState
        else
            International

    let calculateShippingCost' validatedOrder =
        match validatedOrder.ShippingAddress with
        | UsLocalState -> 5.0
        | UsRemoteState -> 10.0
        | International -> 20.0

module C130102 =
    type OrderId = OrderId of string
    type Price = Undefined
    type PricedOrderProductLine = Undefined

    type PricedOrder =
        { OrderId: OrderId
          OrderTotal: Price
          ProductLine: PricedOrderProductLine list }

    type PricedOrderWithShippingInfo = Undefined

    type AddShippingInfoToOrder = PricedOrder -> PricedOrderWithShippingInfo

    type ShippingMethod =
        | PostalService
        | Fedex24
        | Fedex48
        | Ups48


    type ShippingInfo =
        { ShippingMethod: ShippingMethod
          ShippingCost: Price }

    type PricedOrderWithShippingMethod =
        { ShippingInfo: ShippingInfo
          PricedOrder: PricedOrder }

    type PricedOrder' =
        { OrderId: OrderId
          ShippingInfo: ShippingInfo
          ProductLine: PricedOrderProductLine list }

    type PricedOrder'' =
        { OrderId: OrderId
          ProductLine: PricedOderLine list }

    and PricedOderLine =
        | Product of PricedOrderProductLine
        | ShippingInfo of ShippingInfo

    type AddShippingInfoToOrder' = PricedOrder -> PricedOrder'

    let addShippingInfoToOrder calculateShippingCost : AddShippingInfoToOrder' =
        fun pricedOrder ->
            let shippingInfo =
                { ShippingMethod = undefined ()
                  ShippingCost = calculateShippingCost pricedOrder }

            { OrderId = pricedOrder.OrderId
              ShippingInfo = shippingInfo
              ProductLine = [] }

    let unvalidatedOrder _ = undefined ()
    let calculateShippingCost = undefined ()
    let validateOrder unvalidatedOrder = undefined ()
    let priceOrder validatedOrder = undefined ()
    let addShippingInfo = addShippingInfoToOrder calculateShippingCost

    let pricedOrder' =
        unvalidatedOrder |> validateOrder |> priceOrder |> addShippingInfo

module C1302 =
    type OrderId = OrderId of int
    type CustomerId = CustomerId of int
    type CustomerInfo = { CustomerId: CustomerId; IsVip: bool }

    type CustomerStatus =
        | Normal of CustomerInfo'
        | Vip of CustomerInfo'

    and CustomerInfo' = { CustomerId: CustomerId }

    type Order =
        { OrderId: OrderId
          CustomerStatus: CustomerStatus }

    type LoyaltyCardId = LoyaltyCardId of string

    type CustomerInfo'' =
        { CustomerId: CustomerId
          VipStatus: VipStatus
          LoyaltyCardStatus: LoyaltyCardStatus }

    and VipStatus =
        | Normal
        | Vip

    and LoyaltyCardStatus =
        | None
        | LoyaltyCard of LoyaltyCardId


type ResultBuilder() =
    member this.Return(x) = Ok x
    member this.Bind(x, f) = Result.bind f x

let result = ResultBuilder()

module C130201 =

    module Domain =
        type UnvalidatedCustomerInfo = { VipStatus: string }

    module Dto =
        type CustomerInfo = { VipStatus: string }

    type VipStatus =
        | Normal
        | Vip

    module VipStatus =
        let create _ = Vip

    type CustomerInfo = { VipStatus: VipStatus }

    let validateCustomerInfo (unvalidatedCustomerInfo: Domain.UnvalidatedCustomerInfo) =
        result {
            let vipStatus = VipStatus.create unvalidatedCustomerInfo.VipStatus

            let customerInfo: CustomerInfo = { VipStatus = vipStatus }
            return customerInfo
        }

module C130202 =
    type PricedOrderWithShippingMethod = Undefined

    type FreeVipShipping = PricedOrderWithShippingMethod -> PricedOrderWithShippingMethod

module C1303 =
    // C130301
    type PromotionCode = PromotionCode of string

    module PromotionCode =
        let value (PromotionCode v) = v

    type ValidatedOrder = { PromotionCode: PromotionCode option }
    type OrderDto = { PromotionCode: string }
    type UnvalidatedOrder = { PromotionCode: string }

    // C130302
    type ProductCode = ProductCode of string
    type Price = Price of decimal
    type GetProductPrice = ProductCode -> Price

    // option を渡すのは不自然
    // 言われてみれば確かに... あまり意識したことがなかった
    type GetPricingFunction = PromotionCode option -> GetProductPrice

    type PricingMethod =
        | Standard
        | Promotion of PromotionCode

    type ValidatedOrder' =
        { PricingMethod: PricingMethod
          OrderLines: ValidatedOrderLine list }

    and ValidatedOrderLine = { ProductCode: ProductCode }

    type GetPricingFunction' = PricingMethod -> GetProductPrice

    type PricedOrder = Undefined

    type PriceOrder =
        GetPricingFunction' // 依存
            -> ValidatedOrder' // 入力
            -> PricedOrder // 出力

    // C130303
    type GetStandardPriceTable = unit -> IDictionary<ProductCode, Price>
    type GetPromotionPriceTable = PromotionCode -> IDictionary<ProductCode, Price>

    let getPricingFunction
        (standardPrices: GetStandardPriceTable)
        (promoPrices: GetPromotionPriceTable)
        : GetPricingFunction' =

        let getStandardPrice: GetProductPrice =
            let standardPrice = standardPrices ()
            fun productCode -> standardPrice.[productCode]

        let getPromotionPrice promotionCode : GetProductPrice =
            let promotionPrice = promoPrices promotionCode

            fun productCode ->
                match promotionPrice.TryGetValue productCode with
                | true, price -> price
                | false, _ -> getStandardPrice productCode

        fun pricingMethod ->
            match pricingMethod with
            | Standard -> getStandardPrice
            | Promotion promotionCode -> getPromotionPrice promotionCode

    // C130304
    type CommentLine = CommentLine of string

    module CommentLine =
        let create str = CommentLine str

    type PriceOrderProductLine =
        { ProductCode: ProductCode
          Price: Price }

    type PricedOrderLine =
        | Product of PriceOrderProductLine
        | Comment of CommentLine

    let toPricedOrderLine orderLine = undefined ()

    type PricedOrder' = { OrderLines: PricedOrderLine list }

    type PriceOrder' =
        GetPricingFunction' // 依存
            -> ValidatedOrder' // 入力
            -> PricedOrder' // 出力

    let priceOrder: PriceOrder' =
        fun getPricingFunction validatedOrder ->
            let getProductPrice = getPricingFunction validatedOrder.PricingMethod

            let productOrderLines =
                validatedOrder.OrderLines |> List.map (toPricedOrderLine getProductPrice)

            let orderLines =
                match validatedOrder.PricingMethod with
                | Standard -> productOrderLines
                | Promotion promotion ->
                    let promoCode = promotion |> PromotionCode.value

                    let commentLine =
                        $"Applied promotion %s{promoCode}" |> CommentLine.create |> Comment

                    List.append productOrderLines [ commentLine ]

            { OrderLines = orderLines }

    // C130306
    type OrderId = OrderId of string
    type Address = Address of string

    type ShippableOrderLine =
        { ProductCode: ProductCode
          Quantity: float }

    type ShippableOrderPlaced =
        { OrderId: OrderId
          ShippingAddress: Address
          ShipmentLines: ShippableOrderLine list }

    type BillableOrderPlaced = Undefined

    type OrderAcknowledgmentSent = Undefined

    type PlaceOrderEvent =
        | ShippableOrderPlaced of ShippableOrderPlaced
        | BillableOrderPlaced of BillableOrderPlaced
        | AcknowledgmentSent of OrderAcknowledgmentSent

module C1304 =
    let isBusinessHour hour = 9 <= hour && hour <= 17

    let businessHoursOnly getHour onError onSuccess =
        let hour = getHour ()
        if isBusinessHour hour then onSuccess () else onError ()

    type ValidationError = Undefined

    type PlaceOrderError =
        | Validation of ValidationError
        | OutsideBusinessHours

    let placeOrder unvalidatedOrder = undefined ()

    let placeOrderInBusinessHours unvalidatedOrder =
        let onError () = Error OutsideBusinessHours
        let onSuccess () = placeOrder unvalidatedOrder
        let getHour () = DateTime.Now.Hour
        businessHoursOnly getHour onError onSuccess
namespace Chap0605

module C060501 =
    type Order =
        { OrderLines: OrderLine list
          AmountToBill: float }

    and OrderLine = { id: int; Price: float }

    let findOrderLine orderLineId lines =
        lines |> List.find (fun ol -> ol.id = orderLineId)

    let replaceOrderLine orderLineId newOrderLine lines =
        lines |> List.map (fun ol -> if ol.id = orderLineId then newOrderLine else ol)


    let changeOrderLinePrice order orderLineId newPrice =
        let orderLine = order.OrderLines |> findOrderLine orderLineId
        let newOrderLine = { orderLine with Price = newPrice }
        let newOrderLines = order.OrderLines |> replaceOrderLine orderLineId newOrderLine
        let newAmountToBill = newOrderLines |> List.sumBy (fun line -> line.Price)

        { order with
            OrderLines = newOrderLines
            AmountToBill = newAmountToBill }

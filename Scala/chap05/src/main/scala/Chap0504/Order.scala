package Chap0504


case class Order(
                  customerInfo: CustomerInfo,
                  shippingAddress: ShippingAddress,
                  billingAddress: BillingAddress,
                  orderLines: List[OrderLine],
                  amountToBill: BillingAmount,
                )

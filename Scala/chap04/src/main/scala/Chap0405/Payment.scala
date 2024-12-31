package Chap0405

type PaymentAmount = BigDecimal

case class Payment(
                    amount: PaymentAmount,
                    currency: Currency,
                    method: PaymentMethod,
                  )

package Chap0405

import Chap0405.Payment.PaymentAmount

case class Payment(
                    amount: PaymentAmount,
                    currency: Currency,
                    method: PaymentMethod,
                  )

object Payment:
  opaque type PaymentAmount = BigDecimal

  object PaymentAmount:
    def apply(value: BigDecimal): PaymentAmount = value

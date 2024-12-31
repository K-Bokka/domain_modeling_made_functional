package Chap0405

enum PaymentMethod:
  case Cash
  case CheckNumber(value: Int)
  case CardNumber(value: String)

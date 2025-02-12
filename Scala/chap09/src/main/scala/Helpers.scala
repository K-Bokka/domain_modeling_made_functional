import Domains.*
import Utils.Pipe.*
import Utils.*
import Workflows.*

object Helpers:

  def toCustomerInfo(customer: UnvalidatedCustomerInfo): CustomerInfo =
    val firstName = customer.firstName |> String50.apply
    val lastName = customer.lastName |> String50.apply
    val emailAddress = customer.emailAddress |> EmailAddress.apply
    val name = PersonalName(firstName, lastName)
    CustomerInfo(name, emailAddress)

  def toAddress(checkAddressExists: CheckAddressExists, unvalidatedAddress: UnvalidatedAddress): Address =
    val checkedAddress: CheckedAddress = unvalidatedAddress |> checkAddressExists
    val addressLine1 = checkedAddress.addressLine1 |> String50.apply
    val addressLine2 = checkedAddress.addressLine2 |> String50.applyOpt
    val addressLine3 = checkedAddress.addressLine3 |> String50.applyOpt
    val addressLine4 = checkedAddress.addressLine4 |> String50.applyOpt
    val city = checkedAddress.city |> String50.apply
    val zipCode = checkedAddress.zipCode |> ZipCode.apply
    Address(addressLine1, addressLine2, addressLine3, addressLine4, city, zipCode)

  def convertToPassthru(checkProductCodeExists: CheckProductCodeExists, productCode: ProductCode): ProductCode =
    if checkProductCodeExists(productCode) then productCode
    else throw new Exception(s"Invalid product code: ${productCode}")

  def toProductCode(checkProductCodeExists: CheckProductCodeExists, productCode: String): ProductCode =
    val checkProduct = productCode => convertToPassthru(checkProductCodeExists, productCode)
    productCode |> ProductCode.apply |> checkProduct

  def toOrderQuantity(productCode: ProductCode, quantity: BigDecimal): OrderQuantity =
    productCode match
      case ProductCode.Widget(_) => quantity.toInt |> UnitQuantity.apply |> OrderQuantity.Unit.apply
      case ProductCode.Gizmo(_) => quantity |> KilogramQuantity.apply |> OrderQuantity.Kilos.apply

  def toOrderLine(
                   checkProductCodeExists: CheckProductCodeExists,
                   unvalidatedOrderLine: UnvalidatedOrderLine,
                 ): OrderLine =
    val toProductCodeCurried = toProductCode(checkProductCodeExists, _: String)
    val orderLineId = unvalidatedOrderLine.orderLineId |> OrderLineId.apply
    val productCode = unvalidatedOrderLine.productCode |> toProductCodeCurried
    val toOrderQuantityCurried = toOrderQuantity(productCode, _: BigDecimal)
    val quantity = unvalidatedOrderLine.quantity |> toOrderQuantityCurried
    OrderLine(orderLineId, productCode, quantity)

  def toPricedOrderLine(
                         getProductPrice: GetProductPrice,
                         validatedOrderLine: OrderLine,
                       ): PricedOrderLine =
    val qty = validatedOrderLine.quantity |> OrderQuantity.value
    val price = validatedOrderLine.productId |> getProductPrice
    val priceMultiplyCurried = Price.multiply(qty, _: Price)
    val linePrice = price |> priceMultiplyCurried
    PricedOrderLine(
      validatedOrderLine.orderLineId,
      validatedOrderLine.productId,
      validatedOrderLine.quantity,
      price,
    )

  def createBillingEvent(pricedOrder: PricedOrder): Option[BillableOrderPlaced] =
    val billingAmount = pricedOrder.amountToBill.value
    if billingAmount > 0D then
      Some(BillableOrderPlaced(
        orderId = pricedOrder.orderId,
        billingAddress = pricedOrder.billingAddress,
        amountToBill = pricedOrder.amountToBill,
      ))
    else
      None

end Helpers

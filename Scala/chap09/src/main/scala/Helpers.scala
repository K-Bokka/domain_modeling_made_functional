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
    val checkedAddress: CheckedAddress = checkAddressExists(unvalidatedAddress)
    val addressLine1 = checkedAddress.addressLine1 |> String50.apply
    val addressLine2 = checkedAddress.addressLine2 |> String50.applyOpt
    val addressLine3 = checkedAddress.addressLine3 |> String50.applyOpt
    val addressLine4 = checkedAddress.addressLine4 |> String50.applyOpt
    val city = checkedAddress.city |> String50.apply
    val zipCode = checkedAddress.zipCode |> ZipCode.apply
    Address(addressLine1, addressLine2, addressLine3, addressLine4, city, zipCode)

  def toOrderLine(unvalidatedOrderLine: UnvalidatedOrderLine): OrderLine = ???

end Helpers

import Domains._
import Utils.Pipe._
import Utils._

object Helpers:

  def toCustomerInfo(customer: UnvalidatedCustomerInfo): CustomerInfo =
    val firstName = customer.firstName |> String50.apply
    val lastName = customer.lastName |> String50.apply
    val emailAddress = customer.emailAddress |> EmailAddress.apply
    val name = PersonalName(firstName, lastName)
    CustomerInfo(name, emailAddress)

  def toAddress(UnvalidatedAddress: UnvalidatedAddress): CheckedAddress = ???

  def toOrderLine(unvalidatedOrderLine: UnvalidatedOrderLine): OrderLine = ???

end Helpers
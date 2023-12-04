using CurrentAccount.Core.Shared;

namespace CurrentAccount.Core.Customer
{
    public class IndividualCustomerEntity : CustomerEntity
    {
        public IndividualCustomerEntity(Guid customerId, NameValue firstName, NameValue lastName) : base(customerId)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public NameValue FirstName { get; init; }
        public NameValue LastName { get; init; }
    }
}
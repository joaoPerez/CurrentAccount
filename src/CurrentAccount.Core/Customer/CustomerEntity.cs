namespace CurrentAccount.Core.Customer
{
    public class CustomerEntity
    {
        public CustomerEntity(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; private set; }

        public void SetCustomerId(Guid customerId) { CustomerId = customerId; }
    }
}

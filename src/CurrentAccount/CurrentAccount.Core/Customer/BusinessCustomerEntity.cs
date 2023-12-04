using CurrentAccount.Core.Shared;

namespace CurrentAccount.Core.Customer
{
    public class BusinessCustomerEntity : CustomerEntity
    {
        public BusinessCustomerEntity(Guid customerId, NameValue businessName, BusinessTypeValue businessType) : base(customerId)
        {
            BusinessName = businessName;
            BusinessType = businessType;
        } 

        public NameValue BusinessName { get; private set; }
        public BusinessTypeValue BusinessType { get; private set; } // Would be nice to have an array with Business types coming from the database and stored in memory.
    }
}
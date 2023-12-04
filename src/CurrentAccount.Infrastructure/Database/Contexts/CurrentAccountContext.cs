using CurrentAccount.Core.CurrentAccount;
using CurrentAccount.Infrastructure.Database.Models;
using CurrentAccount.Infrastructure.Database.Models.CurrentAccount;
using CurrentAccount.Infrastructure.Database.Models.Customer;
using Microsoft.EntityFrameworkCore;

namespace CurrentAccount.Infrastructure.Database.Context
{
	public class CurrentAccountContext : DbContext
	{

        public CurrentAccountContext(DbContextOptions<CurrentAccountContext> options)
		: base(options)
		{
		}

		public DbSet<CurrentAccountDataModel> CurrentAccounts { get; set; } = null;
		public DbSet<CustomerDataModel> Customers { get; set; } = null;
		public DbSet<AccountHolderAddressDataModel> AccountHolderAddresses { get; set; } = null;
		public DbSet<ContactInformationDataModel> ContactInformations { get; set; } = null;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var customerId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
			var contactInfoId = Guid.NewGuid();
			var accountHolderAddressId = Guid.NewGuid();

			modelBuilder.Entity<AccountHolderAddressDataModel>().HasData(
				new AccountHolderAddressDataModel
				{
					Id = accountHolderAddressId,
					City = "test city",
					Country = "test country",
					State = "test state",
					Street = "street test",
					ZipCode = "111111"
				});

			modelBuilder.Entity<ContactInformationDataModel>().HasData(
				new ContactInformationDataModel
				{
					Id = contactInfoId,
					Email = "test@email.com",
					PhoneNumber = "555555555",
					CountryCode = "55"
				});

			modelBuilder.Entity<IndividualCustomerDataModel>().HasData(
				new IndividualCustomerDataModel
				{
					CustomerId = customerId,
					FirstName = "first name",
					LastName = "last name",
				});

			modelBuilder.Entity<CurrentAccountDataModel>().HasData(
				new CurrentAccountDataModel
				{
					Id = Guid.NewGuid(),
					CustomerId = customerId,
					AccountDigit = 0,
					AccountHolderAddressId = accountHolderAddressId,
					AccountNumber = "0000000001",
					AccountType = Enum.GetName(AccountTypeEnum.Individual),
					IsActive = true,
					Balance = 0,
					ContactInfoId = contactInfoId,
					CreationDate = DateTime.Now,
					Currency = "USD"
				}
			);
		}

		/*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "CurrentAccountInMemoryDB");
		} */
	}
}

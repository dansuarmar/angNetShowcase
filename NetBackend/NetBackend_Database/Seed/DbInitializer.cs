using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetBackend_Database.Model;

namespace NetBackend_Database.Seed
{
    public class DbInitializer(AppDbContext appDbContext, ILogger<DbInitializer> logger)
    {
        readonly AppDbContext appDbContext = appDbContext;

        public void Seed()
        {
            try
            {
                try
                { // In case of Mac Migrations don't apply, so I'm simply skipping the Exception.
                    if (appDbContext.Database.GetPendingMigrations().Any())
                        appDbContext.Database.Migrate();
                } catch {}

                SeedFirstCustomers();
                SeedMultipleCustomersIfNotEnoughExists(100);
                appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        private void SeedFirstCustomers()
        {
            var customerId = new Guid("c3aa9321-79a3-41ef-81b4-a99536a7a6bc");
            var someCustomer = appDbContext.Customers.FirstOrDefault(m => m.Id == customerId);

            if (someCustomer == null)
            {
                var customer = new Customer()
                {
                    Id = customerId,
                    FirstName = "Joe",
                    LastName = "Doe",
                    Email = "email@customer.com",
                    Phone = "(555) 555-5555",
                    Description = "Description about this customer needs.",
                    Created = DateTime.Now,
                };
                appDbContext.Customers.Add(customer);
            }
        }

        private void SeedMultipleCustomersIfNotEnoughExists(int number)
        {
            var customersNumber = appDbContext.Customers.Count();
            int pending = number - customersNumber;
            if (pending > 0)
            {
                var faker = new Faker<Customer>().
                    RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName()).
                    RuleFor(u => u.LastName, (f, u) => f.Name.LastName()).
                    RuleFor(u => u.Phone, (f, u) => f.Phone.PhoneNumber()).
                    RuleFor(u => u.Email, (f, u) => f.Internet.Email()).
                    RuleFor(u => u.Description, (f, u) => f.Rant.Random.Words()).
                    RuleFor(u => u.Created, (f, u) => f.Date.PastOffset());

                var customersToAdd = new List<Customer>();
                for (int i = 0; i < pending; i++)
                {
                    customersToAdd.Add(faker.Generate());
                }
                appDbContext.AddRange(customersToAdd);
            }
        }
    }
}

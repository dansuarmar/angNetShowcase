﻿using Microsoft.EntityFrameworkCore;
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
                if(appDbContext.Database.GetPendingMigrations().Any()) 
                    appDbContext.Database.Migrate();

                SeedCustomers();
                appDbContext.SaveChanges();
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
            }
        }

        private void SeedCustomers()
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
    }
}
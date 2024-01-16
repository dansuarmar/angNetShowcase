using Bogus;
using NetBackend_Application.CustomerApp;
using NetBackend_Database;
using NetBackend_Database.Model;

namespace NetBackend_UnitTests;

public class GetAllQueryHandler_Tests
{
    private AppDbContext context = TestContextGenerator.Generate();
    public GetAllQueryHandler_Tests()
    {   
        context.Database.EnsureDeleted();
        var faker = new Faker<Customer>().
            RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName()).
            RuleFor(u => u.LastName, (f, u) => f.Name.LastName()).
            RuleFor(u => u.Phone, (f, u) => f.Phone.PhoneNumber()).
            RuleFor(u => u.Email, (f, u) => f.Internet.Email()).
            RuleFor(u => u.Description, (f, u) => f.Rant.Random.Words()).
            RuleFor(u => u.Created, (f, u) => f.Date.PastOffset()); 
            
        for(var n = 0; n < 16; n++)
        {
            context.Add(faker.Generate());
        }
        context.SaveChanges();
    }

    [Fact]
    public async void Handle_ShouldReturnAllCustomers()
    {
        var query = new GetAllCustomersQuery();
        var sut = new GetAllCustomersQueryHandler(context, new AppMapper());
        var result = await sut.Handle(query, new CancellationToken());

        Assert.Equal(context.Customers.Count(), result.Total);
    }

    [Fact]
    public async void HandlePaging_ShouldReturnFirstPage()
    {
        var query = new GetAllCustomersQuery()
        {
            Page = 1,
            Size = 4,
        };
        var sut = new GetAllCustomersQueryHandler(context, new AppMapper());
        var result = await sut.Handle(query, new CancellationToken());

        Assert.Equal(query.Size, result.Items.Count);
        Assert.True(result.HasNext);
        Assert.False(result.HasPrev);
    }
}

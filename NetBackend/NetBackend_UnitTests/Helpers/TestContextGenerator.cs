using Microsoft.EntityFrameworkCore;
using NetBackend_Database;

namespace NetBackend_UnitTests;

public static class TestContextGenerator
{
    public static AppDbContext Generate()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestDb");
        var context = new AppDbContext(optionsBuilder.Options);
        return context;
    }
}

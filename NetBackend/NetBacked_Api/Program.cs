using Microsoft.EntityFrameworkCore;
using NetBackend_Database;
using NetBackend_Database.Seed;

namespace NetBacked_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            //Add SQL and AppDbContext
            builder.Services.AddDbContext<AppDbContext>(option => 
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("NetBackend_Database"))
            );
            builder.Services.AddScoped<DbInitializer, DbInitializer>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope()) 
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
                dbInitializer.Seed();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

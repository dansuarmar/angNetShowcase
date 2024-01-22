using Microsoft.EntityFrameworkCore;
using NetBackend_Api.Controllers;
using NetBackend_Application.CustomerApp;
using NetBackend_Database;
using NetBackend_Database.Seed;
using System.Reflection;

[assembly: AssemblyVersion("1.0.*")]
namespace NetBackend_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("LocalPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            
            builder.Services.AddControllers().AddApplicationPart(typeof(VersionController).Assembly);


            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                //Use InMemoryDB when Running on Mac
                builder.Services.AddDbContext<AppDbContext>(option =>
                    option.UseInMemoryDatabase("MemoryDB"));
            }
            else 
            {
                //Add SQL and AppDbContext
                builder.Services.AddDbContext<AppDbContext>(option =>
                    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("NetBackend_Database"))
                );
            }



            builder.Services.AddScoped<DbInitializer, DbInitializer>();

            builder.Services.AddSingleton<AppMapper, AppMapper>();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllCustomersQuery).Assembly));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseCors("LocalPolicy");

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

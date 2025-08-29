using OPS.InventoryService.Repositories.Concrete;
using OPS.InventoryService.Source.Repositories;
using OPS.Shared;
using OPS.Shared.Exceptions.Handlers;

namespace OPS.InventoryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            IServiceCollection services = builder.Services;

            // Add services to the container.
            services.AddControllers();
        
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Configure MongoDB
            services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
        
            // Register services
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            
            // Exception handling
            services.AddProblemDetails();
            services.AddExceptionHandler<MongoExceptionHandler>();
            services.AddExceptionHandler<FallbackExceptionHandler>();
            
            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler();
            
            app.UseHttpsRedirection();
            app.UseAuthorization();
        
            app.MapControllers();

            app.Run();
        }
    }
}

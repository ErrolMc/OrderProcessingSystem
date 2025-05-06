using OPS.UserService.Repositories;
using OPS.UserService.Repositories.Concrete;
using OPS.UserService.Services;
using OPS.UserService.Services.Concrete;

namespace OPS.UserService
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
        
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IAuthService, AuthService>();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
        
            app.MapControllers();

            app.Run();
        }
    }
}
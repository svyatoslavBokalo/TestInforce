
using Backend.Data;
using Backend.Models;
using Backend.RepositoryFolder;
using Backend.Services.Interfaces;
using BackendServices.Realization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Serialization;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<AppUser>()
                .AddEntityFrameworkStores<AppDbContext>();


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));
            builder.Services.AddScoped<IUrlService, UrlService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
                options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            var app = builder.Build();
            app.MapIdentityApi<AppUser>();
            app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

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

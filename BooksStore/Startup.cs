using AutoMapper;
using BooksStore.Automapper;
using BooksStore.DAL;
using BooksStore.ServiceExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

namespace BooksStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BooksStore",
                    Description = "DB of books and authors.",
                    Contact=new OpenApiContact 
                    {
                        Name="Kseniya Rohaleva",
                        Email="kseniya.rohaleva@innowise-group.com",
                        Url= new Uri("https://www.linkedin.com/in/kseniya-rogaleva-555b7b1a7/"),
                    }
                });
            });

            var connectionString = Configuration.GetConnectionString("PostgreSqlConnectionString");
            services.AddDbContext<PostgreSqlContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddServices();

        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
            });
        }
    }
}

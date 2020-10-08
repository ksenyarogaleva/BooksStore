using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace BooksStore.Helpers
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach(var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Version = description.ApiVersion.ToString(),
                    Title = $"BooksStore {description.ApiVersion}",
                    Description = $"DB of books and authors.",
                    Contact = new OpenApiContact
                    {
                        Name = "Kseniya Rohaleva",
                        Email = "kseniya.rohaleva@innowise-group.com",
                        Url = new Uri("https://www.linkedin.com/in/kseniya-rogaleva-555b7b1a7/"),
                    }
                });
            }
        }
    }
}

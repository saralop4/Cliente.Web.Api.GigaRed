using Clente.Web.Api.Modules.Authentication;
using Cliente.Web.Api.Modules.Injection;
using Cliente.Web.Api.Modules.Swagger;
using Cliente.Web.Api.Modules.Validator;
using Cliente.Web.Api.Modules.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Cliente.Web.Api;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddVersioning();
        builder.Services.AddAuthentication(builder.Configuration);
        builder.Services.AddAuthorization();
        builder.Services.AddValidator();
        builder.Services.AddInjection(builder.Configuration);
        builder.Services.AddSwaggerDocumentation();

        builder.Services.AddCors(option =>
        {
            option.AddPolicy("policyApi", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });


        var app = builder.Build();

        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }



        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();  
        app.UseCors("policyApi");
        app.MapControllers();

        app.Run();
    }
}
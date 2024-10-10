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

        //builder.Services.AddControllers().AddNewtonsoftJson(options =>
        //{
        //    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
        //});



        builder.Services.AddVersioning();
     //   builder.Services.AddAuthentication(builder.Configuration);
        //  builder.Services.AddMapper();
        //   builder.Services.AddFeature(builder.Configuration);
        builder.Services.AddValidator();
        //  builder.Services.AddHealthCheck(builder.Configuration);
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
      //  app.UseAuthentication();

        app.UseCors("policyApi");
     //   app.UseAuthorization();
        app.MapControllers();

        /*    app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        */
        app.Run();
    }
}
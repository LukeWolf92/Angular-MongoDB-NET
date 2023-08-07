using Microsoft.OpenApi.Models;
using RestfulApi.Data;
using RestfulApi.Services;

namespace RestfulApi;
public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;
    private const string AllowAllPolicy = "AllowAll";
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddPolicy(AllowAllPolicy,
            builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

        services.AddControllers();
        //services.AddControllers().AddJsonOptions(options =>
        //{
        //    options.JsonSerializerOptions.Converters.Add(new StringConverter());
        //    options.JsonSerializerOptions.Converters.Add(new DateConverter());
        //    options.JsonSerializerOptions.Converters.Add(new DateNullableConverter());
        //});
        services.AddAuthentication();

        services.AddHttpContextAccessor();

        services.AddSingleton<MongoDBContext>();
        services.AddScoped<ICustomer, CustomerService>();

        // Register the Swagger generator
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "RESTful API",
                Description = "ASP.NET Core Web API",
            });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "CloudWebApiSwagger v1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                //c.EnableFilter();
                //c.MaxDisplayedTags(10);
            });
        }
        else if (env.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "CloudWebApiSwagger v1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                //c.EnableFilter();
                //c.MaxDisplayedTags(10);
            });
        }
        else
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "CloudWebApiSwagger v1"));
        }
        app.UseCors(AllowAllPolicy);
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
using System.Reflection;
using System.Text.Json.Serialization;
using eShop_DHA.Data;
using eShop_DHA.Helpers;
using eShop_DHA.Salesforce;
using eShop_DHA.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
// Add services to the container.
var isDevelopmentEnviroment = builder.Environment.IsDevelopment();
var connectionString = isDevelopmentEnviroment
    ? builder.Configuration.GetConnectionString("DefaultConnection")
    : HerokuHelper.GetConnectionString();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option
        .UseNpgsql(connectionString);
    
});
builder.Services.AddControllers();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<IAuthenProvider, AuthenProvider>();
// builder.Services.AddControllers().AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//     // added this
//     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
// });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Supermarket API",
        Version = "v1",
        Description = "Simple RESTful API built with ASP.NET Core 6",
        Contact = new OpenApiContact
        {
            Name = "Tuan Le",
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
        },
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
if (env.IsDevelopment())
{
    
}
else
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(Int32.Parse(Environment.GetEnvironmentVariable("PORT")));
    });
}
var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "eShop-DHA API V1");
    c.RoutePrefix = string.Empty;
    c.DocumentTitle = "eShop API";
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
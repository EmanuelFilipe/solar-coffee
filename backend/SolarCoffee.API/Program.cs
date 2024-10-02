using Microsoft.EntityFrameworkCore;
using SolarCoffee.Infrastructure.Persistence;
using SolarCoffee.Services.Product;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// configurando postgresql
builder.Services.AddDbContext<SolarDbContext>(opts =>
{
    opts.EnableDetailedErrors();
    opts.UseNpgsql(builder.Configuration.GetConnectionString("solar.dev"));
});

builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

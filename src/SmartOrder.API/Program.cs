
using Microsoft.EntityFrameworkCore;
using SmartOrder.Application.Services;
using SmartOrder.Domain.Repositories;
using SmartOrder.Infrastructure.Data;
using SmartOrder.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// EF Core
builder.Services.AddDbContext<SmartOrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Application Services
builder.Services.AddScoped<OrderServices>();
builder.Services.AddScoped<ProductService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

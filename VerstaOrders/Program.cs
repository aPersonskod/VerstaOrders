using Microsoft.EntityFrameworkCore;
using VerstaOrders;
using VerstaOrders.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => 
    o.AddPolicy("CorsPolicy", b =>
    {
        b.AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowCredentials();
    }));

builder.Services.AddTransient<IOrderService, OrderService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionDev")));
}
else
{
    builder.Services.AddDbContext<DataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
}

var app = builder.Build();
app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
using Forecasting.Data;
using Forecasting.Repositories;
using Microsoft.EntityFrameworkCore;
using Forecasting.Sales;
using Forecasting.Predictions;
using Forecasting.Goals.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<SalesRepository>();
builder.Services.AddScoped<ProductsRepository>();
builder.Services.AddScoped<SalesService>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<GoalRepository>();
builder.Services.AddScoped<GoalsService>();
builder.Services.AddHttpClient<PredictionClient>( client =>
{
    client.BaseAddress = new Uri("http://127.0.0.1:8000");
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowDevCors",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

//We can remove this line while we use this as a container only accesed 
//throug nginx proxy.
//app.UseHttpsRedirection();

app.UseCors("AllowDevCors");

app.UseAuthorization();

app.MapControllers();

app.Run();

using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
using var scope = app.Services.CreateScope();
var logger = scope.ServiceProvider.GetService<ILogger<Program>>();

try
{

    var context = scope.ServiceProvider.GetService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);

}
catch (Exception ex)
{
    
    logger.LogError(ex, "An error occured during migrations");
}


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

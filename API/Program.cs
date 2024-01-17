using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using API.Middleware;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Extension
builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
app.UseStatusCodePagesWithReExecute("errors/{0}");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();


app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
var context=services.GetRequiredService<StoreContext>();
var logger=services.GetRequiredService<ILogger<Program>>();

try 
{
 await context.Database.MigrateAsync();
 await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
logger.LogError(ex,"An error occured during migration");
}
app.Run();

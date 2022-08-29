using Abhiroop.Domain.Entities;
using Abhiroop.Domain.Helper;
using Abhiroop.Domain.SeedingData;
using Abhiroop.Web;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

new DIManager(builder.Services, builder.Configuration);

var app = builder.Build();

try
{
    await AddDefaultUser(app);
}
catch
{
    throw;
}
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.

app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            context.Response.AddApplicationError(error.Error.Message);
            await context.Response.WriteAsync(error.Error.Message);
        }
    });
});
app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task AddDefaultUser(WebApplication app)
{
    var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    await DefaultUserSeed.SeedRoleAsync(roleManager);
    await DefaultUserSeed.SeedAdminUserAsync(userManager, roleManager);
}
using Microsoft.Extensions.Configuration;
using Salary.Models.DBContext;
using System;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using Salary.Models.Data;


var sqlStringDB = "";

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

IWebHostEnvironment environment = builder.Environment;

if (environment.IsDevelopment())
{
    configuration.AddJsonFile("appsettings.Development.json")
    .AddEnvironmentVariables()
    .Build();
    
    sqlStringDB = configuration.GetConnectionString("AppDBSalary");

}



// Add services to the container.
builder.Services.AddControllersWithViews();

//add to connection to database

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(sqlStringDB));
builder.Services.AddScoped<IAssociateData, AssociateData>();
builder.Services.AddScoped<IDepartmentData, DepartmentData>();
builder.Services.AddScoped<IIncreaseData, IncreaseData>();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// verify db connection
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<AppDBContext>();
        var isConnected = dbContext.Database.CanConnect();
        if (isConnected)
        {
            Console.WriteLine("DB CONNECT");
        }
        else
        {
            Console.WriteLine("DB NOT CONNECT");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error Connect: "+ ex);
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

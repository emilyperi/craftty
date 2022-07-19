using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Craftty.Data;
using Craftty.WebSite.Services;
using System.Text.Json;
using Craftty.WebSite.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddTransient<JsonFileProductService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddControllers();

builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();
//app.MapGet("/products", (context) =>
//    {
//        var products = app.Services.GetService<JsonFileProductService>().GetProducts();
//        var json = JsonSerializer.Serialize<IEnumerable<Product>>(products);
//        return context.Response.WriteAsync(json);
//    });

app.Run();


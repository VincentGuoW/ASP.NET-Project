using Microsoft.EntityFrameworkCore;
using ProjectWG.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//*******************************************************************************
//Add Session =>
builder.Services.AddDistributedMemoryCache(); // Required to use session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(3600); // For example, 1 hour
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//*******************************************************************************



var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<UsedVehicleContext>(item =>
item.UseSqlServer(configuration.GetConnectionString("SalesContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//*******************************************************************************
//Use Session =>
app.UseSession();
//*******************************************************************************


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

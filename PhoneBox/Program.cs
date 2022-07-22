
using PhoneBox;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneBox.Context;
using PhoneBox.Entities.Identity;
using PhoneBox.ExceptionHandling;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PhoneBoxContextConnection") ?? throw new InvalidOperationException("Connection string 'PhoneBoxContextConnection' not found.");

//Identity and Database
builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

//Add Serilog
Log.Logger = new LoggerConfiguration().CreateLogger();
builder.Host.UseSerilog(((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .WriteTo.Console()));


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddPhoneBoxServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseExceptionHandlerMiddleware();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

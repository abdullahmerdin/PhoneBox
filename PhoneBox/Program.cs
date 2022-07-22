<<<<<<< HEAD
using PhoneBox;
=======
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneBox.Areas.Identity.Data;
using PhoneBox.Data;
using PhoneBox.ExceptionHandling;
using Serilog;
>>>>>>> 827645c862f177cf1192ef10adf1fc97635872bc

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PhoneBoxContextConnection") ?? throw new InvalidOperationException("Connection string 'PhoneBoxContextConnection' not found.");

//Identity and Database
builder.Services.AddDbContext<PhoneBoxContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<PhoneBoxUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PhoneBoxContext>();

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
    app.UseExceptionHandlerMiddleware();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

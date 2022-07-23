
using System.Globalization;
using PhoneBox;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using PhoneBox.Context;
using PhoneBox.Entities.Identity;
using PhoneBox.ExceptionHandling;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Identity and Database
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddIdentity<AppUser, AppRole>(x =>
    {
        x.Password.RequireUppercase = false;
        x.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AppDbContext>();

//Add Serilog
Log.Logger = new LoggerConfiguration().CreateLogger();
builder.Host.UseSerilog(((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .WriteTo.Console()));


builder.Services.AddPhoneBoxServices();

//Add Localization
builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(opt =>
    {
        var supportedCultures = new[]
        {
            new CultureInfo("tr"),
            new CultureInfo("en")
        };
        opt.DefaultRequestCulture = new RequestCulture("tr");
        opt.SupportedCultures = supportedCultures;
        opt.SupportedUICultures = supportedCultures;
    });


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandlerMiddleware();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();;

app.UseAuthorization();

app.UseRequestLocalization(((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PhoneNumbers}/{action=GetAll}/{id?}");

app.Run();

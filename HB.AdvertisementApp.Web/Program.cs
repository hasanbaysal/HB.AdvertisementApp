using FluentValidation;
using HB.AdvertisementApp.Business.DependencyResolvers.Microsoft;
using HB.AdvertisementApp.Web.Models;
using HB.AdvertisementApp.Web.ValidationRules;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddBusinessDependencies(a =>
{
    a.ConnectionString = builder.Configuration.GetConnectionString("SqlCon")!;
},Assembly.GetExecutingAssembly());


builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));


builder.Services.AddTransient<IValidator<UserCreateModel>, UserCreateMoldelValidator>();



//custom cookie Auth

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.Cookie.Name = "UygulamaCookie";
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.SecurePolicy= CookieSecurePolicy.SameAsRequest;
        opt.ExpireTimeSpan = TimeSpan.FromDays(20);
        opt.LoginPath = new PathString("/Account/SignIn");
        opt.LogoutPath= new PathString("/Account/LogOut");
        opt.AccessDeniedPath= new PathString("/Account/AccesDenied");


    });

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

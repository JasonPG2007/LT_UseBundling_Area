using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options=>
{
    options.LoginPath = "/Admin/Login";
    options.LogoutPath = "/Admin/Logout";
    options.AccessDeniedPath = "/Admin/AccessDenied";
    options.ReturnUrlParameter = "";
    //options.Events.OnRedirectToLogin = context =>
    //{
    //    context.Response.Redirect("/Admin/Login");
    //    return Task.CompletedTask;
    //};
    //options.Events.OnRedirectToAccessDenied = context =>
    //{
    //    context.Response.Redirect("/Admin/AccessDenied");
    //    return Task.CompletedTask;
    //};
    //options.Events.OnRedirectToLogout = context =>
    //{
    //    context.Response.Redirect("/Admin/Logout");
    //    return Task.CompletedTask;
    //};
});
builder.Services.AddControllersWithViews();
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

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "Dash-board",
//    defaults: new { controller = "Home", action = "Index" });
app.MapControllerRoute(
  name: "areas",
  pattern: "{area=Admin}/{controller=Login}/{action=Index}/{id?}"
);
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

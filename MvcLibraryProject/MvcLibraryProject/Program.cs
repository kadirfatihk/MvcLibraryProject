using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // It adds the services and components required to create and serve web pages using the MVC structure.

// Configures Cookie Authentication.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.LogoutPath = new PathString("/");
    options.AccessDeniedPath = new PathString("/");
});

var app = builder.Build();

app.UseAuthentication();  // to active authentication 

app.UseStaticFiles();   // For use of static files

app.MapControllerRoute  // Default routing process.  SignUp is selected as the default page.
    (
        name: "default",
        pattern: "{Controller=Auth}/{Action=SignUp}/{id?}"
    );

app.Run();

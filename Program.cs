using Microsoft.AspNetCore.Authentication.Cookies;
using MRoom.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<MRoomDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnString")));

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = new PathString("/Home/Login"));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = new PathString("/Home/Login");
            options.LogoutPath = new PathString("/Home/Logout");
            options.AccessDeniedPath = "/";
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

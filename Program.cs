using LoginSocialNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
services.AddIdentity<AppUser, IdentityRole<int>>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

services.AddAuthentication()
    .AddGoogle(opts =>
    {
        opts.ClientId = configuration["Authentication:Google:ClientId"];
        opts.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        opts.SignInScheme = IdentityConstants.ExternalScheme;
    });
services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

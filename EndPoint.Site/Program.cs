using Microsoft.AspNetCore.Authentication.Cookies;
using ModirOnline.Application.Interfaces.Contexts;
using ModirOnline.Application.Services.Material.Commands;
using ModirOnline.Application.Services.Material.Queries;
using ModirOnline.Application.Services.Product.Commands;
using ModirOnline.Common;
using ModirOnline.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
    options.AddPolicy(UserRoles.Customer, policy => policy.RequireRole(UserRoles.Customer));
    options.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/Authentication/Signin");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
});

builder.Services.AddScoped<IModirOnlineDbContext, ModirOnlineDbContext>();
builder.Services.AddScoped<IMaterialManagmentService, MaterialManagmentService>();
builder.Services.AddScoped<IGetAllMaterialCategoriesService, GetAllMaterialCategoriesService>();
builder.Services.AddScoped<IGetAllInventoriesService, GetAllInventoriesService>();
builder.Services.AddScoped<IProductManagmentService, ProductManagmentService>();


// Add services to the container.
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

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

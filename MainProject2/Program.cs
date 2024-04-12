using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.DataAccess.Data;
using Project.DataAccess.Repository;
using Project.DataAccess.Repository.IRepository;
using Project.Utility;

// final update to git

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); // we are binding the EF with the identity tables.



builder.Services.ConfigureApplicationCookie(options => // hol la aaml overwrite 3l identity access lal admin page
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // hon ma3neta l interface hie awl arg w tene arg howe l implementation
builder.Services.AddScoped<IEmailSender, EmailSender>();

//In.NET MVC projects, it's common to use dependency injection (DI) to manage dependencies and promote loose coupling between components.
//Let's break down what each part of your code does:
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//This line registers the IUnitOfWork interface and its corresponding implementation UnitOfWork with the dependency injection container during application startup.
//It specifies that a new instance of UnitOfWork will be created for each HTTP request (scoped lifetime) and that it should be injected wherever an IUnitOfWork dependency is requested.
//private readonly IUnitOfWork _unitOfWork;:
//This line declares a private field _unitOfWork of type IUnitOfWork in your class.
//Without DI, you would typically instantiate UnitOfWork directly within your class, like this: _unitOfWork = new UnitOfWork();
//However, by using DI, you can inject IUnitOfWork into your class, which allows for easier testing and better separation of concerns.
//By registering IUnitOfWork with the DI container and injecting it into your classes, you're adhering to the dependency inversion principle,
//which states that high-level modules should not depend on low-level modules but rather both should depend on abstractions.
//In this case, your class doesn't depend directly on the UnitOfWork implementation; instead, it depends on the IUnitOfWork interface,
//which can be easily replaced with a different implementation if needed (e.g., for testing or changing database providers) without modifying your class's code.

//So, in summary, using dependency injection with builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//allows for more flexible and testable code compared to manually instantiating dependencies within your classes.

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


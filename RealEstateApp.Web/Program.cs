using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using RealEstateApp.Web.Components;
using RealEstateApp.Web.Data;
using RealEstateApp.Web.Models;
using RealEstateApp.Web.Services;

var builder = WebApplication.CreateBuilder(args);

//
// DATABASE
//

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

//
// BLAZOR
//

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

//
// IDENTITY
//

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;

        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;

        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//
// COOKIE LOGIN PATH FIX
//

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/auth/login";
    options.AccessDeniedPath = "/auth/login";
});

//
// AUTHORIZATION
//

builder.Services.AddAuthorization();

//
// SERVICES
//

builder.Services.AddScoped<ImageUploadService>();

var app = builder.Build();

//
// DATABASE + ROLES + ADMIN SEED
//

using (var scope = app.Services.CreateScope())
{
    var services =
        scope.ServiceProvider;

    var db =
        services.GetRequiredService<AppDbContext>();

    var roleManager =
        services.GetRequiredService<RoleManager<IdentityRole>>();

    var userManager =
        services.GetRequiredService<UserManager<ApplicationUser>>();

    db.Database.EnsureCreated();

    //
    // ROLES
    //

    var roles = new[]
    {
        "Admin",
        "Agent",
        "User"
    };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(
                new IdentityRole(role)
            );
        }
    }

    //
    // ADMIN USER
    //

    var adminEmail =
        "admin@realestate.com";

    var adminPassword =
        "123456";

    var adminUser =
        await userManager.FindByEmailAsync(adminEmail);

    if (adminUser is null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FullName = "System Admin",
            EmailConfirmed = true
        };

        var createResult =
            await userManager.CreateAsync(
                adminUser,
                adminPassword
            );

        if (!createResult.Succeeded)
        {
            foreach (var error in createResult.Errors)
            {
                Console.WriteLine(error.Description);
            }
        }
    }

    if (adminUser is not null)
    {
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(
                adminUser,
                "Admin"
            );
        }
    }
}

//
// MIDDLEWARE
//

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseAntiforgery();

//
// LOGIN
//

app.MapPost("/auth/login-post", async (
    HttpContext http,
    SignInManager<ApplicationUser> signInManager) =>
{
    var form =
        await http.Request.ReadFormAsync();

    var email =
        form["Email"].ToString().Trim();

    var password =
        form["Password"].ToString();

    if (string.IsNullOrWhiteSpace(email) ||
        string.IsNullOrWhiteSpace(password))
    {
        return Results.Redirect("/auth/login?error=1");
    }

    var result =
        await signInManager.PasswordSignInAsync(
            email,
            password,
            isPersistent: false,
            lockoutOnFailure: false
        );

    if (result.Succeeded)
    {
        return Results.Redirect("/profile");
    }

    return Results.Redirect("/auth/login?error=1");
})
.DisableAntiforgery();

//
// LOGOUT
//

app.MapPost("/auth/logout-post", async (
    SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();

    return Results.Redirect("/");
})
.DisableAntiforgery();

//
// BLAZOR
//

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
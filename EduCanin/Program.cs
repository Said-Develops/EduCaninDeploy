using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EduCanin.Data;
using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using EduCanin.Repositories;
using EduCanin.Service;
using EduCanin.Service.Interfaces;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Durée du blocage
    options.Lockout.MaxFailedAccessAttempts = 5; // Nombre d’échecs avant le blocage
    options.Lockout.AllowedForNewUsers = true; // Active le blocage même pour les nouveaux comptes

    options.SignIn.RequireConfirmedAccount = false;

}).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//D�but de mon injection de d�pendance
//Ici les Repository
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
builder.Services.AddScoped<ICourseSessionRepository, CourseSessionRepository>();
builder.Services.AddScoped<ICourseTypeRepository, CourseTypeRepository>();
builder.Services.AddScoped<IBreedRepository, BreedRepository>();
builder.Services.AddScoped<IDogCourseSessionRepository, DogCourseSessionRepository>();
builder.Services.AddScoped<IDogRepository, DogRepository>();

//Ici les Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
builder.Services.AddScoped<IDogService, DogService>();
builder.Services.AddScoped<IBreedService, BreedService>();
builder.Services.AddScoped<ICourseTypeService, CourseTypeService>();
builder.Services.AddScoped<ICourseSessionService, CourseSessionService>();
builder.Services.AddScoped<IDogCourseSessionService, DogCourseSessionService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
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
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}


// On cree un scope manuellement avec 'using' pour garantir qu'il sera libere une fois utilise.
// ASP.NET Core ne cree pas automatiquement de scope ici car on est en dehors d'une requete HTTP.
// Sans scope, les services avec une duree de vie 'scoped' comme RoleManager ou DbContext ne peuvent pas fonctionner correctement.
using (IServiceScope scope = app.Services.CreateScope())
{
    // On recupere le fournisseur de services associe au scope.
    // C est lui qui va nous permettre d�obtenir les services comme RoleManager ou UserManager.
    IServiceProvider services = scope.ServiceProvider;

    // Appel de la methode qui va creer les roles (User, Coach, Admin) s ils n existent pas encore.
    await CreateRolesAsync(services);
}




app.Run();


static async Task CreateRolesAsync(IServiceProvider serviceProvider)
{
    // On recupere le service RoleManager via l injection de dependances.
    // Il permet de gerer les reles dans l application (creation, suppression, etc.).
    //C est une methode d extension(fournie par Microsoft.Extensions.DependencyInjection) qui permet de demander un service de type T
    RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Liste des roles qu on veut avoir dans l application.
    string[] roles = { "User", "Coach", "Admin" };

    // On parcourt chaque role
    foreach (string role in roles)
    {
        // On verifie si le role existe deja dans la base
        bool roleExists = await roleManager.RoleExistsAsync(role);

        // S il n existe pas, on le cree avec le RoleManager
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

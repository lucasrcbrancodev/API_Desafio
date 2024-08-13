using API_Desafio.Application._Extensions;
using API_Desafio.ApplicationServices.Users;
using API_Desafio.Domain.Models;
using API_Desafio.Infrastructure._Extensions;
using API_Desafio.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Auth/Login";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
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

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider.GetRequiredService<IRandomUserGeneratorAPIService>();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

var pendingMigrations = context.Database.GetPendingMigrations();

app.Services.ExecutePendingMigrations(app.Environment.IsDevelopment(), pendingMigrations);

if (pendingMigrations.Any(m => m.Contains("Initial")))
{
    var user = new User(
            documentInformation: new DocumentInformation(
                Type: "Not Informed",
                Number: "Not Informed"),

            nameInformation: new NameInformation(
                First: "Super",
                Last: "Admin"),

            loginInformation: new LoginInformation(
                Username: "superadmin",
                Password: "7DJB4tD+MOK+VU/iiKJCRRsGhcKSzZiRac4ndVSGAaA=",
                Salt: "S3CR3TS4LT!",
                SHA256: "Not Informed"),

            locationInformation: new LocationInformation(
                Number: 0,
                Name: "Not Informed",
                City: "Not Informed",
                State: "Not Informed",
                Country: "Not Informed",
                Postcode: 0),

            dateOfBirthInformation: new DateOfBirthInformation(
                dateOfBirth: DateTime.MinValue,
                age: 0),

            imageInformation: new ImageInformation(Thumbnail: "Not Informed"),

            contactInformation: new ContactInformation(
                Email: "super@admin.com",
                Phone: "Not Informed",
                Cell: "Not Informed"),

            gender: "Not Informed",
            nat: "Not Informed");

    var randomUsers = await service.GetRandomUsersFromAPIAsync(amount: 5);

    foreach (var item in randomUsers)
    {
        var randomUser = item.results.FirstOrDefault();
        if (randomUser is null)
        {
            continue;
        }

        var superAdmin = new User(
            documentInformation: new DocumentInformation(
                randomUser.id.name,
                randomUser.id.value),

            nameInformation: new NameInformation(
                randomUser.name.first,
                randomUser.name.last),

            loginInformation: new LoginInformation(
                randomUser.login.username,
                randomUser.login.password,
                randomUser.login.salt,
                randomUser.login.sha256),

            locationInformation: new LocationInformation(
                randomUser.location.street.number,
                randomUser.location.street.name,
                randomUser.location.city,
                randomUser.location.state,
                randomUser.location.country,
                randomUser.location.postcode),

            dateOfBirthInformation: new DateOfBirthInformation(
                randomUser.dob.date,
                randomUser.dob.age),

            imageInformation: new ImageInformation(randomUser.picture.thumbnail),

            contactInformation: new ContactInformation(
                randomUser.email,
                randomUser.phone,
                randomUser.cell),

            randomUser.gender,
            randomUser.nat);

        await context.User.AddAsync(superAdmin);

        try
        {
            await context.User.AddAsync(user);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}

app.Run();

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Infrastructure.Data;
using UniversitySystem.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// MVC
// =======================================
builder.Services.AddControllersWithViews();

// =======================================
// DATABASE
// =======================================
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

// =======================================
// AUTHENTICATION (COOKIE)
// =======================================
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";

        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;

        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

// =======================================
// AUTHORIZATION
// =======================================
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
    options.AddPolicy("TeacherOnly", p => p.RequireRole("Teacher"));
    options.AddPolicy("StudentOnly", p => p.RequireRole("Student"));
});

// =======================================
// DEPENDENCY INJECTION
// =======================================
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

var app = builder.Build();

// =======================================
// ERROR HANDLING
// =======================================
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// =======================================
// AUTO DATABASE INIT + SEED
// =======================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();

    // =========================
    // USER SEED
    // =========================
    var hasher = new PasswordHasher<AppUser>();

    if (!db.Users.Any())
    {
        var users = new List<AppUser>
        {
            new() { Username="admin", FullName="System Admin", Role="Admin" },
            new() { Username="teacher", FullName="Default Teacher", Role="Teacher" },
            new() { Username="student", FullName="Default Student", Role="Student" }
        };

        foreach (var user in users)
        {
            user.PasswordHash = hasher.HashPassword(user, "123456");
        }

        db.Users.AddRange(users);
        db.SaveChanges();
    }

    // =========================
    // 🔥 DEPARTMENT SEED (IMPORTANT FIX)
    // =========================
    if (!db.Departments.Any())
    {
        db.Departments.AddRange(
            new Department { Name = "CSE" },
            new Department { Name = "EEE" },
            new Department { Name = "BBA" },
            new Department { Name = "Law" }
        );

        db.SaveChanges();
    }
}

// =======================================
// MIDDLEWARE
// =======================================
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// =======================================
// ROUTES
// =======================================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
);

app.Run();
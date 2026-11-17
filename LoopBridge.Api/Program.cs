using LoopBridge.Api.Common;
using LoopBridge.Models;
using LoopBridge.Repositories;
using LoopBridge.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
JwtSettings.OpenApiSetting(builder);

IConfiguration configuration = builder.Configuration;

builder.Services.AddDbContextPool<ApplicationDbContext>(e =>
{
    e.UseMySql(
        configuration.GetConnectionString("LocalConnection"),
        ServerVersion.AutoDetect(configuration.GetConnectionString("LocalConnection")),
        e => e.MigrationsAssembly("LoopBridge.Api")
        );
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

JwtSettings.Configure(builder);


builder.Services.AddScoped<IStudent, StudentRepository>();
builder.Services.AddScoped<IParent, ParentRepository>();
builder.Services.AddScoped<ISchool, SchoolRepository>();
builder.Services.AddScoped<IClassTeacher, ClassTeacherRepository>();
builder.Services.AddScoped<ITeacher, TeacherRepository>();
builder.Services.AddScoped<IRoom, RoomRepository>();
builder.Services.AddScoped<IApplicationUser, ApplicationUserRepository>();
builder.Services.AddScoped<IDiary, DiaryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(e => e.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    app.MapOpenApi();
    app.MapScalarApiReference("/", e =>
    {
        e.Theme = ScalarTheme.BluePlanet;
        e.WithTitle("LoopBridge_Testing");
        e.Authentication = new ScalarAuthenticationOptions { PreferredSecurityScheme = JwtBearerDefaults.AuthenticationScheme };
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

 

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

using EduConnect.Data;
using EduConnect.Entities;
using EduConnect.Repositories;
using EduConnect.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//them db context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

//them Service
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IParentService, ParentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<EduConnect.Services.IReportService, ReportService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IYearService, YearService>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<ITermService, TermService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IUserService, UserService>();

//them repository
builder.Services.AddScoped<IScoreRepository, ScoreRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<EduConnect.Repositories.IReportRepository, ReportRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IYearRepository, YearRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
builder.Services.AddScoped<ITermRepository, TermRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

//cau hinh JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    //doc key tu appsetting.json
    var jwtConfig = builder.Configuration.GetSection("Jwt");
    var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]!);

    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig["Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        RoleClaimType = ClaimTypes.Role
    };
});

//them cors (cross-origin resource sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EduConnect API",
        Version = "v1",
        Description = "API documentation for EduConnect system"
    });

    // them cau hinh JWT va hien Authorize
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,              
        Scheme = "Bearer",                            
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {your JWT token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EduConnect API v1");
        c.DocumentTitle = "EduConnect Swagger UI";
    });
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

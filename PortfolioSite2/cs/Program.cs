using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortfolioSite2.Services;

class Program {
    static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        
        builder.Services.AddCors(options => {
            options.AddPolicy("OpenCorsPolicy", policy => {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        builder.Services.ConfigureApplicationCookie(options => {
            options.Cookie.HttpOnly = true;
            options.Events.OnRedirectToLogin = context => {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
            options.Events.OnRedirectToAccessDenied = context => {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            };
        });
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<BlogService>();
        builder.Services.AddScoped<ProjectenService>();
        
        
        var app = builder.Build();
        
        app.UseCors("OpenCorsPolicy");
        
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.Urls.Add("http://localhost:5050");
        Console.WriteLine("http://localhost:5050/api/projecten");
        Console.WriteLine("http://localhost:5050/api/blog");
        Console.WriteLine("http://localhost:5050/swagger");
        
        app.MapControllers();
        app.UseHttpsRedirection();
        app.Run();
    }
}
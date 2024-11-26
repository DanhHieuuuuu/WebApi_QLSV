using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Services.Implements;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Configure JWT settings
            var jwtSettings = new Jwtsettings();
            builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
            builder.Services.AddSingleton(jwtSettings);

            // Configure JWT authentication
            builder
                .Services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.SecretKey)
                        ),
                    };
                });
            builder.Services.AddAuthorization();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IStudentServices, StudentServices>();
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddScoped<IManagerService, ManagerService>();
            builder.Services.AddScoped<IKhoaService, KhoaService>();
            builder.Services.AddScoped<INganhService, NganhService>();
            builder.Services.AddScoped<ILopQLService, LopQLService>();
            builder.Services.AddScoped<IBoMonService, BoMonService>();
            builder.Services.AddScoped<ICauHoiService, CauHoiService>();
            builder.Services.AddScoped<IMonHocService, MonHocService>();
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }
                );
            });

            var app = builder.Build();
            app.UseCors("AllowAllOrigins");
            //app.UseCors("AllowSpecificOrigins");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            //http://192.168.23.229:5024/swagger/index.html

            // Sử dụng middleware để phục vụ các tệp tĩnh
            app.UseStaticFiles();
            //app.Run("http://192.168.59.229:5024");
            //app.Urls.Add("http://192.168.1.2:5000");
            app.Run();
        }
    }
}

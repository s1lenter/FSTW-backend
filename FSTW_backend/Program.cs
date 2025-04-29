using FSTW_backend.Middlewares;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace FSTW_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5001", "http://localhost:3000", "http://localhost")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        //policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAuthTokenService, AuthTokenService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["AppSettings:Audience"],
                        ValidateLifetime = true,
                        //ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:TokenKey"]!)),
                        ValidateIssuerSigningKey = true
                    };
                });


            var app = builder.Build();

            app.Logger.LogInformation(connectionString);

            app.UseMiddleware<TokenHeaderMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

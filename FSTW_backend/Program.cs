using FSTW_backend.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FSTW_backend.Mapping;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using FSTW_backend.Services.Auth;
using FSTW_backend.Services.PersonalCabinet;
using FSTW_backend.Services.ResumeService;
using FSTW_backend.Services.Admin;
using FSTW_backend.Services.Neuro;
using FSTW_backend.Services.Internships;
using FSTW_backend.Services.SitesParsingServices;

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
                        policy.WithOrigins("http://localhost")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        options.AddPolicy("SwaggerPolicy", policy =>
                         {
                             policy.WithOrigins("http://localhost:5072") 
                                   .AllowAnyHeader()
                                   .AllowAnyMethod();
                         });
                    });
            });

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0).FirstOrDefault();

                    var result = new List<Dictionary<string, string>>();

                    result.Add(new Dictionary<string, string>()
                    {
                        { "Error", errors.Value.Errors[0].ErrorMessage }
                    });

                    return new BadRequestObjectResult(result);
                };
            });
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(AppMapperProfile));

            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 
            var connectionString = builder.Configuration.GetConnectionString("LocalConnection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAuthTokenService, AuthTokenService>();

            builder.Services.AddScoped<IPersonalCabinetService, PersonalCabinetService>();

            builder.Services.AddScoped<IResumeEditorService, ResumeEditorService>();

            builder.Services.AddScoped<IAdminService, AdminService>();

            builder.Services.AddScoped<INeuronetService, NeuronetService>();

            builder.Services.AddScoped<IInternshipService, InternshipService>();

            builder.Services.AddScoped<IAlfaParsingService, AlfaParsingService>();

            builder.Services.AddHttpClient("hh.ru")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
                }); ;

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
                        ClockSkew = TimeSpan.Zero,
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
            app.UseCors("SwaggerPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

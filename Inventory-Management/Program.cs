
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Common;
using Common.Helpers;
using DotNetEnv;
using Hangfire;
using Hangfire.SqlServer;
using Inventory_Management.Common.Middlewares;
using Inventory_Management.Common.Profiles;
using Inventory_Management.Features.Common.BackGround_jobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Inventory_Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            #region Swagger Bearer
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Food App Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. " +
                                    "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                                    "\r\n\r\nExample: \"Bearer abcdefghijklmnopqrstuvwxyz\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
            });
            #endregion
            builder.Services.AddSwaggerGen();
            //Enviroment
            Env.Load();
            // Add services to the container.
            builder.Services.AddFluentEmail("maim6349@gmail.com")
           .AddRazorRenderer()  // or AddLiquidRenderer() if you want to use Liquid templates
           .AddSmtpSender(new SmtpClient("smtp.gmail.com")
           {
               UseDefaultCredentials = false,
               Credentials = new NetworkCredential("maim6349@gmail.com", "rzam ngki omum hbgw"),
               EnableSsl = true,
               Port = 587
           });




            builder.Services.AddHangfire(configuration =>
     configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                  .UseSimpleAssemblyNameTypeSerializer()
                  .UseRecommendedSerializerSettings()
                  .UseSqlServerStorage("Server=.;Database=test123;Trusted_Connection=True;Encrypt=False;",
                                       new SqlServerStorageOptions
                                       {
                                           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                           QueuePollInterval = TimeSpan.Zero,
                                           UseRecommendedIsolationLevel = true,
                                           DisableGlobalLocks = true
                                       }));

            // Add Hangfire Server
            builder.Services.AddHangfireServer();


            #region AutoFac
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
                builder.RegisterModule(new AutoFacModule()));
            #endregion

            #region AutoMapper
            builder.Services.AddAutoMapper(typeof(UserProfile));
            #endregion

            #region MediatR

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            #endregion


            #region Authentication 
            builder.Services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Environment.GetEnvironmentVariable("ISSUER"),
                        ValidAudience = Environment.GetEnvironmentVariable("AUDIENCE"),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY")))
                    };
                });
            #endregion



            var app = builder.Build();

            app.UseRouting();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHangfireDashboard("/hangfire");

            // Register the recurring job
            RecurringJob.AddOrUpdate<SampleJob2>(
                job => job.ExecuteJobAsync(),
                Cron.Daily(11, 25) // 15 = 3 PM, 30 = 30 minutes
            );

            RecurringJob.AddOrUpdate<SampleJob1>(
                job => job.ExecuteJob(),
                Cron.Daily(12, 25) // 15 = 3 PM, 30 = 30 minutes
            );


            app.UseHttpsRedirection();


            app.UseHttpsRedirection();
            app.UseMiddleware<GlobalErrorHandlerMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            MapperHelper.Mapper = app.Services.GetService<IMapper>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); 
            app.MapControllers();

            app.Run();
        }
    }
}

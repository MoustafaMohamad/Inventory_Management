
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Common;
using Common.Helpers;
using DotNetEnv;
using FluentValidation;
using Inventory_Management.Common.Middlewares;
using Inventory_Management.Common.Profiles;
using Inventory_Management.Features.Products.AddProduct;
using Inventory_Management.Features.Products.AddProduct.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
            builder.Services.AddScoped<IValidator<AddProductEndPointRequest>, AddProductValidator>();

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

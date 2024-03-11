using Application.Interface;
using Application.Interface.Repository;
using Infrasturcture.Presistence.Context;
using Infrasturcture.Presistence.Repository;
using Infrasturcture.shared.Models;
using Infrasturcture.shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.TwiML.Messaging;
using User.Management.Service.Services;

namespace Infrasturcture.Presistence
{
    public static class ServiceExtention
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(option=>
                                                    option.UseSqlServer(configuration.GetConnectionString("DBConnect"),
                                                    builder=>builder.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
                                                    ));

            services.AddScoped<ApplicationContext>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();   
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IPictureHandler,PictureHandler>();
            
            //  services.AddSingleton<EncryptionService>();


            services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidAudience = configuration["JWT:ValidAudience"],
                     ValidIssuer = configuration["JWT:ValidIssuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                 };
             });
            services.AddAuthorization();
            services.AddTransient<EmailService>(); // AddTransient is just an example, use appropriate lifetime

            // Add logging services
            services.AddLogging();
            services.AddScoped<TwilioSettings>();
            services.Configure<TwilioSettings>(configuration.GetSection("TwilioSettings"));
             services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
        }
}
    }

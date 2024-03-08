using Application.Interface;
using Application.Interface.Repository;
using Infrasturcture.Presistence.Context;
using Infrasturcture.Presistence.Repository;
using Infrasturcture.Presistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           services.AddSingleton<EncryptionService>(new EncryptionService(configuration["EncryptionKey"]));
          //  services.AddSingleton<EncryptionService>();
        }
}
    }

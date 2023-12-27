using BLL.ManagerServices.Abstracts;
using BLL.ManagerServices.Concretes;
using DAL.Repositories.Abstracts;
using DAL.Repositories.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DependencyResolvers
{
    public static class RepositoryManagerServiceInjection
    {
        public static IServiceCollection AddRepositoryManagerServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IManager<>), typeof(BaseManager<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactManager, ContactManager>();

            return services;

        }

        //Dependency Injection kullanarak repository ve manager servislerini kayıt eder.
        //IServiceCollection kullanılarak Dependency Injection yapılandırması.
        //IManager<> ve BaseManager<> ile generic manager servisi eklenir.
        //Özel repository ve manager sınıfları eklenir.

        //Dependency Injection sayesinde bu servisleri gerektiğinde kullanabiliyoruz.
        //bir Controller içinde, constructor içinde gerektiğinde kullanıyoruz.
    }
}

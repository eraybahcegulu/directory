using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DependencyResolvers
{
    public static class DbContextServiceInjection
    {
        public static IServiceCollection AddDbContextService(this IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            IConfiguration configuration = provider.GetService<IConfiguration>();
            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies());
            return services;
        }

        //AddDbContextPool metodu kullanılarak MyContext adlı DbContext projeye ekleniyor.
        //UseSqlServer metodu ile Entity Framework e, SQL Server e bağlanılacak adres sağlanır.

        //UseLazyLoadingProxies metodu Entity Framework Core un lazy loading özelliğini etkinleştirir.
        //Bu özellik ilişkili nesnelerin sadece ihtiyaç duyulduğunda veritabanından alınmasını sağlar.

        //bu yol ile IServiceCollection üzerinden DbContext in eklenmesi sağlanır.
        //IServiceCollection ile servislerin kayıt edilmesi sağlandı ve yönetildi.
    }
}
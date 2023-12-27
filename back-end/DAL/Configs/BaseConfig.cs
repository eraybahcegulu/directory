using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configs
{
    public abstract class BaseConfig<T> : IEntityTypeConfiguration<T> where T : class, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {

        }
    }

    //IEntityTypeConfiguration arayüzü uygulanarak Configure metodu sağlanıyor. FluentAPI ile yapılandırma sağlanıyor.
    //Configure metodu FluentAPI ile yapılandırma ayarlarını içeriyor.
    //EntityTypeBuilder nesnesi varlığın özelliklerini ve ilişkilerini belirtmek için Flutent API tarafından kullanılan builder nesnesidir.
    //CategoryConfig, ProductConfig gibi özel yapılandırma sınıflarının BaseConfig<T> i genişletmesi sağlanıyor.
    //Ortak yapılandırmalar BaseConfigde toplanıyor.
    //Kod tekrarını önlemek.

}
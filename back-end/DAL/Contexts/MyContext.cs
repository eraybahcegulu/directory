using DAL.Configs;
using ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DAL.Context
{

    //EF Core DbContext sınıfı, ASP.NET Core Identity sistemi
    //MyContext sınıfı DbContext sınıfını genişletiyor. Veritabanı etkileşimlerini yönetiyor
    public class MyContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public MyContext(DbContextOptions<MyContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new ContactConfig());

            var passwordHasher = new PasswordHasher<User>();

            builder.Entity<User>().HasData(
                new User
                {
                    Id = -1,
                    Name = "admin",
                    Email = "admin@admin.com",
                    UserName = "admin",
                    PasswordHash = passwordHasher.HashPassword(null, "admin123"),
                },

                new User
                {
                    Id = -2,
                    Name = "admin2",
                    Email = "admin2@admin.com",
                    UserName = "admin2",
                    PasswordHash = passwordHasher.HashPassword(null, "admin123"),
                }
                );

            builder.Entity<Contact>()
              .HasOne(c => c.User)
              .WithMany(u => u.Contacts)
              .HasForeignKey(c => c.CreatedUserID)
              .IsRequired();
        }

        //veritabanı modelleri oluşturulması sırasında yapılacak configler içeriliyor.

        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }   //MyContext sınıfındaki her entity için DbSet oluşturuldu.
}

    //Entity Framework Core Code-First ile veritabanı modeli oluşturma ve yapılandırma
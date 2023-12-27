using ENTITIES.Enums;
using ENTITIES.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ENTITIES.Models
{
    public class User : IdentityUser<int>, IEntity
    {

        public User()
        {
            CreatedDate = DateTime.Now;
            Status = DataStatus.Active;
        }

        [JsonPropertyName("userId")]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DataStatus Status { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}

//ASP.NET Core Identity deki IdentityUser sınıfından türetiliyor, IEntity arayüzünü uyguluyor
using ENTITIES.Enums;
using ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES.Models
{
    public abstract class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
            Status = DataStatus.Active;
        }
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }

        public DataStatus Status { get; set; }
    }
}

//BaseEntity soyut sınıfı. IEntity arayüzünü uyguluyor ve temel entity yapısı sağlıyor.
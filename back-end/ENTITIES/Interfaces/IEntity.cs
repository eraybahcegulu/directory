using ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES.Interfaces
{
    public interface IEntity
    {
        public int ID { get; set; }
        public DataStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

// IEntity arayüzü. veri modelini temsil etmek üzere oluşturuldu. 
// Bu arayüzü sağlayan sınıflar arayüzün tanımladığı özelliklere sahit olmak zorunda.
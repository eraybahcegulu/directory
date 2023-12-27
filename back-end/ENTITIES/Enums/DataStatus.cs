using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES.Enums
{
    public enum DataStatus
    {
        Active = 1,
        Deleted = 2,
    }
}


//soft delete için nesnelerin aktif veya silinmiş olması sağlanıyor. veritabanından tamamen silinmiyor.
using ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ManagerServices.Abstracts
{
    public interface IUserManager : IManager<User>
    {
        Task<bool> CreateUserAsync(User item);
        Task<User> FindByNameAsync(string userName);

        Task<User> FindByEmailAsync(string email);
        Task UpdateSecurityStampAsync(User user);
    }
}

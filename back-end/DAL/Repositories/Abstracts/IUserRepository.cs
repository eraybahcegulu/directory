using ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Abstracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByNameAsync(string userName);

        Task<User> FindByEmailAsync(string email);
        Task<bool> AddUser(User item);

        Task UpdateSecurityStampAsync(User user);
    }
}

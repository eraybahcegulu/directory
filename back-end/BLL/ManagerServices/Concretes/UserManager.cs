using DAL.Repositories.Abstracts;
using ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ManagerServices.Abstracts;

namespace BLL.ManagerServices.Concretes
{
    public class UserManager : BaseManager<User>, IUserManager
    {

        IUserRepository _userRep;

        public UserManager(IUserRepository userRep) : base(userRep)
        {
            _userRep = userRep;
        }

        public async Task<User> FindByNameAsync(string userName)
        {

            return await _userRep.FindByNameAsync(userName);

        }

        public async Task<User> FindByEmailAsync(string email)
        {

            return await _userRep.FindByEmailAsync(email);


        }
        public async Task<bool> CreateUserAsync(User item)
        {
            return await _userRep.AddUser(item);
        }

        public async Task UpdateSecurityStampAsync(User user)
        {
            await _userRep.UpdateSecurityStampAsync(user);

        }

    }
}

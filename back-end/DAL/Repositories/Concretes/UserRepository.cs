using DAL.Context;
using DAL.Repositories.Abstracts;
using DAL.Repositories.Concretes;
using ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(MyContext db, UserManager<User> userManager) : base(db)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<User> FindByNameAsync(string userName)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> AddUser(User item)
    {
        IdentityResult result = await _userManager.CreateAsync(item, item.PasswordHash);
        return result.Succeeded;
    }

    public async Task UpdateSecurityStampAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        user.SecurityStamp = Guid.NewGuid().ToString();
        await _userManager.UpdateAsync(user);
    }
}

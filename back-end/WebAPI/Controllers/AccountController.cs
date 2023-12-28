using Microsoft.AspNetCore.Mvc;
using ENTITIES.Models;
using BLL.ManagerServices.Abstracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BLL.ManagerServices.Concretes;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IUserManager userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //yapıcı metod IUserManager, SignInManager<User> DependencyInjection bağımlılıkların sağlanması.
        //SignInManager<User> ASP.NET Core Identity sisteminde kullanıcı oturum açma işlemlerini yönetiyor




        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                var claimsPrincipal = TokenService.GetPrincipal(token, "eraybahcegulu_key");

                if (claimsPrincipal != null)
                {
                    var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

                    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
                    {
                        var user = await _userManager.FindAsync(userId);

                        if (user != null)
                        {
                            var username = user.UserName;
                            var email = user.Email;
                            var id = user.Id;
                            var securityStamp = user.SecurityStamp;
                            return Ok(new { username, email,id, securityStamp });
                        }
                    }
                }

                return StatusCode(401, new { Message = "Unauthorized" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server error", Error = ex.Message });
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(model.UsernameOrEmail)
                      ?? await _userManager.FindByEmailAsync(model.UsernameOrEmail);
                    //kullanıcıyı ad veya emaile göre bul

                    if (user != null)//bulunursa
                    {
                        await _userManager.UpdateSecurityStampAsync(user); //UpdateSecurityStampAsync

                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                        //şifre kontrolü ve succeeded

                        if (result.Succeeded)
                        {
                            var id = user.Id;
                            var username = user.UserName;
                            var securityStamp = user.SecurityStamp;

                            var tokenHandler = new JwtSecurityTokenHandler();
                            var key = Encoding.ASCII.GetBytes("eraybahcegulu_key");
                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new[]
                                        {
                                            new Claim(ClaimTypes.Name, username),
                                            new Claim(ClaimTypes.NameIdentifier, id.ToString())
                                        }),
                                Expires = DateTime.UtcNow.AddDays(30),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                            };

                            var token = tokenHandler.CreateToken(tokenDescriptor);

                            return Ok(new
                            {
                                Message = "Giriş başarılı",
                                Token = tokenHandler.WriteToken(token),

                                securityStamp
                            });
                        }
                    }
                }
                //Kullanıcı bulunamazsa
                return BadRequest(new { Message = "Geçersiz kullanıcı adı veya şifre" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Sunucu hatası", Error = ex.Message });
            }
        }

    }
}
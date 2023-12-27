using BLL.ManagerServices.Abstracts;
using ENTITIES.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebAPI.ViewModels;


[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactManager _contactManager;

    public ContactController(IContactManager contactManager)
    {
        _contactManager = contactManager;
    }

    [HttpGet]
    public IActionResult GetContacts()
    {
        try
        {
            var contacts = _contactManager.GetActives().ToList();
            return Ok(contacts); //statusu active olanları al ve döndür
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Sunucu hatası", Error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContact(int id)
    {
        try
        {
            var contact = await _contactManager.FindAsync(id); // id ye sahip olan kişiyi bul ve döndür

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Sunucu hatası", Error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(ContactViewModel item)
    {
        try
        {

            Contact c = new() //ViewModel den alınan verilerle yeni bir contact oluştur
            {
                Phone = item.Phone,
                Name = item.Name,
                Surname = item.Surname,
                CreatedUserID = item.CreatedUserID
            };

            string result = _contactManager.Add(c); //yeni kişiyi ekle

            return Ok(result); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Sunucu hatası", Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactViewModel item)
    {
        var contact = _contactManager.FindAsync(id).Result;

        item.CreatedUserID = contact.CreatedUserID;

        try
        {
            Contact c = new() //temsil edecek nesne oluşturma
            {
                ID = id,
                Name = item.Name,
                Surname = item.Surname,
                Phone = item.Phone,
                CreatedUserID = item.CreatedUserID
            };

            await _contactManager.Update(c); //kişiyi güncelleme

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Sunucu hatası", Error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteContact(int id)
    {
        try
        {
            var contact = _contactManager.FindAsync(id).Result; //id ile bul

            if (contact == null)
            {
                return NotFound();
            }

            _contactManager.Delete(contact); //kişinin statusunu Active(1) durumundan Deleted(2) durumuna al
            return NoContent();
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { Message = "Sunucu hatası", Error = ex.Message });
        }
    }
}

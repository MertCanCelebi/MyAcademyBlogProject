using Blogy.Business.DTOs.ContactDtos;
using Blogy.Business.Services.ContactServices;
using Blogy.WebUI.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Roles.Admin}")]
    public class ContactController(IContactService _contactService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var values = await _contactService.GetAllAsync();
            return View(values);
        }
        public async Task<IActionResult> CreateContact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto contactDto)
        {
            if (!ModelState.IsValid)
            {
                return View(contactDto);
            }

            await _contactService.CreateAsync(contactDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateContact(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateContactDto);
            }

            await _contactService.UpdateAsync(updateContactDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

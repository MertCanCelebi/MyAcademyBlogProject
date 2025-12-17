using Blogy.Business.DTOs.MessageDtos;
using Blogy.Business.Services.MessageServices;
using Blogy.WebUI.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Roles.Admin}")]
    public class MessageController(IMessageService _messageService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var values = await _messageService.GetAllAsync();
            return View(values);
        }
        public async Task<IActionResult> CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDto messageDto)
        {
            if (!ModelState.IsValid)
            {
                return View(messageDto);
            }

            await _messageService.CreateAsync(messageDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateMessage(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateMessageDto);
            }

            await _messageService.UpdateAsync(updateMessageDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _messageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

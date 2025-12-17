using Blogy.Business.DTOs.CommentDtos;
using Blogy.Business.Services.CommentServices;
using Blogy.Business.Services.HuggingServices;
using Blogy.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Controllers
{
    public class CommentController(UserManager<AppUser> _userManager, ICommentService _commentService, IToxicityService _toxicityService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            var (isToxic, score) = await _toxicityService.AnalyzeAsync(dto.Content);

            dto.UserId = user.Id;
            dto.IsToxic = isToxic;
            dto.ToxicRate = score;

            await _commentService.CreateAsync(dto);

            if (dto.IsToxic)
            {
                TempData["Error"] =
                    "Yorumunuz toksik içerik barındırdığı için yayınlanmadı. ";
            }
            else
            {
                TempData["Success"] = "Yorumunuz başarıyla paylaşıldı.";
            }

            return RedirectToAction(
                "BlogDetails",
                "Blog",
                new { id = dto.BlogId }
            );
        }
    }
}

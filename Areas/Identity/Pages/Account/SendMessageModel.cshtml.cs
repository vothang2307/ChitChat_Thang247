using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ChiChat.Models;
using ChiChat.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChiChat.Areas.Identity.Pages.Account
{
    public class SendMessageModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MessageService _messageService;

        public SendMessageModel(UserManager<IdentityUser> userManager, MessageService messageService)
        {
            _userManager = userManager;
            _messageService = messageService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var senderId = _userManager.GetUserId(User);
                var receiver = await _userManager.FindByEmailAsync(Input.Email);

                if (receiver == null)
                {
                    ModelState.AddModelError(string.Empty, "Người dùng với email này không tồn tại.");
                    return Page();
                }

                var receiverId = receiver.Id;
                var content = Input.Content;

                await _messageService.SendMessageAsync(senderId, receiverId, content);

                return RedirectToPage("/Messages/Index");
            }

            return Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email người nhận")]
            public string Email { get; set; }

            [Required]
            [StringLength(500)]
            [Display(Name = "Nội dung tin nhắn")]
            public string Content { get; set; }
        }
    }
}

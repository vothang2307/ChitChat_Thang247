using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChiChat.Pages.Messages
{
    public class SentConfirmationModel : PageModel
    {
        public void OnGet()
        {
            
        }
        public IActionResult OnPost()
        {
            // Xử lý khi có dữ liệu được gửi từ form (nếu có)
            return RedirectToPage("/Messages/Index"); // Chuyển hướng đến trang Index trong thư mục Messages
        }

    }
}

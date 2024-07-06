using System.Collections.Generic;
using System.Threading.Tasks;
using ChiChat.Data;
using ChiChat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChiChat.Pages.Messages
{
    public class InboxModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public InboxModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Message> Messages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Messages = await _dbContext.Messages.ToListAsync();
            return Page();
        }
    }
}

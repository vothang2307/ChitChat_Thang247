using Microsoft.AspNetCore.Identity;
using System;
namespace ChiChat.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsLoggedIn { get; set; }
        
    }
}

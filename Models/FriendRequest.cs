using Microsoft.AspNetCore.Identity;

namespace ChiChat.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public IdentityUser Sender { get; set; }
        public string ReceiverId { get; set; }
        public IdentityUser Receiver { get; set; }
        public bool IsAccepted { get; set; }
    }
}

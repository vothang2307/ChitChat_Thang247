using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChiChat.Models
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string FriendId { get; set; }
        [Required]
        public string FriendEmail { get; set;}
        [NotMapped]
        public bool HasUnreadMessages { get; set; }
    }
}

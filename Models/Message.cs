using System;
using System.ComponentModel.DataAnnotations;

namespace ChiChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string ReceiverId { get; set; }
        [Required]
        [StringLength(500)]
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public string SenderEmail { get; set; }
        public bool IsRead { get; set; }
        public string Discriminator { get; set; }

    }
}

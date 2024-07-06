using ChiChat.Data;
using ChiChat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChiChat.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MessageService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Message>> GetMessagesAsync(string userId, string friendId)
        {
            var messages = await _context.Messages
                .Where(m => (m.SenderId == userId && m.ReceiverId == friendId) ||
                            (m.SenderId == friendId && m.ReceiverId == userId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            foreach (var message in messages)
            {
                message.Discriminator = message.Discriminator ?? string.Empty;
                message.SenderEmail = message.SenderEmail ?? string.Empty;
            }

            return messages;
        }



        public async Task SendMessageAsync(string senderId, string receiverId, string content)
        {
            if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId) || string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("SenderId, ReceiverId, and Content cannot be null or empty");
            }

            var sender = await _userManager.FindByIdAsync(senderId);
            var receiver = await _userManager.FindByIdAsync(receiverId);

            if (sender == null || receiver == null)
            {
                throw new ArgumentException("Sender or receiver not found");
            }

            var message = new Message
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Content = content,
                SentAt = DateTime.UtcNow,
                IsRead = false,
                Discriminator = "0",
                SenderEmail = "0"// or any valid default value
                

            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }


        public async Task MarkAsReadAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Message>> GetUnreadMessagesAsync(string userId)
        {
            return await _context.Messages
                .Where(m => m.ReceiverId == userId && !m.IsRead)
                .ToListAsync();
        }
        public async Task<bool> HasUnreadMessagesAsync(string userId, string friendId)
        {
            return await _context.Messages
                .AnyAsync(m => m.SenderId == friendId && m.ReceiverId == userId && !m.IsRead);
        }
        public async Task MarkMessagesAsRead(string userId, string friendId)
        {
            var unreadMessages = await _context.Messages
                .Where(m => m.SenderId == friendId && m.ReceiverId == userId && !m.IsRead)
                .ToListAsync();

            foreach (var message in unreadMessages)
            {
                message.IsRead = true;
            }

            await _context.SaveChangesAsync();
        }


    }
}

using ChiChat.Models;

namespace ChiChat.Services
{
    public interface IMessageService
    {
        Task SendMessageAsync(string senderId, string receiverId, string content);
        Task<List<Message>> GetMessagesAsync(string userId1, string userId2);
    }
}
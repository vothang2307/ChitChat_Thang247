using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChiChat.Data;
using ChiChat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChiChat.Services
{
    public class FriendService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FriendService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddFriendAsync(string userId, string friendId)
        {
            var friend = await _userManager.FindByIdAsync(friendId);
            var friendship = new Friendship
            {
                UserId = userId,
                FriendId = friendId,
                FriendEmail = friend?.Email // Assign the FriendEmail field if the friend is found
            };

            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFriendAsync(string userId, string friendId)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);

            if (friendship != null)
            {
                _context.Friendships.Remove(friendship);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Friendship>> GetFriendsAsync(string userId)
        {
            var friendships = await _context.Friendships
                .Where(f => f.UserId == userId)
                .ToListAsync();

            foreach (var friendship in friendships)
            {
                var friend = await _userManager.FindByIdAsync(friendship.FriendId);
                friendship.FriendEmail = friend?.Email;
            }

            return friendships;
        }

        public async Task<List<Friendship>> GetFriendsWithUnreadStatusAsync(string userId)
        {
            var friendships = await _context.Friendships
                .Where(f => f.UserId == userId)
                .ToListAsync();

            foreach (var friendship in friendships)
            {
                var friend = await _userManager.FindByIdAsync(friendship.FriendId);
                friendship.FriendEmail = friend?.Email;
                friendship.HasUnreadMessages = await _context.Messages
                    .AnyAsync(m => m.SenderId == friendship.FriendId && m.ReceiverId == userId && !m.IsRead);
            }

            return friendships;
        }

        public async Task<List<IdentityUser>> SearchUsersAsync(string searchTerm)
        {
            return await _userManager.Users
                .Where(u => u.Email.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<List<FriendRequest>> GetSentFriendRequestsAsync(string userId)
        {
            return await _context.FriendRequests
                .Where(fr => fr.SenderId == userId && !fr.IsAccepted)
                .Include(fr => fr.Receiver)
                .ToListAsync();
        }


        // New methods for friend request management
        public async Task SendFriendRequestAsync(string userId, string friendId)
        {
            var friendRequest = new FriendRequest
            {
                SenderId = userId,
                ReceiverId = friendId,
                IsAccepted = false
            };

            _context.FriendRequests.Add(friendRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FriendRequest>> GetFriendRequestsAsync(string userId)
        {
            return await _context.FriendRequests
                .Where(fr => fr.ReceiverId == userId && !fr.IsAccepted)
                .Include(fr => fr.Sender)
                .ToListAsync();
        }

        public async Task AcceptFriendRequestAsync(int requestId)
        {
            var friendRequest = await _context.FriendRequests.FindAsync(requestId);
            if (friendRequest != null)
            {
                friendRequest.IsAccepted = true;

                var friendship1 = new Friendship
                {
                    UserId = friendRequest.SenderId,
                    FriendId = friendRequest.ReceiverId
                };
                var friendship2 = new Friendship
                {
                    UserId = friendRequest.ReceiverId,
                    FriendId = friendRequest.SenderId
                };

                _context.Friendships.AddRange(friendship1, friendship2);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeclineFriendRequestAsync(int requestId)
        {
            var friendRequest = await _context.FriendRequests.FindAsync(requestId);
            if (friendRequest != null)
            {
                _context.FriendRequests.Remove(friendRequest);
                await _context.SaveChangesAsync();
            }
        }
    }
}

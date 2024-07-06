using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChiChat.Models;
using Microsoft.AspNetCore.Identity;

namespace ChiChat.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Further configuration
        }
    }
}

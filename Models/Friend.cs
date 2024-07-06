namespace ChiChat.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}

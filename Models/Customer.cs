namespace ChiChat.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsFriend { get; set; }
        public bool HasPendingRequest { get; set; }
    }
}

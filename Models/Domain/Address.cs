namespace OnlineShop.Models.Domain
{
    public class Address : Entity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}

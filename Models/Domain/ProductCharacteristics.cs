namespace OnlineShop.Models.Domain
{

    public class ProductCharacteristics : Entity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}

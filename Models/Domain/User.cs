using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineShop.Models.Domain
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ShoppingCart Cart { get; set; }
    }
}

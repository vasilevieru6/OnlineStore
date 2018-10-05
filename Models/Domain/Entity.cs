using System;

namespace OnlineShop.Models.Domain
{
    public abstract class Entity
    {
        public long Id { get; protected set; }
    }
}

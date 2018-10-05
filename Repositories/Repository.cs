using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using OnlineShop.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Repositories
{
    public class Repository : IRepository
    {
        protected readonly ShopDbContext context;

        public Repository(ShopDbContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : Entity
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public IQueryable<T> GetAll<T>() where T : Entity
        {
            return context.Set<T>();
        }

        public T GetById<T>(long id) where T : Entity
        {
            return context.Find<T>(id);
        }

        public void Update<T>(T entity) where T : Entity
        {
            context.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}

using OnlineShop.Models.Domain;
using System.Linq;

namespace OnlineShop.Repositories
{
    public interface IRepository
    {
        T GetById<T>(long id) where T : Entity;
        IQueryable<T> GetAll<T>() where T : Entity;
        void Add<T>(T entity) where T : Entity;
        void Update<T>(T entity) where T : Entity;
        void Delete<T>(T entity) where T : Entity;
    }
}

using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        public T Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public IEnumerable<T> GetAll();
        public T? GetById(int id);
    }
}

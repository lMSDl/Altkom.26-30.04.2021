using DataService.Interfaces;
using Models;
using Models.Base;
using System.Linq;
using System.Collections.Generic;

namespace DataService
{
    public class Service<T> : IService<T> where T : Entity
    {
        protected List<T> Entities { get; } = new List<T>();

        public T Create(T entity)
        {
            //var maxId = 0;
            //foreach (var item in Entities)
            //{
            //    if (maxId < item.Id)
            //        maxId = item.Id;
            //}

            if (Entities.Any())
            {       var maxId = Entities.Max(x => x.Id);
                    entity.Id = ++maxId;
            }
            else
            {
                entity.Id = 1;
            }
            Entities.Add(entity);

            return entity;
        }

        public void Delete(int id)
        {
            var entity = Read(id);
            if (entity == null)
                throw new KeyNotFoundException(id.ToString());
            Entities.Remove(entity);
        }

        public T Read(int id)
        {
            //foreach (var item in Entities)
            //{
            //    if (id == item.Id)
            //        return item;
            //}
            //return null;
            return Entities.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> Read()
        {
            //return new List<T>(Entities);
            return Entities.ToList();
        }

        public void Update(int id, T entity)
        {
            entity.Id = id;
            Delete(id);
            Entities.Add(entity);
        }
    }
}

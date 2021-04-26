using DataService.Interfaces;
using Models;
using Models.Base;
using System;
using System.Collections.Generic;

namespace DataService
{
    public class Service<T> : IService<T> where T : Entity
    {
        private List<T> Entities { get; } = new List<T>();

        public T Create(T entity)
        {
            var maxId = 0;
            foreach (var item in Entities)
            {
                if (maxId < item.Id)
                    maxId = item.Id;
            }
            entity.Id = ++maxId;
            Entities.Add(entity);

            return entity;
        }

        public void Delete(int id)
        {
            var entity = Read(id);
            if (entity == null)
                throw new KeyNotFoundException();
            Entities.Remove(entity);
        }

        public T Read(int id)
        {
            foreach (var item in Entities)
            {
                if (id == item.Id)
                    return item;
            }
            return null;
        }

        public IEnumerable<T> Read()
        {
            return new List<T>(Entities);
        }

        public void Update(int id, T entity)
        {
            entity.Id = id;
            Delete(id);
            Entities.Add(entity);
        }
    }
}

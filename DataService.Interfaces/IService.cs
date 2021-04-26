using Models;
using Models.Base;
using System;
using System.Collections.Generic;

namespace DataService.Interfaces
{
    public interface IService<T> where T : Entity
    {
        T Create(T student);
        T Read(int id);
        IEnumerable<T> Read();
        void Update(int id, T entity);
        void Delete(int id);
    }
}

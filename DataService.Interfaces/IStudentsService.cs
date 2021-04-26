using Models;
using System;
using System.Collections.Generic;

namespace DataService.Interfaces
{
    public interface IStudentsService
    {
        Student Create(Student student);
        Student Read(int id);
        IEnumerable<Student> Read();
        void Update(int id, Student entity);
        void Delete(int id);
    }
}

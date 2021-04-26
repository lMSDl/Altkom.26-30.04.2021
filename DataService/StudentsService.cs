using DataService.Interfaces;
using Models;
using System;
using System.Collections.Generic;

namespace DataService
{
    public class StudentsService : IStudentsService
    {
        private List<Student> Entities { get; } = new List<Student>();

        public Student Create(Student student)
        {
            var maxId = 0;
            foreach (var item in Entities)
            {
                if (maxId < item.Id)
                    maxId = item.Id;
            }
            student.Id = ++maxId;
            Entities.Add(student);

            return student;
        }

        public void Delete(int id)
        {
            var entity = Read(id);
            if (entity == null)
                throw new KeyNotFoundException();
            Entities.Remove(entity);
        }

        public Student Read(int id)
        {
            foreach (var item in Entities)
            {
                if (id == item.Id)
                    return item;
            }
            return null;
        }

        public IEnumerable<Student> Read()
        {
            return new List<Student>(Entities);
        }

        public void Update(int id, Student entity)
        {
            entity.Id = id;
            Delete(id);
            Entities.Add(entity);
        }
    }
}

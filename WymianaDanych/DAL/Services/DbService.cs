using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class DbService<T> where T : Entity
    {
        private DbContext _context;

        public DbService(DbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity.Id = 0;
            var entry = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task UpdateAsync(int id, T entity)
        {
            entity.Id = id;
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

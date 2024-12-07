using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DALImpliment
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        BE_102024Context _context;
        public GenericRepository(BE_102024Context context)
        {
            _context = context;
        }
        public async Task<int> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> Insert(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return _context.SaveChanges();
        }

        public async Task<int> Update(T entity)
        {
             _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}

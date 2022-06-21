using Market.Core.InterFaces.MarketInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Market.EF.Repostories.MarketRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
           
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteRange(IEnumerable<T> entities)
        {
            _context.RemoveRange(entities);
           await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRange(Expression<Func<T, bool>> match )
        {
             var  entities =  _context.Set<T>().Where(match);
            _context.RemoveRange(entities);
           await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match = null, string[] Includes = null)
        {
            IQueryable<T> query =  _context.Set<T>().AsNoTracking();

            if(Includes != null && match != null)
            { 
                foreach(var include in Includes)
                    query = query.Include(include);
                return query.Where(match).ToList();
            }
            if(Includes == null && match != null)
            {
                return query.Where(match).ToList();
            }
            if(Includes != null && match == null)
            {
                foreach (var include in Includes)
                    query = query.Include(include);
                return await query.ToListAsync();
            }
            return query.ToList();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> match = null, string[] Includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (Includes != null && match != null)
            {
                foreach (var include in Includes)
                    query = query.Include(include);
                return query.Where(match).FirstOrDefault();
            }
            if (Includes == null && match != null)
            {
                return query.Where(match).FirstOrDefault();
            }
            if (Includes != null && match == null)
            {
                foreach (var include in Includes)
                    query = query.Include(include);
                return  query.FirstOrDefault();
            }
            return query.FirstOrDefault();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Update(entity);
           await _context.SaveChangesAsync();
            return entity;
        }
    }
}

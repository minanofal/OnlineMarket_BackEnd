using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.InterFaces.MarketInterfaces
{
    public interface IBaseRepository <T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match = null, string[] Includes = null);
        Task<T> GetAsync(Expression<Func<T, bool>> match = null, string[] Includes = null);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteRange(IEnumerable<T> entities);
        Task<bool> DeleteRange(Expression<Func<T, bool>> match);
    }
}

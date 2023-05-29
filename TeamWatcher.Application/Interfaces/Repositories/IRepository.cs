using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWatcher.Application.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetSingleAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> CreateAsync(T entity);
        
    }
}

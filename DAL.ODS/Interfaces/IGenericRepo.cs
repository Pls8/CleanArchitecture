using DAL.ODS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ODS.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        ////Func delegate that represents a function


        Task<T> AddAsync(T entity); 
        //Task<T?> GetByIdAsync(int id);
        //Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        Task DeleteAsync(T entity);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        Task<int> CountAsync();
    }
}

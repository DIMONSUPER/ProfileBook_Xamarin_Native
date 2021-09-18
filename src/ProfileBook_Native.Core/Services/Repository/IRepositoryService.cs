using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProfileBook_Native.Core.Models;

namespace ProfileBook_Native.Core.Services.Repository
{
    public interface IRepositoryService
    {
        public interface IRepositoryService
        {
            Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new();
            Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate = null) where T : IEntityBase, new();
            Task<T> GetByIdAsync<T>(int id) where T : IEntityBase, new();
            Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : IEntityBase, new();
            Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new();
            Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new();
            Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new();
        }
    }
}

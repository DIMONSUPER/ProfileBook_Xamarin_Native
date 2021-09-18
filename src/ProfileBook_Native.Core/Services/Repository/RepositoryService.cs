using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProfileBook_Native.Core.Models;
using SQLite;

namespace ProfileBook_Native.Core.Services.Repository
{
    public class RepositoryService : IRepositoryService
    {
        private readonly SQLiteAsyncConnection _database;

        public RepositoryService()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _database = new SQLiteAsyncConnection(Path.Combine(path, Constants.DATABASE_NAME));
            InitTableAsync().Wait();
        }

        #region -- IRepositoryService implementation --

        public async Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new()
        {
            return await _database.DeleteAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate = null) where T : IEntityBase, new()
        {
            var table = _database.Table<T>();
            List<T> result = predicate == null ? await table.ToListAsync() : await table.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new()
        {
            var table = _database.Table<T>();
            List<T> result = await table.ToListAsync();
            return result;
        }

        public async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : IEntityBase, new()
        {
            return await _database.FindAsync(predicate);
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : IEntityBase, new()
        {
            return await _database.GetAsync<T>(id);
        }

        private async Task InitTableAsync()
        {
            await _database.CreateTableAsync<ProfileModel>();
            await _database.CreateTableAsync<UserModel>();
        }

        public async Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new()
        {
            int result = -1;
            try
            {
                result = entity.Id != 0 ? await UpdateAsync(entity) : await _database.InsertAsync(entity);
            }
            catch
            {
            }

            return result;
        }

        public async Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new()
        {
            return await _database.UpdateAsync(entity);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProfileBook_Native.Core.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace ProfileBook_Native.Core.Services.Repository
{
    public class RepositoryService : IRepositoryService
    {
        private Lazy<SQLiteAsyncConnection> _database;

        public static RepositoryService Instance { get; private set; }

        public RepositoryService()
        {
            InitDatabaseAndTables();

            Instance = this;
        }

        #region -- IRepositoryService implementation --

        public async Task DeleteAllAsync<T>()
            where T : class, IEntityBase, new()
        {
            await _database.Value.DropTableAsync<T>();
            await _database.Value.CreateTableAsync<T>();
        }

        public async Task<int> CountAsync<T>()
            where T : class, IEntityBase, new()
        {
            return await _database.Value.Table<T>().CountAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
            where T : class, IEntityBase, new()
        {
            return await _database.Value.GetAllWithChildrenAsync<T>();
        }

        public Task<T> GetSingleByIdAsync<T>(int id)
            where T : class, IEntityBase, new()
        {
            return GetSingleAsync<T>(x => x.Id == id);
        }

        public async Task<int> SaveAsync<T>(T entity)
            where T : class, IEntityBase, new()
        {
            var row = -1;

            if (entity is not null)
            {
                row = await _database.Value.InsertAsync(entity);
            }

            return row;
        }

        public async Task<int> SaveOrUpdateAsync<T>(T entity)
            where T : class, IEntityBase, new()
        {
            var row = -1;

            if (entity is not null)
            {
                var existEntity = await GetSingleAsync<T>(x => x.Id == entity.Id);

                if (existEntity is null)
                {
                    row = await _database.Value.InsertAsync(entity);
                }
                else
                {
                    entity.Id = existEntity.Id;
                    row = await _database.Value.UpdateAsync(entity);
                }
            }

            return row;
        }

        public async Task DeleteAsync<T>(T entity)
            where T : class, IEntityBase, new()
        {
            if (entity is not null)
            {
                await _database.Value.DeleteAsync(entity);
            }
        }

        public async Task SaveOrUpdateRangeAsync<T>(IEnumerable<T> entities)
            where T : class, IEntityBase, new()
        {
            if (entities is not null && entities.Any())
            {
                foreach (var entity in entities)
                {
                    await SaveOrUpdateAsync(entity);
                }
            }
        }

        public async Task DeleteAllAsync<T>(IEnumerable<T> entities)
            where T : class, IEntityBase, new()
        {
            if (entities is not null && entities.Any())
            {
                foreach (var entity in entities)
                {
                    await DeleteAsync(entity);
                }
            }
        }

        public async Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class, IEntityBase, new()
        {
            var allMatched = await FindWhereAsync(predicate);

            return allMatched?.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> FindWhereAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class, IEntityBase, new()
        {
            return await _database.Value.GetAllWithChildrenAsync(predicate);
        }

        public async Task DeleteWhereAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class, IEntityBase, new()
        {
            var result = await FindWhereAsync(predicate);

            await DeleteAllAsync(result);
        }

        #endregion

        #region -- Private Helpers --

        private void InitDatabaseAndTables()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.DATABASE_NAME));

                database.CreateTableAsync<UserModel>().Wait();
                database.CreateTableAsync<ProfileModel>().Wait();

                return database;
            });
        }

        #endregion
    }
}

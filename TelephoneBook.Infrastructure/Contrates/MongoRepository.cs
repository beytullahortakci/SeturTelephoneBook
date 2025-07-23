using MongoDB.Driver;
using TelephoneBook.Domain.Common;
using TelephoneBook.Infrastructure.Interfaces;

namespace TelephoneBook.Infrastructure.Contrates
{
    public class MongoRepository<T> : IRepository<T> where T : IBaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase database)
        {
            var collectionName = typeof(T).Name.ToLower();
            if (collectionName.EndsWith("ies"))
            {
                collectionName = collectionName.Substring(0, collectionName.Length - 3) + "y"; // örnek: categories → category
            }
            else if (collectionName.EndsWith("s"))
            {
                collectionName = collectionName.Substring(0, collectionName.Length - 1); // örnek: contacts → contact
            }
            else if (collectionName.EndsWith("es"))
            {
                collectionName = collectionName.Substring(0, collectionName.Length - 2); // örnek: addresses → address
            }

            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(FilterDefinition<T>.Empty).ToListAsync();
        }

        public async Task<IEnumerable<T>> FilterAsync(FilterDefinition<T> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<T> FilterFirstAsync(FilterDefinition<T> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(string id, T entity)
        {
            await _collection.ReplaceOneAsync(e => e.Id == id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(e => e.Id == id);
        }
    }
}

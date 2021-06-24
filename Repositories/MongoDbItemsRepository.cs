using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "Catalog";
        private const string CollectionName = "items";
        private readonly IMongoCollection<Item> _itemsCollection;

        private readonly FilterDefinitionBuilder<Item> filterrBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase db = mongoClient.GetDatabase(databaseName);
            _itemsCollection = db.GetCollection<Item>(CollectionName);
        }
        public async Task CreatItemAsync(Item item)
        {
            await _itemsCollection.InsertOneAsync(item);  
        }

        public async Task DeletItemAsync(Guid id)
        {
            var filter = filterrBuilder.Eq(Item=>Item.Id,id);
            await _itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterrBuilder.Eq(Item=>Item.Id,id);
            return await _itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterrBuilder.Eq(existingItem=>existingItem.Id,item.Id);
            await _itemsCollection.ReplaceOneAsync(filter,item);
        }
    }
}
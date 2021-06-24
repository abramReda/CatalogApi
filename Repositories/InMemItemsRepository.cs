using System;
using System.Collections.Generic;
using Catalog.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Repositories
{

    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        };


        public async Task< IEnumerable<Item> >  GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
           var i =  items.FirstOrDefault(x => x.Id == id);
           return await Task.FromResult(i);
        }

        public async Task CreatItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(existingItem=> existingItem.Id == item.Id);

            items[index] = item;
            await Task.CompletedTask;

        }

        public async Task DeletItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem=> existingItem.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;

        }
    }

}
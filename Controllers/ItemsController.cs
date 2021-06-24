using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    // Get / items
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync()).Select(i=>i.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);
            if(item is null)
                return NotFound();
            return Ok(item.AsDto());
        }

        // Post /items
        [HttpPost]
        public async Task< ActionResult<ItemDto> >CreatItemAsync(CreatItemDto itemDto)
        {
            Item item = new(){
                Id=Guid.NewGuid(),
                CreatedDate = DateTimeOffset.UtcNow,
                Price = itemDto.Price,
                Name=itemDto.Name
            };
            await repository.CreatItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync),new{id = item.Id},item.AsDto());
        }

        // Put /items/{id}
        [HttpPut("{id}")]
        public async Task< ActionResult > UpdateItemAsync(Guid id,UpdateItemDto itemDto)
        {
            var existingItem =await repository.GetItemAsync(id);
            if(existingItem is null) return NotFound();

            var updatedItem = existingItem with{
                Name = itemDto.Name,
                Price=itemDto.Price,
            };

            await repository.UpdateItemAsync(updatedItem);
            return NoContent();

        }

        // Delete items/{id}
        [HttpDelete("{id}")]
        public async Task< ActionResult > DeleteItemAsync(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);
            if(existingItem is null) return NotFound();

            await repository.DeletItemAsync(id);
            return NoContent();

        }

    }
    
}
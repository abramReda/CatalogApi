using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item i)
        {
            return new ItemDto{
                        Id=i.Id,
                        Price=i.Price,
                        Name=i.Name,
                        CreatedDate=i.CreatedDate
                    };            
        }
    }
}
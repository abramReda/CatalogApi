using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos
{
    public record CreatItemDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(0,10000)]
        public decimal Price{ get; init; } 
    }    
}
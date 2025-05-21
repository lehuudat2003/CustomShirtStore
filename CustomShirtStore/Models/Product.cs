using System.Collections.Generic;

namespace CustomShirtStore.Models
{
    public class Product
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal BasePrice { get; set; }

        public int Quantity { get; set; }

        public string? Color { get; set; }

        public string? ImageUrl { get; set; }

        public string? Category { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<CustomerDesign> CustomerDesigns { get; set; } = new List<CustomerDesign>();
    }
}

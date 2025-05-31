namespace CustomShirtStore.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? DesignImageUrl { get; set; }
        public string? QrCodeImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int GeneratedId { get; set; }
    }

}

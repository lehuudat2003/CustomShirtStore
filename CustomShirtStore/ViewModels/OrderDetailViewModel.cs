using CustomShirtStore.Models;

namespace CustomShirtStore.ViewModels
{
    public class OrderDetailViewModel
    {
        public long OrderId { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public string GuestPhoneNumber { get; set; }
        public string GuestAddress { get; set; }
        public string TotalAmountFormatted { get; set; }
        public string OrderStatus { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
        public class OrderItemViewModel
        {
            public long OrderItemId { get; set; }
            public string ProductName { get; set; }
            public string DesignImageUrl { get; set; } // From CustomerDesign or Product
            public int Quantity { get; set; } // Adjusted from long to int
            public string LinkMessage { get; set; }
            public string ItemPriceFormatted { get; set; }
            public string Size { get; set; }
        }
    }
}

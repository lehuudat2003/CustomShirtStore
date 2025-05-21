using CustomShirtStore.Models;

namespace CustomShirtStore.ViewModels
{
    public class ShirtDesignViewModel
    {
        public Product Product { get; set; }
        public List<string> AvailableColors { get; set; } = new();
        public List<string> Sizes { get; set; } = new() { "XS", "S", "M", "L", "XL", "XXL", "XXXL", "XXXXL", "XXXXXL" };
        public CustomerDesign Design { get; set; } = new();
    }

}

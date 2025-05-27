namespace CustomShirtStore.ViewModels
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public string GuestName { get; set; }
        public string GuestPhoneNumber { get; set; }
        public string GuestAddress { get; set; }
        public string TotalAmountFormatted { get; set; }
        public string OrderStatus { get; set; }
    }
}

namespace CustomShirtStore.Models
{
    public class GreetingCard
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string ImagePath { get; set; }
        public string NoteImagePath { get; set; }
        public string AudioPath { get; set; }
        public string VideoPath { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public long UserId { get; set; }
        public virtual UserAccount User { get; set; }
    }
}

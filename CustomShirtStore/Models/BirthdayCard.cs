using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomShirtStore.Models
{
    public class BirthdayCard
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên người nhận")]
        [MaxLength(255)]
        public string Recipient { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên người gửi")]
        [MaxLength(255)]
        public string Sender { get; set; }

        [MaxLength(1000)]
        public string Message1 { get; set; }

        [MaxLength(1000)]
        public string Message2 { get; set; }

        [MaxLength(1000)]
        public string Message3 { get; set; }

        [MaxLength(1000)]
        public string CenterMessage { get; set; }

        [MaxLength(255)]
        public string? ImagePath { get; set; }

        [MaxLength(255)]
        public string? BackgroundImagePath { get; set; }

        [MaxLength(255)]
        public string? NoteImagePath { get; set; }

        [MaxLength(255)]
        public string? AudioPath { get; set; }


        // File uploads - not mapped to DB
        [NotMapped]
        public IFormFile ImageUpload { get; set; }

        [NotMapped]
        public IFormFile BackgroundImageUpload { get; set; }

        [NotMapped]
        public IFormFile NoteImageUpload { get; set; }

        [NotMapped]
        public IFormFile AudioUpload { get; set; }
    }
}

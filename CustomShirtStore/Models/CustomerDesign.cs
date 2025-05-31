using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomShirtStore.Models;

public class CustomerDesign
{
    public long Id { get; set; }
    public string? UserId { get; set; }
    public long ProductId { get; set; }
    public string? DesignImageUrl { get; set; }
    public string? Color { get; set; }
    public string? Text { get; set; }
    public string? UploadedImagePath { get; set; }
    public string? Size { get; set; }
    public decimal Price { get; set; }
    public string? QrCodeImagePath { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual Product? Product { get; set; }
}





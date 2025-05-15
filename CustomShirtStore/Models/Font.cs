using System;
using System.Collections.Generic;

namespace CustomShirtStore.Models;

public partial class Font
{
    public long Id { get; set; }

    public string FontName { get; set; } = null!;

    public string FontUrl { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

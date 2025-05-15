using System;
using System.Collections.Generic;

namespace CustomShirtStore.Models;

public partial class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string GuestName { get; set; } = null!;

    public string GuestEmail { get; set; } = null!;

    public string GuestPhoneNumber { get; set; } = null!;

    public string GuestAddress { get; set; } = null!;

    public string OrderStatus { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual UserAccount User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace CustomShirtStore.Models;

public partial class UserAccount
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public long Address { get; set; }

    public string Provider { get; set; } = null!;

    public string ProviderId { get; set; } = null!;

    public long RoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;
}

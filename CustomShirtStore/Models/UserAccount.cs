using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace CustomShirtStore.Models;

public partial class UserAccount : IdentityUser<long>
{
    // Keep your existing properties
    // (Some of these will override IdentityUser base properties)
    // public long Id { get; set; } - This is already in IdentityUser<long> as Id
    // public string Email { get; set; } = null!; - Already in IdentityUser
    // public string PhoneNumber { get; set; } = null!; - Already in IdentityUser

    public string Password { get; set; }

    public long Address { get; set; }

    public string Provider { get; set; }

    public string ProviderId { get; set; }

    public long RoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsActive { get; set; }


    public virtual ICollection<GreetingCard> GreetingCards { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;
}

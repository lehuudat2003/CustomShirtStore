using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CustomShirtStore.Models;

public partial class Role : IdentityRole<long>
{
    // public long Id { get; set; } - Already in IdentityRole<long>
    // public string Name { get; set; } = null!; - Already in IdentityRole as Name

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}

using System;
using System.Collections.Generic;

namespace CustomShirtStore.Models;

public partial class CustomerDesign
{
    public long Id { get; set; }

    public string BackDesign { get; set; } = null!;

    public string FrontDesign { get; set; } = null!;

    public long OrderItemId { get; set; }

    public virtual OrderItem OrderItem { get; set; } = null!;
}

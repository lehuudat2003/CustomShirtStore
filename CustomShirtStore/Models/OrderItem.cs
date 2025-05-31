using System;
using System.Collections.Generic;

namespace CustomShirtStore.Models;

public partial class OrderItem
{
    public long Id { get; set; }

    public string MaleName { get; set; } = null!;

    public string FemaleName { get; set; } = null!;

    public string MessageTitle { get; set; } = null!;

    public string MsgContent { get; set; } = null!;

    public string MsgImageUrl { get; set; } = null!;

    public string MsgAudioUrl { get; set; } = null!;

    public decimal ItemPrice { get; set; }

    public long OrderId { get; set; }

    public long? FontId { get; set; }

    public long Quantity { get; set; }

    public string Template { get; set; } = null!;

    public string Size { get; set; } = null!;

    public virtual ICollection<CustomerDesign> CustomerDesigns { get; set; } = new List<CustomerDesign>();

    public virtual Font Font { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}

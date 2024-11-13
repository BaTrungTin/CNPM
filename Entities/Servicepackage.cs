using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class Servicepackage
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public string? Description { get; set; }

    public string ShippingType { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public sbyte? Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Pricelist> Pricelists { get; set; } = new List<Pricelist>();
}

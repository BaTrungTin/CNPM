using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class Pricelist
{
    public int PriceId { get; set; }

    public int? ServicePackageId { get; set; }

    public string WeightRange { get; set; } = null!;

    public string Distance { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime EffectiveDate { get; set; }

    public DateTime? EndDate { get; set; }

    public sbyte? Status { get; set; }

    public virtual Servicepackage? ServicePackage { get; set; }
}

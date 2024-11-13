using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class Country
{
    public int CountryId { get; set; }

    public string CountryCode { get; set; } = null!;

    public string CountryName { get; set; } = null!;

    public sbyte? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Province> Provinces { get; set; } = new List<Province>();
}

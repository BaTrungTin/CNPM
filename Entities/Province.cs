using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class Province
{
    public int ProvinceId { get; set; }

    public int CountryId { get; set; }

    public string ProvinceCode { get; set; } = null!;

    public string ProvinceName { get; set; } = null!;

    public sbyte? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<District> Districts { get; set; } = new List<District>();
}

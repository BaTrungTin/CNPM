using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class District
{
    public int DistrictId { get; set; }

    public int ProvinceId { get; set; }

    public string DistrictCode { get; set; } = null!;

    public string DistrictName { get; set; } = null!;

    public sbyte? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Province Province { get; set; } = null!;

    public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
}

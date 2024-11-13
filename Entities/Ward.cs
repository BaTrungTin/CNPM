using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class Ward
{
    public int WardId { get; set; }

    public int DistrictId { get; set; }

    public string WardCode { get; set; } = null!;

    public string WardName { get; set; } = null!;

    public sbyte? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual District District { get; set; } = null!;
}

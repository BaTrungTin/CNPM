using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class Customersupport
{
    public int SupportId { get; set; }

    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public string IssueType { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public int? HandledBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ResolvedDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}

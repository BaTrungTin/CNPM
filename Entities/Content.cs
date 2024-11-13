using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class Content
{
    public int ContentId { get; set; }

    public string Title { get; set; } = null!;

    public string Content1 { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Status { get; set; }

    public string Author { get; set; } = null!;

    public DateTime? PublishDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}

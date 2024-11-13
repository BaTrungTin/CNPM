using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class Report
{
    public int ReportId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? ReportData { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }
}

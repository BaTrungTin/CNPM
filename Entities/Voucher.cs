using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public string VoucherCode { get; set; } = null!;

    public string DiscountType { get; set; } = null!;

    public decimal DiscountValue { get; set; }

    public decimal? MinimumOrderValue { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public sbyte? Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

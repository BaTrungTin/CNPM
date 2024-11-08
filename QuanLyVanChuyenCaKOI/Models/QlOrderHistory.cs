using System;
using System.Collections.Generic;

namespace quanlyvanchuyencakoi.web3.Models;

public partial class QlOrderHistory
{
    public int IdOrderHistory { get; set; }

    public string IdUser { get; set; } = null!;

    public string IdProduct { get; set; } = null!;

    public string NameProduct { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public string StatusProduct { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace quanlyvanchuyencakoi.web3.Models;

public partial class QlProduct
{
    public int IdProduct { get; set; }

    public string NameProduct { get; set; } = null!;

    public string Note { get; set; } = null!;

    public string Price { get; set; } = null!;

    public string ImgProduct { get; set; } = null!;
}

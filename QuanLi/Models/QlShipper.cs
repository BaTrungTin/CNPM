using System;
using System.Collections.Generic;

namespace quanlyvanchuyencakoi.web3.Models;

public partial class QlShipper
{
    public int IdShipper { get; set; }

    public string NameShipper { get; set; } = null!;

    public string PhoneShipper { get; set; } = null!;

    public byte[] ImgShipper { get; set; } = null!;
}

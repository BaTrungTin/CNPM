using System;
using System.Collections.Generic;

namespace quanlyvanchuyencakoi.web3.Models;

public partial class QlFeedback
{
    public int IdFeedBack { get; set; }

    public string IdUser { get; set; } = null!;

    public string IdProduct { get; set; } = null!;

    public string? Coment { get; set; }

    public DateTime Date { get; set; }

    public int? ProductReview { get; set; }
}

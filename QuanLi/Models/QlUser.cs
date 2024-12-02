using System;
using System.Collections.Generic;

namespace quanlyvanchuyencakoi.web3.Models;

public partial class QlUser
{
    public int IdUser { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Access { get; set; } = null!;
}

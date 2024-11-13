using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class Admin
{
    public int StaffId { get; set; }

    public int AccountId { get; set; }

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public sbyte? Status { get; set; }

    public virtual Account Account { get; set; } = null!;
}

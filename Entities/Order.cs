using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public decimal Weight { get; set; }

    public int Quantity { get; set; }

    public string? FishStatus { get; set; }

    public int CustomerId { get; set; }

    public int ServicePackageId { get; set; }

    public int? VoucherId { get; set; }

    public int? DriverId { get; set; }

    public string? DeliveryVehicle { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public DateTime? PickupDate { get; set; }

    public string? OrderStatus { get; set; }

    public string? Note { get; set; }

    public string? DeliveryAddress { get; set; }

    public string? PickupAddress { get; set; }

    public decimal TotalPrice { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Customersupport> Customersupports { get; set; } = new List<Customersupport>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Servicepackage ServicePackage { get; set; } = null!;

    public virtual Voucher? Voucher { get; set; }
}

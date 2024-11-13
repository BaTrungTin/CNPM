using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace KoiDeliveryOrdering.Repositories.Entities;

public partial class KoiDeliveryOrderingContext : DbContext
{
    public KoiDeliveryOrderingContext()
    {
    }

    public KoiDeliveryOrderingContext(DbContextOptions<KoiDeliveryOrderingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customersupport> Customersupports { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Pricelist> Pricelists { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Servicepackage> Servicepackages { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=KoiDeliveryOrdering;user=root;password=Nguynn-NgothonGlucose", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.ToTable("account");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.Username, "Username").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AccountStatus).HasDefaultValueSql("'1'");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasDefaultValueSql("'Customer'")
                .HasColumnType("enum('Customer','Admin')");
            entity.Property(e => e.UpdatedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PRIMARY");

            entity.ToTable("admin");

            entity.HasIndex(e => e.AccountId, "AccountID");

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(45);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.Account).WithMany(p => p.Admins)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("admin_ibfk_1");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PRIMARY");

            entity.ToTable("content");

            entity.Property(e => e.ContentId).HasColumnName("ContentID");
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Content1)
                .HasColumnType("text")
                .HasColumnName("Content");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Draft'")
                .HasColumnType("enum('Draft','Published','Archived')");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type).HasColumnType("enum('Blog','News','FAQ','Policy','Service','About')");
            entity.Property(e => e.UpdatedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PRIMARY");

            entity.ToTable("country");

            entity.HasIndex(e => e.CountryCode, "CountryCode").IsUnique();

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryCode).HasMaxLength(3);
            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");
            entity.Property(e => e.UpdatedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.HasIndex(e => e.AccountId, "AccountID");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Fullname).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(15);

            entity.HasOne(d => d.Account).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("customer_ibfk_1");
        });

        modelBuilder.Entity<Customersupport>(entity =>
        {
            entity.HasKey(e => e.SupportId).HasName("PRIMARY");

            entity.ToTable("customersupport");

            entity.HasIndex(e => e.CustomerId, "CustomerID");

            entity.HasIndex(e => e.OrderId, "OrderID");

            entity.Property(e => e.SupportId).HasColumnName("SupportID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IssueType).HasMaxLength(100);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ResolvedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Open'")
                .HasColumnType("enum('Open','InProgress','Resolved','Closed')");

            entity.HasOne(d => d.Customer).WithMany(p => p.Customersupports)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customersupport_ibfk_2");

            entity.HasOne(d => d.Order).WithMany(p => p.Customersupports)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customersupport_ibfk_1");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("PRIMARY");

            entity.ToTable("district");

            entity.HasIndex(e => e.DistrictCode, "DistrictCode").IsUnique();

            entity.HasIndex(e => e.ProvinceId, "ProvinceID");

            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.DistrictCode).HasMaxLength(5);
            entity.Property(e => e.DistrictName).HasMaxLength(100);
            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");
            entity.Property(e => e.UpdatedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");

            entity.HasOne(d => d.Province).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("district_ibfk_1");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedBackId).HasName("PRIMARY");

            entity.ToTable("feedback");

            entity.HasIndex(e => e.CustomerId, "CustomerID");

            entity.HasIndex(e => e.OrderId, "OrderID");

            entity.Property(e => e.FeedBackId).HasColumnName("FeedBackID");
            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.CommentId)
                .HasMaxLength(50)
                .HasColumnName("CommentID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("feedback_ibfk_1");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("feedback_ibfk_2");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.CustomerId, "CustomerID");

            entity.HasIndex(e => e.ServicePackageId, "ServicePackageID");

            entity.HasIndex(e => e.VoucherId, "VoucherID");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DeliveryAddress).HasMaxLength(255);
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.DeliveryVehicle).HasMaxLength(45);
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.OrderStatus)
                .HasDefaultValueSql("'Pending'")
                .HasColumnType("enum('Pending','Confirmed','PickingUp','InTransit','Delivered','Cancelled')");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PickupAddress).HasMaxLength(255);
            entity.Property(e => e.PickupDate).HasColumnType("datetime");
            entity.Property(e => e.ServicePackageId).HasColumnName("ServicePackageID");
            entity.Property(e => e.TotalPrice).HasPrecision(10, 2);
            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
            entity.Property(e => e.Weight).HasPrecision(10, 2);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_ibfk_1");

            entity.HasOne(d => d.ServicePackage).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ServicePackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_ibfk_3");

            entity.HasOne(d => d.Voucher).WithMany(p => p.Orders)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("orders_ibfk_2");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PRIMARY");

            entity.ToTable("payment");

            entity.HasIndex(e => e.OrderId, "OrderID");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasColumnType("enum('CreditCard','DebitCard','PayPal','CashOnDelivery','BankTransfer')");
            entity.Property(e => e.PaymentStatus)
                .HasDefaultValueSql("'Pending'")
                .HasColumnType("enum('Pending','Completed','Failed','Refunded')");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_ibfk_1");
        });

        modelBuilder.Entity<Pricelist>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PRIMARY");

            entity.ToTable("pricelist");

            entity.HasIndex(e => e.ServicePackageId, "ServicePackageID");

            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.Distance).HasMaxLength(50);
            entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.ServicePackageId).HasColumnName("ServicePackageID");
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");
            entity.Property(e => e.WeightRange).HasMaxLength(50);

            entity.HasOne(d => d.ServicePackage).WithMany(p => p.Pricelists)
                .HasForeignKey(d => d.ServicePackageId)
                .HasConstraintName("pricelist_ibfk_1");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.ProvinceId).HasName("PRIMARY");

            entity.ToTable("province");

            entity.HasIndex(e => e.CountryId, "CountryID");

            entity.HasIndex(e => e.ProvinceCode, "ProvinceCode").IsUnique();

            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ProvinceCode).HasMaxLength(5);
            entity.Property(e => e.ProvinceName).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");
            entity.Property(e => e.UpdatedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");

            entity.HasOne(d => d.Country).WithMany(p => p.Provinces)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("province_ibfk_1");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PRIMARY");

            entity.ToTable("report");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ReportData).HasColumnType("json");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Servicepackage>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PRIMARY");

            entity.ToTable("servicepackages");

            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.BasePrice).HasPrecision(10, 2);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.PackageName).HasMaxLength(100);
            entity.Property(e => e.ShippingType).HasColumnType("enum('Domestic','International')");
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PRIMARY");

            entity.ToTable("voucher");

            entity.HasIndex(e => e.VoucherCode, "VoucherCode").IsUnique();

            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
            entity.Property(e => e.DiscountType).HasColumnType("enum('Percentage','Fixed')");
            entity.Property(e => e.DiscountValue).HasPrecision(10, 2);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.MinimumOrderValue).HasPrecision(10, 2);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");
            entity.Property(e => e.VoucherCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.WardId).HasName("PRIMARY");

            entity.ToTable("ward");

            entity.HasIndex(e => e.DistrictId, "DistrictID");

            entity.HasIndex(e => e.WardCode, "WardCode").IsUnique();

            entity.Property(e => e.WardId).HasColumnName("WardID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");
            entity.Property(e => e.UpdatedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.WardCode).HasMaxLength(5);
            entity.Property(e => e.WardName).HasMaxLength(100);

            entity.HasOne(d => d.District).WithMany(p => p.Wards)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ward_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace quanlyvanchuyencakoi.web3.Models;

public partial class QuanlyContext : DbContext
{
    public QuanlyContext()
    {
    }

    public QuanlyContext(DbContextOptions<QuanlyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<QlFeedback> Feedbacks { get; set; }

    public virtual DbSet<QlOrderHistory> OrderHistories { get; set; }

    public virtual DbSet<QlProduct> Products { get; set; }

    public virtual DbSet<QlShipper> Shippers { get; set; }

    public virtual DbSet<QlUser> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=quanly;user=root;password=ABC123abc", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.1.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<QlFeedback>(entity =>
        {
            entity.HasKey(e => new { e.IdFeedBack, e.Date })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("feedback");

            entity.Property(e => e.IdFeedBack)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_Feed_Back");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Coment).HasMaxLength(45);
            entity.Property(e => e.IdProduct)
                .HasMaxLength(45)
                .HasColumnName("Id_Product");
            entity.Property(e => e.IdUser)
                .HasMaxLength(45)
                .HasColumnName("Id_User");
            entity.Property(e => e.ProductReview).HasColumnName("Product_Review");
        });

        modelBuilder.Entity<QlOrderHistory>(entity =>
        {
            entity.HasKey(e => e.IdOrderHistory).HasName("PRIMARY");

            entity.ToTable("order_history");

            entity.Property(e => e.IdOrderHistory).HasColumnName("Id_Order_History");
            entity.Property(e => e.IdProduct)
                .HasMaxLength(45)
                .HasColumnName("Id_product");
            entity.Property(e => e.IdUser)
                .HasMaxLength(45)
                .HasColumnName("Id_User");
            entity.Property(e => e.NameProduct)
                .HasMaxLength(45)
                .HasColumnName("Name_product");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(45)
                .HasDefaultValueSql("'GHTK'")
                .HasColumnName("Payment_Method");
            entity.Property(e => e.StatusProduct)
                .HasMaxLength(45)
                .HasColumnName("Status_product");
            entity.Property(e => e.TotalPrice)
                .HasPrecision(10)
                .HasColumnName("Total_price");
        });

        modelBuilder.Entity<QlProduct>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PRIMARY");

            entity.ToTable("product");

            entity.Property(e => e.IdProduct)
                .ValueGeneratedNever()
                .HasColumnName("id_Product");
            entity.Property(e => e.ImgProduct).HasColumnName("Img_product");
            entity.Property(e => e.NameProduct)
                .HasMaxLength(45)
                .HasColumnName("Name_product");
            entity.Property(e => e.Note)
                .HasMaxLength(45)
                .HasColumnName("note");
            entity.Property(e => e.Price).HasMaxLength(45);
        });

        modelBuilder.Entity<QlShipper>(entity =>
        {
            entity.HasKey(e => e.IdShipper).HasName("PRIMARY");

            entity.ToTable("shipper");

            entity.Property(e => e.IdShipper).HasColumnName("id_shipper");
            entity.Property(e => e.ImgShipper).HasColumnName("img_shipper");
            entity.Property(e => e.NameShipper)
                .HasMaxLength(45)
                .HasColumnName("name_shipper");
            entity.Property(e => e.PhoneShipper)
                .HasMaxLength(45)
                .HasColumnName("phone_shipper");
        });

        modelBuilder.Entity<QlUser>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Password, "Password_UNIQUE").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("Id_User");
            entity.Property(e => e.Access)
                .HasMaxLength(45)
                .HasDefaultValueSql("'user'");
            entity.Property(e => e.Address).HasMaxLength(45);
            entity.Property(e => e.Email).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(45)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.UserName)
                .HasMaxLength(45)
                .HasColumnName("User_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

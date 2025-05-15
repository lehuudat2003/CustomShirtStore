using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomShirtStore.Models;

public partial class Exe201Context : DbContext
{
    public Exe201Context()
    {
    }

    public Exe201Context(DbContextOptions<Exe201Context> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerDesign> CustomerDesigns { get; set; }

    public virtual DbSet<Font> Fonts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        if (!optionsBuilder.IsConfigured) { optionsBuilder.UseSqlServer(config.GetConnectionString("value")); }

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerDesign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customerdesign_id_primary");

            entity.ToTable("CustomerDesign");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BackDesign)
                .HasMaxLength(255)
                .HasColumnName("backDesign");
            entity.Property(e => e.FrontDesign)
                .HasMaxLength(255)
                .HasColumnName("frontDesign");
            entity.Property(e => e.OrderItemId).HasColumnName("orderItemId");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.CustomerDesigns)
                .HasForeignKey(d => d.OrderItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customerdesign_orderitemid_foreign");
        });

        modelBuilder.Entity<Font>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("fonts_id_primary");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdAt");
            entity.Property(e => e.FontName)
                .HasMaxLength(255)
                .HasColumnName("fontName");
            entity.Property(e => e.FontUrl)
                .HasMaxLength(255)
                .HasColumnName("fontUrl");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_id_primary");

            entity.ToTable("Order");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdAt");
            entity.Property(e => e.GuestAddress)
                .HasMaxLength(255)
                .HasColumnName("guestAddress");
            entity.Property(e => e.GuestEmail)
                .HasMaxLength(255)
                .HasColumnName("guestEmail");
            entity.Property(e => e.GuestName)
                .HasMaxLength(255)
                .HasColumnName("guestName");
            entity.Property(e => e.GuestPhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("guestPhoneNumber");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(255)
                .HasDefaultValue("Pending")
                .HasColumnName("orderStatus");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("totalAmount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_userid_foreign");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderitem_id_primary");

            entity.ToTable("OrderItem");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FemaleName)
                .HasMaxLength(255)
                .HasColumnName("femaleName");
            entity.Property(e => e.FontId).HasColumnName("fontId");
            entity.Property(e => e.ItemPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("itemPrice");
            entity.Property(e => e.MaleName)
                .HasMaxLength(255)
                .HasColumnName("maleName");
            entity.Property(e => e.MessageTitle)
                .HasMaxLength(255)
                .HasColumnName("messageTitle");
            entity.Property(e => e.MsgAudioUrl)
                .HasMaxLength(255)
                .HasColumnName("msgAudioUrl");
            entity.Property(e => e.MsgContent)
                .HasMaxLength(255)
                .HasColumnName("msgContent");
            entity.Property(e => e.MsgImageUrl)
                .HasMaxLength(255)
                .HasColumnName("msgImageUrl");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Size)
                .HasMaxLength(255)
                .HasColumnName("size");
            entity.Property(e => e.Template)
                .HasMaxLength(255)
                .HasColumnName("template");

            entity.HasOne(d => d.Font).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.FontId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitem_fontid_foreign");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitem_orderid_foreign");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_id_primary");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("useraccount_id_primary");

            entity.ToTable("UserAccount");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Provider)
                .HasMaxLength(255)
                .HasColumnName("provider");
            entity.Property(e => e.ProviderId)
                .HasMaxLength(255)
                .HasColumnName("providerId");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Role).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("useraccount_role_id_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

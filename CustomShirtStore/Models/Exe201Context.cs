using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomShirtStore.Models;

public partial class Exe201Context : IdentityDbContext<
    UserAccount, Role, long,
    IdentityUserClaim<long>,
    IdentityUserRole<long>,
    IdentityUserLogin<long>,
    IdentityRoleClaim<long>,
    IdentityUserToken<long>>
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

    public virtual DbSet<Product> Products { get; set; }

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
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("userId");

            entity.Property(e => e.ProductId)
                .HasColumnName("productId");

            entity.Property(e => e.DesignImageUrl)
                .HasMaxLength(255)
                .HasColumnName("designImageUrl");

            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");

            entity.Property(e => e.Text)
                .HasMaxLength(500)
                .HasColumnName("text");

            entity.Property(e => e.UploadedImagePath)
                .HasMaxLength(255)
                .HasColumnName("uploadedImagePath");

            entity.Property(e => e.Size)
                .HasMaxLength(20)
                .HasColumnName("size");

            entity.Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("price");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.CustomerDesigns)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customerdesign_productid_foreign");
        });



        modelBuilder.Entity<Font>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("fonts_id_primary");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
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
                .ValueGeneratedOnAdd()
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
                .ValueGeneratedOnAdd()
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

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("product_id_primary");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("productId");

            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("productName");

            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");

            entity.Property(e => e.BasePrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("basePrice");

            entity.Property(e => e.Quantity)
                .HasColumnName("quantity");

            entity.Property(e => e.Color)
                .HasMaxLength(100)
                .HasColumnName("color");

            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("imageUrl");

            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");

            entity.Property(e => e.IsActive)
                .HasColumnName("isActive");
        });


        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.HasKey(e => e.Id).HasName("role_id_primary");


            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.NormalizedName)
                .HasMaxLength(255)
                .HasColumnName("normalized_name");
            entity.Property(e => e.ConcurrencyStamp)
                .HasColumnName("concurrency_stamp");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.ToTable("UserAccount");

            entity.HasKey(e => e.Id).HasName("useraccount_id_primary");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
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
            // Add Identity specific properties
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("user_name");

            entity.Property(e => e.NormalizedUserName)
                .HasMaxLength(255)
                .HasColumnName("normalized_user_name");

            entity.Property(e => e.NormalizedEmail)
                .HasMaxLength(255)
                .HasColumnName("normalized_email");

            entity.Property(e => e.EmailConfirmed)
                .HasColumnName("email_confirmed")
                .HasDefaultValue(false);

            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");

            entity.Property(e => e.SecurityStamp)
                .HasMaxLength(255)
                .HasColumnName("security_stamp");

            entity.Property(e => e.ConcurrencyStamp)
                .HasMaxLength(255)
                .HasColumnName("concurrency_stamp");

            entity.Property(e => e.PhoneNumberConfirmed)
                .HasColumnName("phone_number_confirmed")
                .HasDefaultValue(false);

            entity.Property(e => e.TwoFactorEnabled)
                .HasColumnName("two_factor_enabled")
                .HasDefaultValue(false);

            entity.Property(e => e.LockoutEnd)
                .HasColumnName("lockout_end");

            entity.Property(e => e.LockoutEnabled)
                .HasColumnName("lockout_enabled")
                .HasDefaultValue(false);

            entity.Property(e => e.AccessFailedCount)
                .HasColumnName("access_failed_count")
                .HasDefaultValue(0);
            entity.HasOne(d => d.Role).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("useraccount_role_id_foreign");
        });
         // Configure Identity related tables
        modelBuilder.Entity<IdentityUserClaim<long>>(entity =>
        {
            entity.ToTable("UserClaims");
            
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ClaimType).HasColumnName("claim_type");
            entity.Property(e => e.ClaimValue).HasColumnName("claim_value");
        });

        modelBuilder.Entity<IdentityUserRole<long>>(entity =>
        {
            entity.ToTable("UserRoles");
            
            entity.HasKey(e => new { e.UserId, e.RoleId });
            
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
        });

        modelBuilder.Entity<IdentityUserLogin<long>>(entity =>
        {
            entity.ToTable("UserLogins");
            
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            
            entity.Property(e => e.LoginProvider).HasColumnName("login_provider");
            entity.Property(e => e.ProviderKey).HasColumnName("provider_key");
            entity.Property(e => e.ProviderDisplayName).HasColumnName("provider_display_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<IdentityRoleClaim<long>>(entity =>
        {
            entity.ToTable("RoleClaims");
            
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.ClaimType).HasColumnName("claim_type");
            entity.Property(e => e.ClaimValue).HasColumnName("claim_value");
        });

        modelBuilder.Entity<IdentityUserToken<long>>(entity =>
        {
            entity.ToTable("UserTokens");
            
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.LoginProvider).HasColumnName("login_provider");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Value).HasColumnName("value");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

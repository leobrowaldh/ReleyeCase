using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReleyeCase.Data.DbModels;

namespace ReleyeCase.Data;

public partial class ReleyeDbContext : DbContext
{
    public ReleyeDbContext(DbContextOptions<ReleyeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2A1B7C8CE0A4");

            entity.Property(e => e.AddressId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B82741FDB1");

            entity.Property(e => e.CustomerId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Address).WithMany(p => p.Customers).HasConstraintName("FK_Customer_Address");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

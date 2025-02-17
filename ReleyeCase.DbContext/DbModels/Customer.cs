using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ReleyeCase.Data.DbModels;

[Index("Email", Name = "UQ__Customer__A9D105340E2AF804", IsUnique = true)]
public partial class Customer
{
    [Key]
    [Column("CustomerID")]
    public Guid CustomerId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("AddressID")]
    public Guid? AddressId { get; set; }

    [ForeignKey("AddressId")]
    [InverseProperty("Customers")]
    public virtual Address? Address { get; set; } = new Address();
}

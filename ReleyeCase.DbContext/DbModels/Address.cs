using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ReleyeCase.Data.DbModels;

public partial class Address
{
    [Key]
    [Column("AddressID")]
    public Guid AddressId { get; set; }

    [StringLength(50)]
    public string Country { get; set; } = null!;

    [StringLength(50)]
    public string City { get; set; } = null!;

    [StringLength(100)]
    public string? Street { get; set; }

    [StringLength(20)]
    public string? ZipCode { get; set; }

    [InverseProperty("Address")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}

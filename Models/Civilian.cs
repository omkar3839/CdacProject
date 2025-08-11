using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EPoliceConnectAPI.Models;

[Table("Civilian")]
public partial class Civilian
{
    [Key]
    [Column("civilian_id")]
    public int CivilianId { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("email")]
    [StringLength(100)]
    [RegularExpression(@"^[^@\s]+@(yahoo\.com|gmail\.com)$",
    ErrorMessage = "Email must be a valid Yahoo or Gmail address.")]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("phone")]
    [RegularExpression(@"^(?!00)\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits long and cannot start with '00'.")]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("address")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Address { get; set; }

    [Column("password")]
    [StringLength(100)]
    [Unicode(false)]
    [RegularExpression(
    @"^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()\-_+=])([a-zA-Z0-9!@#$%^&*()\-_+=]{8,25})$",
    ErrorMessage = "Password must be 8-25 characters long, contain at least one uppercase letter, one number, and one special character.")]
    public string? Password { get; set; }

    [InverseProperty("Civilian")]
    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    //virtaul keyword is for lazy loading
    //which allows related data to be loaded automatically when it's accessed.
}

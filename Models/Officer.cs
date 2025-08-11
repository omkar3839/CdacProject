using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EPoliceConnectAPI.Models;

[Table("Officer")]
public partial class Officer
{
    [Key]
    [Column("officer_id")]
    public int OfficerId { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("rank")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Rank { get; set; }

    [Column("email")]
    [StringLength(100)]
    [RegularExpression(@"^[^@\s]+@(yahoo\.com|gmail\.com|maha.gov.in)$",
    ErrorMessage = "Email must be a valid Yahoo, Gmail, or maha.gov.in address")]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("phone")]
    [RegularExpression(@"^(?!00)\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits long and cannot start with '00'.")]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("password")]
    [StringLength(100)]
    [Unicode(false)]
    [RegularExpression(
    @"^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()\-_+=])([a-zA-Z0-9!@#$%^&*()\-_+=]{8,25})$",
    ErrorMessage = "Password must be 8-25 characters long, contain at least one uppercase letter, one number, and one special character.")]
    public string? Password { get; set; }

    [Column("is_designated")]
    public bool? IsDesignated { get; set; }

    [InverseProperty("Officer")]
    public virtual ICollection<Criminal> Criminals { get; set; } = new List<Criminal>();

    [InverseProperty("Officer")]
    public virtual ICollection<IncidentReport> IncidentReports { get; set; } = new List<IncidentReport>();
}

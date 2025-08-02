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
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("phone")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("password")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column("is_designated")]
    public bool? IsDesignated { get; set; }

    [InverseProperty("Officer")]
    public virtual ICollection<Criminal> Criminals { get; set; } = new List<Criminal>();

    [InverseProperty("Officer")]
    public virtual ICollection<IncidentReport> IncidentReports { get; set; } = new List<IncidentReport>();
}

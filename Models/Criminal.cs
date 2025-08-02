using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EPoliceConnectAPI.Models;

[Table("Criminal")]
public partial class Criminal
{
    [Key]
    [Column("criminal_id")]
    public int CriminalId { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("age")]
    public int? Age { get; set; }

    [Column("gender")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Gender { get; set; }

    [Column("address")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Address { get; set; }

    [Column("crime_committed", TypeName = "text")]
    public string? CrimeCommitted { get; set; }

    [Column("arrest_date")]
    public DateOnly? ArrestDate { get; set; }

    [Column("officer_id")]
    public int? OfficerId { get; set; }

    [ForeignKey("OfficerId")]
    [InverseProperty("Criminals")]
    public virtual Officer? Officer { get; set; }

    [InverseProperty("Criminal")]
    public virtual ICollection<PrisonRecord> PrisonRecords { get; set; } = new List<PrisonRecord>();
}

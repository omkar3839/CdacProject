using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EPoliceConnectAPI.Models;

[Table("PrisonRecord")]
public partial class PrisonRecord
{
    [Key]
    [Column("prison_id")]
    public int PrisonId { get; set; }

    [Column("criminal_id")]
    public int? CriminalId { get; set; }

    [Column("prison_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PrisonName { get; set; }

    [Column("sentence_years")]
    public int? SentenceYears { get; set; }

    [Column("release_date")]
    public DateOnly? ReleaseDate { get; set; }

    [ForeignKey("CriminalId")]
    [InverseProperty("PrisonRecords")]
    public virtual Criminal? Criminal { get; set; }
}

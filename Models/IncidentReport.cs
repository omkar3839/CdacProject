using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EPoliceConnectAPI.Models;

[Table("IncidentReport")]
public partial class IncidentReport
{
    [Key]
    [Column("report_id")]
    public int ReportId { get; set; }

    [Column("officer_id")]
    public int? OfficerId { get; set; }

    [Column("location")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Location { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("report_date")]
    public DateOnly? ReportDate { get; set; }

    [ForeignKey("OfficerId")]
    [InverseProperty("IncidentReports")]
    public virtual Officer? Officer { get; set; }
}

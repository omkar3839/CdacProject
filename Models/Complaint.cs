using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EPoliceConnectAPI.Models;

[Table("Complaint")]
public partial class Complaint
{
    [Key]
    [Column("complaint_id")]
    public int ComplaintId { get; set; }

    [Column("civilian_id")]
    public int? CivilianId { get; set; }

    [Column("date_filed")]
    public DateTime? DateFiled { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("status")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Status { get; set; }

    [ForeignKey("CivilianId")]
    [InverseProperty("Complaints")]
    public virtual Civilian? Civilian { get; set; }
}

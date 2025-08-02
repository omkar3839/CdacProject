using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EPoliceConnectAPI.Models;

public partial class EPoliceDbContext : DbContext
{
    public EPoliceDbContext()
    {
    }

    public EPoliceDbContext(DbContextOptions<EPoliceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Civilian> Civilians { get; set; }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<Criminal> Criminals { get; set; }

    public virtual DbSet<IncidentReport> IncidentReports { get; set; }

    public virtual DbSet<Officer> Officers { get; set; }

    public virtual DbSet<PrisonRecord> PrisonRecords { get; set; }

   // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=LAPTOP-8TH34F54\\SQLEXPRESS;Database=EPoliceConnectDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Civilian>(entity =>
        {
            entity.HasKey(e => e.CivilianId).HasName("PK__Civilian__951B00AD0F3A4246");
        });

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.ComplaintId).HasName("PK__Complain__A771F61C691FE666");

            entity.HasOne(d => d.Civilian).WithMany(p => p.Complaints).HasConstraintName("FK__Complaint__civil__4D94879B");
        });

        modelBuilder.Entity<Criminal>(entity =>
        {
            entity.HasKey(e => e.CriminalId).HasName("PK__Criminal__A29D6210206C396D");

            entity.HasOne(d => d.Officer).WithMany(p => p.Criminals).HasConstraintName("FK__Criminal__office__5070F446");
        });

        modelBuilder.Entity<IncidentReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Incident__779B7C58F3CAF145");

            entity.HasOne(d => d.Officer).WithMany(p => p.IncidentReports).HasConstraintName("FK__IncidentR__offic__5629CD9C");
        });

        modelBuilder.Entity<Officer>(entity =>
        {
            entity.HasKey(e => e.OfficerId).HasName("PK__Officer__AF789997F96357C4");
        });

        modelBuilder.Entity<PrisonRecord>(entity =>
        {
            entity.HasKey(e => e.PrisonId).HasName("PK__PrisonRe__7F4B2E49C8FE99DA");

            entity.HasOne(d => d.Criminal).WithMany(p => p.PrisonRecords).HasConstraintName("FK__PrisonRec__crimi__534D60F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

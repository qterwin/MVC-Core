using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using mvccore.Models.CentralAccess;

namespace mvccore.Context;

public partial class CentralAccessContext : DbContext
{
    public CentralAccessContext()
    {
    }

    public CentralAccessContext(DbContextOptions<CentralAccessContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Users> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=192.168.101.25;Database=CentralAccess;User Id=sa;Password=dnhk0723$%;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UserCode);

            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.DateRegistered).HasColumnType("datetime");
            entity.Property(e => e.DateRegistrationVerified).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DesignationId).HasColumnName("DesignationID");
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.EmployeeNo).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PasswordEncypt).HasMaxLength(250);
            entity.Property(e => e.Remarks).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

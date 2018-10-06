using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CompanyWebAPI.DataAccessLayer
{
    public partial class ApnaCompanyContext : DbContext
    {
        public virtual DbSet<TblCandidate> TblCandidate { get; set; }
        public virtual DbSet<TblEmployee> TblEmployee { get; set; }
        public virtual DbSet<TblInterviewDetails> TblInterviewDetails { get; set; }
        public virtual DbSet<TblSkills> TblSkills { get; set; }
        public virtual DbSet<TblCourses> TbCourses { get; set; }
        public virtual DbSet<TblQualification> TblQualifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=ApnaCompany;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCandidate>(entity =>
            {
                entity.HasKey(e => e.CandidateEmail);

                entity.ToTable("tblCandidate");

                entity.Property(e => e.CandidateEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CandidateAddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CandidateContactNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CandidateDateOfBirth)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CandidateHighestQualification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CandidateId)
                    .HasColumnName("CandidateID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CandidateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CandidateResume)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("tblEmployee");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.EmployeeEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeRole)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblInterviewDetails>(entity =>
            {
                entity.HasKey(e => e.ApplicationId);

                entity.ToTable("tblInterviewDetails");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.CandidateId).HasColumnName("CandidateID");

                entity.Property(e => e.Hrinterviewer).HasColumnName("HRInterviewer");

                entity.Property(e => e.Itinterviewer).HasColumnName("ITInterviewer");

                entity.HasOne(d => d.HrinterviewerNavigation)
                    .WithMany(p => p.TblInterviewDetails)
                    .HasForeignKey(d => d.Hrinterviewer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblInterviewDetails_tblEmployee");
            });
            modelBuilder.Entity<TblSkills>(entity =>
            {
                entity.HasKey(e => e.SkillId);

                entity.ToTable("tblSKills");
            });
            modelBuilder.Entity<TblCourses>(entity =>
            {
                entity.HasKey(e => e.CourseId);

                entity.ToTable("tblCourses");
            });
            modelBuilder.Entity<TblQualification>(entity =>
            {
                entity.HasKey(e => e.QualificationID);

                entity.ToTable("tblQualification");
            });
        }
    }
}
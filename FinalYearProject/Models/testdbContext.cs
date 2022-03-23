using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class mydbcon : IdentityDbContext<ApplicationUser>
    {
        public mydbcon()
        {
        }

        public mydbcon(DbContextOptions<mydbcon> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<ExamDetails> ExamDetails { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<FLevels> FLevels { get; set; }
        public virtual DbSet<ScheduleWithCourse> ScheduleWithCourse { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public object Course { get; internal set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreditHrs).HasColumnName("credit_hrs");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.ApplicationUserId).HasColumnName("application_user_id");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ApplicationUser");

                entity.HasOne(f => f.Faculty)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.Faculty_id)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Faculty_iD");

                entity.HasOne(d => d.FLevels)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.FLevel_Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FLevels");


                entity.HasOne(d => d.ExamDetails)
                .WithOne(i => i.Course)
                .HasForeignKey<ExamDetails>(b => b.Course_id);

                entity.HasOne(a => a.ScheduleWithCourse)
                .WithOne(b => b.Course)
                .HasForeignKey<ScheduleWithCourse>(s => s.course_id)
                .OnDelete(DeleteBehavior.NoAction);
            });




            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.ApplicationUserId });

                entity.ToTable("Enrollment");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.ApplicationUserId).HasColumnName("application_user_id");

                entity.Property(e => e.Grade)
                    .HasMaxLength(2)
                    .HasColumnName("grade");

                entity.Property(e => e.TotalMarks).HasColumnName("totalMarks");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enrollment_Student");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enrollment_Course");
            });

            

           
            

            modelBuilder.Entity<Faculty>(entity =>
            {
                

                entity.ToTable("Faculty");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answer)
                    .IsUnicode(false)
                    .HasColumnName("answer");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Goal)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("goal");

                entity.Property(e => e.Hint)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hint");
                entity.Property(e => e.Difficulty)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Diffculty");

                entity.Property(e => e.Questionx)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("question");


                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Question_Course");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("name");
                entity.HasOne(a => a.ScheduleWithCourse)
                .WithOne(b => b.Schedule)
                .HasForeignKey<ScheduleWithCourse>(s => s.schedule_id)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Faculty");

            });

            


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

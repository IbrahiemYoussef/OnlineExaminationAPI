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
        public virtual DbSet<ExamQuestion> ExamQuestions { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<ExamDetails> ExamDetails { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }
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

               
                entity.HasOne(d => d.ExamDetails)
                .WithOne(i => i.Course)
                .HasForeignKey<ExamDetails>(b => b.Course_id);
            });




            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.ApplicationUserId });

                entity.ToTable("Enrollment");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.ApplicationUserId).HasColumnName("application_user_id");

                entity.Property(e => e.Grade)
                    .IsRequired()
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

            

            modelBuilder.Entity<ExamQuestion>(entity =>
            {
                entity.HasKey(e => new { e.ExamId, e.QuestionId })
                    .HasName("PK_ExamQuestions_1");

                entity.HasIndex(e => e.Id, "unique_eqa_id")
                    .IsUnique();

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

               

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ExamQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_ExamQuestions_Question");
            });

            

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.HasOne(a => a.Schedule)
                .WithOne(b => b.Faculty)
                .HasForeignKey<Schedule>(s => s.FacultyId)
                .OnDelete(DeleteBehavior.NoAction);

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


            });

            modelBuilder.Entity<StudentAnswer>(entity =>
            {
                entity.HasKey(e => new { e.ApplicationUserId, e.ExamQuestionsId });

                entity.ToTable("StudentAnswer");

                entity.Property(e => e.ApplicationUserId).HasColumnName("application_user_id");

                entity.Property(e => e.ExamQuestionsId).HasColumnName("exam_questions_id");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnName("answer");

                entity.HasOne(d => d.ExamQuestion)
                    .WithMany(p => p.StudentAnswers)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ExamQuestionsId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_StudentAnswer_ExamQuestions");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.StudentAnswers)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_StudentAnswer_ApplicationUsers");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

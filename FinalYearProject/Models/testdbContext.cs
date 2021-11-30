using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class testdbContext : DbContext
    {
        public testdbContext()
        {
        }

        public testdbContext(DbContextOptions<testdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamQuestion> ExamQuestions { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-GSR45SKK;Initial Catalog=testdb;Integrated Security=True;Pooling=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreditHrs).HasColumnName("credit_hrs");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.ProfessorId).HasColumnName("professor_id");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Course_Schedule");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.StudentId });

                entity.ToTable("Enrollment");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.Grade)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("grade");

                entity.Property(e => e.TotalMarks).HasColumnName("totalMarks");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Enrollment_Course");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.ToTable("Exam");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("course_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Exam_Course");
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

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamQuestions)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamQuestions_Exam");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ExamQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_ExamQuestions_Question");
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
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("answer");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Goal)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("goal");

                entity.Property(e => e.Hint)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hint");

                entity.Property(e => e.Question1)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("question");

                entity.Property(e => e.Type).HasColumnName("type");

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

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");
            });

            modelBuilder.Entity<StudentAnswer>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.ExamQuestionsId });

                entity.ToTable("StudentAnswer");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.ExamQuestionsId).HasColumnName("exam_questions_id");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnName("answer");

                entity.HasOne(d => d.ExamQuestions)
                    .WithMany(p => p.StudentAnswers)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ExamQuestionsId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_StudentAnswer_ExamQuestions");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

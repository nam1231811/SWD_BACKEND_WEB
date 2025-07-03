using System;
using System.Collections.Generic;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<ChatBotLog> ChatBotLogs { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<SchoolYear> SchoolYears { get; set; }

    public virtual DbSet<Term> Terms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=EduConnectDB;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AtID); // Thiết lập khóa chính
            entity.ToTable("Attendance");

            entity.Property(e => e.AtID)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            entity.Property(e => e.CourseId).HasColumnName("courseID");
            entity.Property(e => e.Focus).HasColumnName("focus");
            entity.Property(e => e.Homework).HasColumnName("homework");

            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("note");

            entity.Property(e => e.Participation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("participation");

            entity.Property(e => e.StudentId).HasColumnName("studentID");

            entity.HasOne(d => d.Course).WithMany()
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__cours__5535A963");

            entity.HasOne(d => d.Student).WithMany()
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__stude__5441852A");
        });

        modelBuilder.Entity<ChatBotLog>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__ChatBotL__19BDBDB3C0C4419E");

            entity.ToTable("ChatBotLog");

            entity.Property(e => e.ChatId).HasColumnName("chatID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.MessageId).HasColumnName("messageID");
            entity.Property(e => e.ParentId).HasColumnName("parentID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Message).WithMany(p => p.ChatBotLogs)
                .HasForeignKey(d => d.MessageId)
                .HasConstraintName("FK__ChatBotLo__messa__6383C8BA");

            entity.HasOne(d => d.Parent).WithMany(p => p.ChatBotLogs)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__ChatBotLo__paren__656C112C");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.ClassId);
            entity.ToTable("Classroom");

            entity.Property(e => e.ClassId).HasColumnName("classID");
            entity.Property(e => e.ClassName).HasColumnName("className").HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.TeacherId).HasColumnName("teacherID");

            entity.HasOne(d => d.Teacher)
                .WithOne(p => p.Classroom) // <- 1-1
                .HasForeignKey<Classroom>(d => d.TeacherId)
                .HasConstraintName("FK_Classroom_Teacher");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__2AA84FF184342A62");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId).HasColumnName("courseID");
            entity.Property(e => e.ClassId).HasColumnName("classID");
            entity.Property(e => e.DayOfWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dayOfWeek");
            entity.Property(e => e.EndTime).HasColumnName("endTime");
            entity.Property(e => e.StartTime).HasColumnName("startTime");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TeacherId).HasColumnName("teacherID");
            entity.Property(e => e.SemeId).HasColumnName("semeID");

            entity.HasOne(d => d.Class).WithMany(p => p.Courses)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Course__classID__4F7CD00D");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__Course__teacherI__5070F446");

            entity.HasOne(d => d.Semester).WithMany(p => p.Courses)
                .HasForeignKey(d => d.SemeId)
                .HasConstraintName("FK__Course__termID__5165187F");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Message__4808B873C2F34A8E");

            entity.ToTable("Message");

            entity.Property(e => e.MessageId).HasColumnName("messageID");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.MessageText)
                .HasColumnType("text")
                .HasColumnName("messageText");
            entity.Property(e => e.ResponseText)
                .HasColumnType("text")
                .HasColumnName("responseText");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Report__04C97FDB86006B0D");

            entity.ToTable("Report");

            entity.Property(e => e.ReportId).HasColumnName("ReportId");
            entity.Property(e => e.ClassId).HasColumnName("classID");
            entity.Property(e => e.TeacherId).HasColumnName("teacherID");
            entity.Property(e => e.TermId).HasColumnName("termID");

            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.TeacherName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("teacherName");

            entity.Property(e => e.ClassName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("className");

            entity.HasOne(d => d.Class).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Report__class__59063A47");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Reports)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__Report__teach__5812160E");

            entity.HasOne(d => d.Term)
                .WithOne(p => p.Report)
                .HasForeignKey<Term>(p => p.ReportId)
                .HasConstraintName("FK_Term_Report");
        });

        modelBuilder.Entity<Term>(entity =>
        {
            entity.HasKey(e => e.TermID).HasName("PK_Term");

            entity.ToTable("Term");

            entity.Property(e => e.TermID).HasColumnName("termID");
            entity.Property(e => e.Mode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mode");

            entity.Property(e => e.ReportId).HasColumnName("ReportId");

            entity.HasOne(d => d.Report)
                .WithOne(p => p.Term)
                .HasForeignKey<Term>(d => d.ReportId)
                .HasConstraintName("FK_Term_Report");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.ParentId).HasName("PK__Parent__90658CB8D9406FDE");

            entity.ToTable("Parent");

            entity.Property(e => e.ParentId).HasColumnName("parentID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.Parents)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Parent__userID__3E52440B");
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => e.ScoreId).HasName("PK__Score__B56A0D6D900EDC0A");

            entity.ToTable("Score");

            entity.Property(e => e.ScoreId).HasColumnName("scoreID");

            entity.Property(e => e.Score1)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("score");

            entity.Property(e => e.SubjectId).HasColumnName("subjectID");
            entity.Property(e => e.StudentId).HasColumnName("studentID");

            entity.HasOne(d => d.Subject)
                .WithMany(p => p.Scores)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Score__subjectID__4CA06362");

            entity.HasOne(d => d.Student) 
                .WithMany(p => p.Scores)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Score_Student");
        });


        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__4D11D65C2495EB66");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("studentID");
            entity.Property(e => e.ClassId).HasColumnName("classID");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.ParentId).HasColumnName("parentID");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_Student_Classroom");

            entity.HasOne(d => d.Parent).WithMany(p => p.Students)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Student__parentI__412EB0B6");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subject__ACF9A740AF98EE05");

            entity.ToTable("Subject");

            entity.Property(e => e.SubjectId).HasColumnName("subjectID");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subjectName");
            entity.Property(e => e.SemeId).HasColumnName("semeID");

            entity.HasOne(d => d.Semester).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.SemeId)
                .HasConstraintName("FK__Subject__termID__3B75D760");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId);
            entity.ToTable("Teacher");

            entity.Property(e => e.TeacherId).HasColumnName("teacherID");
            entity.Property(e => e.SubjectId).HasColumnName("subjectID");
            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.FcmToken).HasColumnName("fcm_token").HasMaxLength(500).IsUnicode(false);
            entity.Property(e => e.Platform).HasColumnName("platform").HasMaxLength(20).IsUnicode(false);

            entity.HasOne(d => d.Subject)
                .WithMany(p => p.Teachers)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_Teacher_Subject");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Teachers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Teacher_User");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.SemeId).HasName("PK_Semester");

            entity.ToTable("Semester");

            entity.Property(e => e.SemeId).HasColumnName("semeID");

            entity.Property(e => e.StartDate).HasColumnName("startDate");

            entity.Property(e => e.EndDate).HasColumnName("endDate");

            entity.Property(e => e.SemesterName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("semesterName");

            entity.Property(e => e.SchoolYearID)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("schoolYearID");

            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<SchoolYear>(entity =>
        {
            entity.HasKey(e => e.SchoolYearID).HasName("PK_SchoolYear");

            entity.ToTable("SchoolYear");

            entity.Property(e => e.SchoolYearID)
                .HasColumnName("schoolYearID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");

            // 1 SchoolYear có nhiều Semester
            entity.HasMany(e => e.Semesters)
                .WithOne(e => e.SchoolYear)
                .HasForeignKey(e => e.SchoolYearID)
                .HasConstraintName("FK_Semester_SchoolYear");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CDFEE67CF80");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("passwordHash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.UserImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("userImage");
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


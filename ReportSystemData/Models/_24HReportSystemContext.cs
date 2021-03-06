using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemData.Models
{
    public partial class _24HReportSystemContext : DbContext
    {
        public _24HReportSystemContext()
        {
        }

        public _24HReportSystemContext(DbContextOptions<_24HReportSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountInfo> AccountInfo { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Emotion> Emotion { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<ReportDetail> ReportDetail { get; set; }
        public virtual DbSet<ReportTask> ReportTask { get; set; }
        public virtual DbSet<ReportView> ReportView { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RootCategory> RootCategory { get; set; }
        public virtual DbSet<Task> Task { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<AccountInfo>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.ToTable("Account_Info");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.IdentityCard)
                    .HasColumnName("Identity_Card")
                    .HasMaxLength(12)
                    .IsFixedLength();

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("Phone_Number")
                    .HasMaxLength(11)
                    .IsFixedLength();

                entity.Property(e => e.Username).HasMaxLength(30);

                entity.HasOne(d => d.EmailNavigation)
                    .WithOne(p => p.AccountInfo)
                    .HasForeignKey<AccountInfo>(d => d.Email)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Info_Account");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .HasColumnName("Category_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RootCategory).HasColumnName("Root_Category");

                entity.Property(e => e.SubCategory)
                    .IsRequired()
                    .HasColumnName("Sub_Category")
                    .HasMaxLength(50);

                entity.HasOne(d => d.RootCategoryNavigation)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.RootCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Root_Category");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId)
                    .HasColumnName("Comment_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.CommentTitle)
                    .IsRequired()
                    .HasColumnName("Comment_Title")
                    .HasMaxLength(100);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("Create_TIme")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.PostId)
                    .IsRequired()
                    .HasColumnName("Post_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("User_ID")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Account");
            });

            modelBuilder.Entity<Emotion>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserId });

                entity.Property(e => e.PostId)
                    .HasColumnName("Post_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .HasColumnName("User_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.EmotionStatus).HasColumnName("Emotion_Status");

                entity.Property(e => e.IsView).HasColumnName("Is_View");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Emotion)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Emotion_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Emotion)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Emotion_Account");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId)
                    .HasColumnName("Post_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("Create_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EditorId)
                    .IsRequired()
                    .HasColumnName("Editor_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.PublicTime)
                    .HasColumnName("Public_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SubTitle)
                    .IsRequired()
                    .HasColumnName("Sub_Title")
                    .HasMaxLength(300);

                entity.Property(e => e.TaskId)
                    .HasColumnName("Task_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("Update_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Video)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ViewCount).HasColumnName("View_Count");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Catgory");

                entity.HasOne(d => d.Editor)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.EditorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Account");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_Post_Task");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.ReportId)
                    .HasColumnName("Report_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("Create_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EditorId)
                    .HasColumnName("Editor_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.IsAnonymous).HasColumnName("Is_Anonymous");

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.StaffId)
                    .HasColumnName("Staff_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TimeFraud)
                    .HasColumnName("Time_Fraud")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_ID")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Catgory");

                entity.HasOne(d => d.Editor)
                    .WithMany(p => p.ReportEditor)
                    .HasForeignKey(d => d.EditorId)
                    .HasConstraintName("FK_Report_Account2");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.ReportStaff)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_Report_Account1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReportUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Report_Account");
            });

            modelBuilder.Entity<ReportDetail>(entity =>
            {
                entity.ToTable("Report_Detail");

                entity.Property(e => e.ReportDetailId).HasColumnName("Report_Detail_ID");

                entity.Property(e => e.Media).IsUnicode(false);

                entity.Property(e => e.ReportId)
                    .IsRequired()
                    .HasColumnName("Report_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportDetail)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Detail_Report");
            });

            modelBuilder.Entity<ReportTask>(entity =>
            {
                entity.HasKey(e => new { e.TaskId, e.ReportId });

                entity.ToTable("Report_Task");

                entity.Property(e => e.TaskId)
                    .HasColumnName("Task_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.ReportId)
                    .HasColumnName("Report_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("Create_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportTask)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Task_Report");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.ReportTask)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Task_Task");
            });

            modelBuilder.Entity<ReportView>(entity =>
            {
                entity.HasKey(e => new { e.ReportId, e.UserId });

                entity.ToTable("Report_View");

                entity.Property(e => e.ReportId)
                    .HasColumnName("Report_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .HasColumnName("User_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.IsView).HasColumnName("Is_View");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportView)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_View_Report");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReportView)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_View_Account");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .HasColumnName("Role_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("Role_Name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RootCategory>(entity =>
            {
                entity.ToTable("Root_Category");

                entity.Property(e => e.RootCategoryId)
                    .HasColumnName("Root_Category_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.TaskId)
                    .HasColumnName("Task_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("Create_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeadLineTime)
                    .HasColumnName("DeadLine_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditorId)
                    .IsRequired()
                    .HasColumnName("Editor_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Editor)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.EditorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Account");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using Microsoft.EntityFrameworkCore;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Entities.ClassFd;

namespace WebApi_QLSV.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<ClassStudent> ClassStudents { get; set; }
        public DbSet<LopHP> LopHPs { get; set; }
        public DbSet<LopQL> LopQLs { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<BoMon> BoMons { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<CTKhung> CTKhungs { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<Nganh> Nganhs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Nganh>()
                .HasOne<Khoa>()
                .WithMany()
                .HasForeignKey(e => e.KhoaId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<Nganh>()
                .HasOne<CTKhung>()
                .WithOne()
                .HasForeignKey<Nganh>(e => e.CTKhungId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<LopQL>()
                .HasOne<Nganh>()
                .WithMany()
                .HasForeignKey(e => e.NganhId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<LopHP>()
                .HasOne<Nganh>()
                .WithMany()
                .HasForeignKey(e => e.NganhId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<LopHP>()
                .HasOne<MonHoc>()
                .WithMany()
                .HasForeignKey(e => e.MonId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<ClassStudent>()
                .HasOne<LopHP>()
                .WithMany()
                .HasForeignKey(e => e.LopHPId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<ClassStudent>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ClassStudent>().HasKey(s => new { s.StudentId, s.LopHPId });
            modelBuilder
                .Entity<Teacher>()
                .HasOne<BoMon>()
                .WithMany()
                .HasForeignKey(e => e.BoMonId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<LopHP>()
                .HasOne<Teacher>()
                .WithMany()
                .HasForeignKey(e => e.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<Student>()
                .HasOne<LopQL>()
                .WithMany()
                .HasForeignKey(e => e.TenLopQL)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<LopHP>()
                .HasOne<Block>()
                .WithMany()
                .HasForeignKey(e => e.BlockId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<MonHoc>()
                .HasOne<CTKhung>()
                .WithMany()
                .HasForeignKey(e => e.CTKhungId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }
    }
}

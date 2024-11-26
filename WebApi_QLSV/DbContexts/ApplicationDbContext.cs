using Microsoft.EntityFrameworkCore;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Entities.ClassFd;

namespace WebApi_QLSV.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<LopQL> LopQLs { get; set; }
        public DbSet<BoMon> BoMons { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<Nganh> Nganhs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Teacher_MonHoc> Teacher_MonHocs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BoMon>()
                .HasOne<Khoa>()
                .WithMany()
                .HasForeignKey(e => e.KhoaId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<MonHoc>()
                .HasOne<BoMon>()
                .WithMany()
                .HasForeignKey(e => e.BoMonId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Teacher>()
                .HasOne<BoMon>()
                .WithMany()
                .HasForeignKey(e => e.BoMonId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<Nganh>()
                .HasOne<Khoa>()
                .WithMany()
                .HasForeignKey(e => e.KhoaId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<LopQL>()
                .HasOne<Nganh>()
                .WithMany()
                .HasForeignKey(e => e.NganhId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<Student>()
                .HasOne<LopQL>()
                .WithMany()
                .HasForeignKey(e => e.LopQLId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Teacher_MonHoc>().HasKey(s => new { s.TeacherId, s.MaMonHoc });
            modelBuilder
                .Entity <Teacher_MonHoc>()
                .HasOne<Teacher>()
                .WithMany()
                .HasForeignKey( e => e.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<Teacher_MonHoc>()
                .HasOne<MonHoc>()
                .WithMany()
                .HasForeignKey(e => e.MaMonHoc)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}

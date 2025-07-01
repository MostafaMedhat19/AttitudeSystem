

namespace AttitudeSystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<AttitudeRecord> AttitudeRecords { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Principal> Principals { get; set; }
        public DbSet<VicePrincipal> VicePrincipals { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
        .HasDiscriminator<UserRole>("Role")
        .HasValue<Principal>(UserRole.Principal)
        .HasValue<VicePrincipal>(UserRole.VicePrincipal)
        .HasValue<Manager>(UserRole.Manager)
        .HasValue<Teacher>(UserRole.Teacher)
        .HasValue<User>(UserRole.BasicUser);




            modelBuilder.Entity<Principal>()
                .HasMany(p => p.Students)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VicePrincipal>()
                .HasMany(vp => vp.Students)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Manager>()
                .HasMany(m => m.ProblemStudents)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Students)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

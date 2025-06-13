using EduCanin.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduCanin.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<CourseSession> CourseSessions { get; set; }
    public DbSet<CourseType> CourseTypes { get; set; }
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<DogCourseSession> DogCourseSessions { get; set; }
    public DbSet<Breed> Breeds { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DogCourseSession>(entity =>
        {
            entity.HasKey(DogCourseSession => new { DogCourseSession.DogId, DogCourseSession.CourseSessionId });

            entity.HasOne(DogCourseSession => DogCourseSession.Dog)
            .WithMany(Dog => Dog.DogCourseSessions)
            .HasForeignKey(DogCourseSession => DogCourseSession.DogId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(DogCourseSession => DogCourseSession.CourseSession)
            .WithMany(CourseSession => CourseSession.DogParticipants)
            .HasForeignKey(DogCourseSession => DogCourseSession.CourseSessionId)
            .OnDelete(DeleteBehavior.Restrict);





        });
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.DatabaseContexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Question>().Property(r => r.Position).HasDefaultValue(1);
            builder.Entity<Choice>().Property(r => r.Position).HasDefaultValue(1);

            // Eagerly loading some navigation properties.
            builder.Entity<Survey>().Navigation(r => r.Questions).AutoInclude();
            builder.Entity<Question>().Navigation(r => r.Choices).AutoInclude();
            builder.Entity<Submission>().Navigation(r => r.Answers).AutoInclude();
        }
    }
}

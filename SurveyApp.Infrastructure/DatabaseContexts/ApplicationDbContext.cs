using Microsoft.EntityFrameworkCore;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.DatabaseContexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Question>().Property(r => r.Position).HasDefaultValue(1);
            builder.Entity<Choice>().Property(r => r.Position).HasDefaultValue(1);

            // Eagerly loading navigation properties for Survey model hierarchy
            builder.Entity<Survey>().Navigation(r => r.Questions).AutoInclude();
            builder.Entity<Question>().Navigation(r => r.Choices).AutoInclude();
        }
    }
}

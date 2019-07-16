using Find_A_Tutor.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Find_A_Tutor.Infrastructure.EF
{
    public class FindATurorContext : DbContext
    {
        private readonly SqlSettings _settings;

        public DbSet<User> Users { get; set; }
        public DbSet<SchoolSubject> SchoolSubjects { get; set; }
        public DbSet<PrivateLesson> PrivateLessons { get; set; }
        public FindATurorContext(DbContextOptions<FindATurorContext> options, SqlSettings settings) : base(options)
        {
            _settings = settings;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var itemBuilder = modelBuilder.Entity<User>();
            itemBuilder.HasKey(x => x.Id);
        }
    }
}

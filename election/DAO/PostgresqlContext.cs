using election.Models;
using Microsoft.EntityFrameworkCore;
namespace election.DAO
{
    public class PostgresqlContext : DbContext
    {
        public PostgresqlContext(DbContextOptions<PostgresqlContext> options) : base(options)
        {
        }

        public DbSet<CandidateModel> candidate { get; set; }
        public DbSet<VoteModel> votes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}

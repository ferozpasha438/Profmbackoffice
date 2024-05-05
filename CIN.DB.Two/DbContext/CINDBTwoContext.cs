using CIN.Domain;
using Microsoft.EntityFrameworkCore;

namespace CIN.DB
{
    public class CINDBTwoContext : DbContext
    {
        public CINDBTwoContext()
        {

        }

        public CINDBTwoContext(DbContextOptions<CINDBTwoContext> options)
            : base(options)
        {

        }

        //public DbSet<CINUser> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

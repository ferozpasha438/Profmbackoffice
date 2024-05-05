using CIN.Domain;
using Microsoft.EntityFrameworkCore;

namespace CIN.DB
{
    public class CINDBFourContext : DbContext
    {
        public CINDBFourContext()
        {

        }

        public CINDBFourContext(DbContextOptions<CINDBFourContext> options)
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

using CIN.Domain;
using Microsoft.EntityFrameworkCore;

namespace CIN.DB
{
    public class CINDBThreeContext : DbContext
    {
        public CINDBThreeContext()
        {

        }

        public CINDBThreeContext(DbContextOptions<CINDBThreeContext> options)
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

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CalculordDBLayer
{
    public class CalculordContext : DbContext
    {
        public CalculordContext()
        {
            Database.SetInitializer<CalculordContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Calculation> Calculations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

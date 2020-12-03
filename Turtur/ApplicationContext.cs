using System.Data.Entity;
using Turtur.Models;

namespace Turtur
{
    class ApplicationContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employe> Employee { get; set; }
        public DbSet<Investor> Investors { get; set; }
        public DbSet<Money> Money { get; set; }
        public DbSet<OrgEvent> OrgEvents { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplier> Supplier { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }

    }
}

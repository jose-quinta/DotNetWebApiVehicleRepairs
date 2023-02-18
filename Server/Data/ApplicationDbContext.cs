using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Client> Clients { get; set; } = default!;
        public DbSet<Vehicle> Vehicles { get; set; } = default!;
        public DbSet<Repair> Repairs { get; set; } = default!;
    }
}
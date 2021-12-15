using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoligWebApi.Models
{
    public class BoligWebContext : IdentityDbContext
    {
        
        public BoligWebContext(DbContextOptions<BoligWebContext> options)
            : base(options)
        {
        }

        

        public DbSet<Dokument> Dokuments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Konto> Kontos { get; set; }
    }
}


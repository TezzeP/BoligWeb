using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BoligWebApi.Models
{
    public class BoligWebContext : DbContext
    {
        public BoligWebContext(DbContextOptions<BoligWebContext> options)
            : base(options)
        {
        }

        public DbSet<Dokument> Dokuments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}


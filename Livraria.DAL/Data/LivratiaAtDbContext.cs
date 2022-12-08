
using ConsumeApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LivratiaAt.Data
{
    public class LivratiaAtDbContext : IdentityDbContext<IdentityUser>
    {
        public LivratiaAtDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Autor> Autor { get; set; } 

        public DbSet<Livros> Livros { get; set; }
    }
}

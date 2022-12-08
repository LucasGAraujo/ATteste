using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ConsumeApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LivratiaAt.Data
{
    public class LivratiaAtContext : IdentityDbContext<IdentityUser>
    {
        public LivratiaAtContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Autor> Autor { get; set; } 

        public DbSet<Livros> Livros { get; set; }
    }
}

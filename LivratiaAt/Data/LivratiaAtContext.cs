using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LivratiaAt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LivratiaAt.Data
{
    public class LivratiaAtContext : IdentityDbContext<IdentityUser>
    {
        public LivratiaAtContext (DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<LivratiaAt.Models.Autor> Autor { get; set; } 

        public DbSet<LivratiaAt.Models.Livros> Livros { get; set; }
    }
}

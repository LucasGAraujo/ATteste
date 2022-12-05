using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LivratiaAt.Models;

namespace LivratiaAt.Data
{
    public class LivratiaAtContext : DbContext
    {
        public LivratiaAtContext (DbContextOptions<LivratiaAtContext> options)
            : base(options)
        {
        }

        public DbSet<LivratiaAt.Models.Autor> Autor { get; set; } = default!;

        public DbSet<LivratiaAt.Models.Livros> Livros { get; set; }
    }
}

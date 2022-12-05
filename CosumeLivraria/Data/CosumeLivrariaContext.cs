using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CosumeLivraria.Models;

namespace CosumeLivraria.Data
{
    public class CosumeLivrariaContext : DbContext
    {
        public CosumeLivrariaContext (DbContextOptions<CosumeLivrariaContext> options)
            : base(options)
        {
        }

        public DbSet<CosumeLivraria.Models.Autor> Autor { get; set; } = default!;

        public DbSet<CosumeLivraria.Models.Livros> Livros { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ManterCursosAPI.Models;

namespace ManterCursosAPI.Data
{
    public class ManterCursosAPIContext : DbContext
    {
        public ManterCursosAPIContext (DbContextOptions<ManterCursosAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ManterCursosAPI.Models.Curso>? Curso { get; set; }

        public DbSet<ManterCursosAPI.Models.Categoria>? Categoria { get; set; }
    }
}

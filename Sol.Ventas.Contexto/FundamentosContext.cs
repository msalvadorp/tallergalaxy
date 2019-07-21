
using Microsoft.EntityFrameworkCore;
using Sol.Ventas.DTO;
using System;

namespace Sol.Ventas.Contexto
{
    public class FundamentosContext : DbContext
    {
        public FundamentosContext
            (DbContextOptions<FundamentosContext> options)
            : base (options)
        {
        }

        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}

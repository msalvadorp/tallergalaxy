using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol.Ventas.ClienteMVC
{
    public class SeguridadContext : IdentityDbContext
    {
        public SeguridadContext
            (DbContextOptions<SeguridadContext> options)
                : base(options)
        {

        }
    }
}

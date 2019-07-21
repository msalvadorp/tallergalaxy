using System;
using System.Collections.Generic;
using System.Text;
using Sol.Ventas.Contexto;
using Sol.Ventas.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sol.Ventas.DTO.DTO;

namespace Sol.Ventas.Services
{
    public class ArticuloServicesSQL : IArticuloServices
    {
        private FundamentosContext _context;
        public ArticuloServicesSQL(FundamentosContext context)
        {
            _context = context;
        }

        public Articulo Actualizar(Articulo art)
        {
            _context.Articulo.Update(art);
            _context.SaveChanges();
            return art;
        }

        public void Eliminar(int id)
        {
            DTOClave dto = new DTOClave() { id = id };
            _context.ExecuteSqlNonQuery("uspArticuloEliminar", dto);
        }

        public Articulo Insertar(Articulo art)
        {
            _context.Articulo.Add(art);
            _context.SaveChanges();
            return art;
        }

        public List<Articulo> Listar()
        {

            List<Articulo> lista = (from x in _context.Articulo
                                    select x).ToList();
            return lista;
        }

        public Articulo Recuperar(int id)
        {
            Articulo a = (from x in _context.Articulo
                          where x.Codigo == id
                          select x).FirstOrDefault();
            return a;
        }
    }
}

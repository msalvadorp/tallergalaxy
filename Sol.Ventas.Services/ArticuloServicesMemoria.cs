using System;
using System.Collections.Generic;
using System.Text;
using Sol.Ventas.DTO;
using System.Linq;
namespace Sol.Ventas.Services
{
    public class ArticuloServicesMemoria : IArticuloServices
    {
        private List<Articulo> _contexto;

        public ArticuloServicesMemoria()
        {
            _contexto = new List<Articulo>();
            _contexto.Add(new Articulo { Codigo = 1, Nombre = "Agenda", Precio = 30 });
            _contexto.Add(new Articulo { Codigo = 2, Nombre = "Libro", Precio = 90 });
            _contexto.Add(new Articulo { Codigo = 3, Nombre = "Cuaderno", Precio = 5 });
        }

        public Articulo Actualizar(Articulo art)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Articulo Insertar(Articulo art)
        {

             throw new NotImplementedException();
        }

        public List<Articulo> Listar()
        {
            return _contexto;
        }

        public Articulo Recuperar(int id)
        {
            return _contexto.FirstOrDefault(t => t.Codigo == id);
        }
    }
}

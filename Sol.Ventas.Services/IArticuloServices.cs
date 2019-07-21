using Sol.Ventas.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sol.Ventas.Services
{
    public interface IArticuloServices
    {
        List<Articulo> Listar();
        Articulo Recuperar(int id);
        Articulo Insertar(Articulo art);
        Articulo Actualizar(Articulo art);

        void Eliminar(int id);

    }
}

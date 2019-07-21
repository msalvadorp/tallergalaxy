using Sol.Ventas.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sol.Ventas.Services
{
    public interface IUsuarioServices
    {
        Usuario Autenticar(string login, string clave);
        Usuario Insertar(Usuario usuario);
    }
}

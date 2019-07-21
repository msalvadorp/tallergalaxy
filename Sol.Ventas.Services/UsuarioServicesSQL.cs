using System;
using System.Collections.Generic;
using System.Text;
using Sol.Ventas.Contexto;
using Sol.Ventas.DTO;
using System.Linq;

namespace Sol.Ventas.Services
{
    public class UsuarioServicesSQL : IUsuarioServices
    {
        private readonly FundamentosContext context;

        public UsuarioServicesSQL(FundamentosContext context)
        {
            this.context = context;
        }
        public Usuario Autenticar(string login, string clave)
        {
            return context.Usuario
                .FirstOrDefault(t => t.Login == login && t.Clave == clave);
        }

        public Usuario Insertar(Usuario usuario)
        {
            context.Usuario.Add(usuario);
            context.SaveChanges();
            return usuario;
        }
    }
}

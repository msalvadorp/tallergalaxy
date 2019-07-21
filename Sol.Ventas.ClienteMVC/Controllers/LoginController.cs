using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sol.Ventas.DTO;
using Sol.Ventas.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Sol.Ventas.ClienteMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioServices _Services;
        private readonly SignInManager<IdentityUser> manager;

        public LoginController(
            IUsuarioServices _services,
            SignInManager<IdentityUser> manager
            )
        {
            _Services = _services;
            this.manager = manager;
        }
        [HttpGet]
        public IActionResult Index()
        {

            Usuario u = new Usuario();
            return View(u);
        }

        [HttpPost]
        public IActionResult Index(Usuario user)
        {
            Usuario u = _Services.Autenticar(user.Login, user.Clave);
            if (u == null)
            {
                ModelState.AddModelError("*", "Credenciales invalidas");
                return View(u);
            }
            else
            {
                //grabarlo en sesion 
                HttpContext.Session.SetString("login", u.Login);
                u.Clave = "";
                string usuariotexto = JsonConvert.SerializeObject(u);
                HttpContext.Session.SetString("user", usuariotexto);
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult CerrarSesion()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }

        public IActionResult LoginGoogle()
        {
            //Enviar a google
            string urlRetorno = Url.Action("GoogleResponse", "Login");
            var propiedades =
                manager.ConfigureExternalAuthenticationProperties
                    ("Google", urlRetorno);
            return new ChallengeResult("Google", propiedades);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            //recibir de google
            ExternalLoginInfo res = await manager.GetExternalLoginInfoAsync();
            string correo = res.Principal.Claims.ElementAt(4).Value;
            string nombre = res.Principal.Claims.ElementAt(2).Value;
            Usuario u = new Usuario()
            {
                Clave = "",
                Origen = "GMAIL",
                Login = correo,
                NombreCompleto = nombre
            };
            Usuario u2 = _Services.Insertar(u);
            HttpContext.Session.SetString("login", u2.Login);
            u.Clave = "";
            string usuariotexto = JsonConvert.SerializeObject(u2);
            HttpContext.Session.SetString("user", usuariotexto);
            return RedirectToAction("Index", "Home");
        }
    }
}
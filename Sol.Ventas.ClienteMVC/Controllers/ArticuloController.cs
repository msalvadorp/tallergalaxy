using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sol.Ventas.ClienteMVC.Filtros;
using Sol.Ventas.DTO;
using Sol.Ventas.Services;

namespace Sol.Ventas.ClienteMVC.Controllers
{
    //[AutenticaBasicoFilter]
    public class ArticuloController : Controller
    {
        private IArticuloServices _services;
        public ArticuloController(IArticuloServices services)
        {
            _services = services;
        }

        public IActionResult Index(string miFiltro = "")
        {
            //throw new Exception("Algo anda mal"); 
            List<Articulo> lista = _services.Listar();
            if (!string.IsNullOrEmpty(miFiltro))
            {
                lista = lista.Where
                    (t => t.Nombre.ToUpper().Contains(miFiltro.ToUpper())).ToList();
            }

            bool esAsincrono = false;
            string res = Request.Headers["X-Requested-With"];


            if (!string.IsNullOrEmpty(res) && res == "XMLHttpRequest")
            {
                esAsincrono = true;
            }

            if (esAsincrono)
            {
                return PartialView("_ListaArticulo", lista);
            }
            else
            {
                return View(lista);
            }
            

            //return View(new List<Articulo>());
        }

        [HttpGet]
        public IActionResult EditarParcial(int id = 0)
        {
            Articulo a;

            if (id == 0) //nuevo
            {
                a = new Articulo();
            }
            else //editar
            {
                a = _services.Recuperar(id);
            }
            return PartialView("_EditarArticulo", a);

        }

        [HttpGet]
        public JsonResult GrabarParcial(Articulo a) {
            try
            {
                if (a.Codigo == 0)
                {
                    _services.Insertar(a);
                }
                else
                {
                    _services.Actualizar(a);
                }
                return Json(new { res = 1 });
            }
            catch (Exception ex)
            {

                return Json(new { res = 0, mensaje = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Editar(int id = 0)
        {
            Articulo a;

            if (id == 0) //nuevo
            {
                a = new Articulo();
            }
            else //editar
            {
                a = _services.Recuperar(id);
            }
            return View(a);

        }

        [HttpPost]
        public IActionResult Grabar(Articulo a)
        {
            if (a.Codigo == 0)
            {
                _services.Insertar(a);
            }
            else
            {
                _services.Actualizar(a);
            }

            return RedirectToAction("Index");
        }

        public JsonResult Eliminar(int id) {
            try
            {
                _services.Eliminar(id);
                return Json(new { res = "1" });
            }
            catch (Exception ex)
            {
                return Json(new { res = "0", detalle = ex.Message });
            }

        }

    }
}
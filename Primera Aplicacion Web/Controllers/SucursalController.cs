using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Primera_Aplicacion_Web.Models;

namespace Primera_Aplicacion_Web.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult Index()
        {
            List<SucursalCLS> listaSucursal = new List<SucursalCLS>();
            using(var bd = new BDPasajeEntities()) {
                listaSucursal = (from sucursal in bd.Sucursal
                                 where sucursal.BHABILITADO == 1
                                 select new SucursalCLS {
                                     iidsucursal = sucursal.IIDSUCURSAL,
                                     nombre = sucursal.NOMBRE,
                                     telefono = sucursal.TELEFONO,
                                     email = sucursal.EMAIL,
                                 }).ToList();
            }
            return View(listaSucursal);
        }

        public ActionResult AgregarSucursal() {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarSucursal(SucursalCLS sucursalCLS) {
            if(ModelState.IsValid) {
                using(var bd = new BDPasajeEntities()) {
                    Sucursal sucursal = new Sucursal();
                    sucursal.NOMBRE = sucursalCLS.nombre;
                    sucursal.DIRECCION = sucursalCLS.direccion;
                    sucursal.TELEFONO = sucursalCLS.telefono;
                    sucursal.EMAIL = sucursalCLS.email;
                    sucursal.FECHAAPERTURA = sucursalCLS.fechaapertura;
                    sucursal.BHABILITADO = 1;
                    bd.Sucursal.Add(sucursal);
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(sucursalCLS);
        }
    }
}
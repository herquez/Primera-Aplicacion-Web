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
        public ActionResult Index(SucursalCLS sucursalCLS)
        {
            List<SucursalCLS> listaSucursal = new List<SucursalCLS>();
            using(var bd = new BDPasajeEntities()) {
                if(sucursalCLS.nombre == null) {
                    listaSucursal = (from sucursal in bd.Sucursal
                                     where sucursal.BHABILITADO == 1
                                     select new SucursalCLS {
                                         iidsucursal = sucursal.IIDSUCURSAL,
                                         nombre = sucursal.NOMBRE,
                                         telefono = sucursal.TELEFONO,
                                         email = sucursal.EMAIL,
                                     }).ToList();
                } else {
                    listaSucursal = (from sucursal in bd.Sucursal
                                     where sucursal.BHABILITADO == 1 &&
                                     sucursal.NOMBRE.Contains(sucursalCLS.nombre)
                                     select new SucursalCLS {
                                         iidsucursal = sucursal.IIDSUCURSAL,
                                         nombre = sucursal.NOMBRE,
                                         telefono = sucursal.TELEFONO,
                                         email = sucursal.EMAIL,
                                     }).ToList();
                }
            }
            return View(listaSucursal);
        }

        public ActionResult AgregarSucursal() {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarSucursal(SucursalCLS sucursalCLS) {
            if(ModelState.IsValid) {
                //Validacion
                bool repeated = false;
                using(var bd = new BDPasajeEntities()) {
                    repeated = bd.Sucursal.Where(m => m.NOMBRE.Equals(sucursalCLS.nombre)).Count() > 0;
                }
                if(repeated) {
                    sucursalCLS.errorMessage = "El nombre de la sucursal ya existe.";
                    return View(sucursalCLS);
                }

                Sucursal sucursal = new Sucursal();
                sucursal.NOMBRE = sucursalCLS.nombre;
                sucursal.DIRECCION = sucursalCLS.direccion;
                sucursal.TELEFONO = sucursalCLS.telefono;
                sucursal.EMAIL = sucursalCLS.email;
                sucursal.FECHAAPERTURA = sucursalCLS.fechaapertura;
                sucursal.BHABILITADO = 1;
                using(var bd = new BDPasajeEntities()) {
                    bd.Sucursal.Add(sucursal);
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(sucursalCLS);
        }

        public ActionResult EditarSucursal(int id) {
            SucursalCLS sucursalCLS = new SucursalCLS();
            using(var bd = new BDPasajeEntities()) {
                Sucursal sucursal = bd.Sucursal.Where(m => m.IIDSUCURSAL.Equals(id)).First();
                sucursalCLS.iidsucursal = sucursal.IIDSUCURSAL;
                sucursalCLS.nombre = sucursal.NOMBRE;
                sucursalCLS.telefono = sucursal.TELEFONO;
                sucursalCLS.direccion = sucursal.DIRECCION;
                sucursalCLS.email = sucursal.EMAIL;
                sucursalCLS.fechaapertura = (DateTime)sucursal.FECHAAPERTURA;
            }
            return View(sucursalCLS);
        }

        [HttpPost]
        public ActionResult EditarSucursal(SucursalCLS sucursalCLS) {
            if(ModelState.IsValid) {
                //Validacion
                bool repeated = false;
                using(var bd = new BDPasajeEntities()) {
                    repeated = bd.Sucursal.Where(m => m.NOMBRE.Equals(sucursalCLS.nombre) &&
                    m.IIDSUCURSAL!=sucursalCLS.iidsucursal).Count() > 0;
                }
                if(repeated) {
                    sucursalCLS.errorMessage = "El nombre de la sucursal ya existe.";
                    return View(sucursalCLS);
                }

                //Edicion
                using(var bd = new BDPasajeEntities()) {
                    Sucursal sucursal = bd.Sucursal.Where(m => m.IIDSUCURSAL.Equals(sucursalCLS.iidsucursal)).First();
                    sucursal.NOMBRE = sucursalCLS.nombre;
                    sucursal.TELEFONO = sucursalCLS.telefono;
                    sucursal.DIRECCION = sucursalCLS.direccion;
                    sucursal.EMAIL = sucursalCLS.email;
                    sucursal.FECHAAPERTURA = sucursalCLS.fechaapertura;
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(sucursalCLS);
        }

        [HttpPost]
        public ActionResult BorrarSucursal(int idSucursal) {
            if(ModelState.IsValid) {
                using(var bd = new BDPasajeEntities()) {
                    Sucursal sucursal = bd.Sucursal.Where(m => m.IIDSUCURSAL.Equals(idSucursal)).First();
                    sucursal.BHABILITADO = 0;
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
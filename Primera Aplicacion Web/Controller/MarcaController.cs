using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Primera_Aplicacion_Web.Models;

namespace Primera_Aplicacion_Web.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index(MarcaCLS marcaCLS)
        {
            //  "using" abre y cierra la conexion. Importante agregar el modelo en la cabecera.
            List<MarcaCLS> listaMarca = new List<MarcaCLS>();

            using(var bd = new BDPasajeEntities()) {
                if(marcaCLS.nombre == null) {
                    listaMarca = (from marca in bd.Marca
                              where marca.BHABILITADO == 1
                              select new MarcaCLS {
                                  iidmarca = marca.IIDMARCA,
                                  nombre = marca.NOMBRE,
                                  descripcion = marca.DESCRIPCION,
                              }).ToList();
                } else {
                    listaMarca = (from marca in bd.Marca
                                  where marca.BHABILITADO == 1 &&
                                  marca.NOMBRE.Contains(marcaCLS.nombre)
                                  select new MarcaCLS {
                                      iidmarca = marca.IIDMARCA,
                                      nombre = marca.NOMBRE,
                                      descripcion = marca.DESCRIPCION,
                                  }).ToList();
                }
                
            }
            return View(listaMarca);
        }

        public ActionResult AgregarMarca() {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarMarca(MarcaCLS marcaCLS) {
            //Validacion de elementos repetidos
            bool repeated = false;
            using(var bd = new BDPasajeEntities()) {
                repeated = bd.Marca.Where(m => m.NOMBRE.Equals(marcaCLS.nombre)).Count() > 0;
            }
            if(repeated) {
                marcaCLS.errorMessage = "El nombre de la marca ya existe.";
                return View(marcaCLS);
            }

            //Crea objeto marca para guardarlo en base de datos
            Marca marca = new Marca();
            marca.NOMBRE = marcaCLS.nombre;
            marca.DESCRIPCION = marcaCLS.descripcion;
            marca.BHABILITADO = 1;

            if(ModelState.IsValid) {
                //Guarda en base de datos
                using(var bd = new BDPasajeEntities()) {
                    bd.Marca.Add(marca);
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            //Retorna los datos ya escritos en el formulario
            return View(marcaCLS);
        }

        public ActionResult EditarMarca(int id) {
            MarcaCLS marcaCLS = new MarcaCLS();
            using(var bd = new BDPasajeEntities()) {
                Marca marca = bd.Marca.Where(m => m.IIDMARCA.Equals(id)).First();
                marcaCLS.iidmarca = marca.IIDMARCA;
                marcaCLS.nombre = marca.NOMBRE;
                marcaCLS.descripcion = marca.DESCRIPCION;
            }
            return View(marcaCLS);
        }

        [HttpPost]
        public ActionResult EditarMarca(MarcaCLS marcaCLS) {
            //Validacion de elementos repetidos
            bool repeated = false;
            using(var bd = new BDPasajeEntities()) {
                repeated = bd.Marca.Where(m => m.NOMBRE.Equals(marcaCLS.nombre) && !m.IIDMARCA.Equals(marcaCLS.iidmarca)).Count() > 0;
            }
            if(repeated) {
                marcaCLS.errorMessage = "El nombre de la marca ya existe.";
                return View(marcaCLS);
            }

            if(ModelState.IsValid) {
                using(var bd = new BDPasajeEntities()) {
                    Marca marca = bd.Marca.Where(m => m.IIDMARCA.Equals(marcaCLS.iidmarca)).First();
                    marca.NOMBRE = marcaCLS.nombre;
                    marca.DESCRIPCION = marcaCLS.descripcion;
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(marcaCLS);
        }

        [HttpPost]
        public ActionResult BorrarMarca(int idMarca) {
            if(ModelState.IsValid) {
                using(var bd = new BDPasajeEntities()) {
                    Marca marca = bd.Marca.Where(m => m.IIDMARCA.Equals(idMarca)).First();
                    marca.BHABILITADO = 0;
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
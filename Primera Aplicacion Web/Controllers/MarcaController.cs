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
        public ActionResult Index()
        {
            //  "using" abre y cierra la conexion. Importante agregar el modelo en la cabecera.

            List<MarcaCLS> listMarca = new List<MarcaCLS>();
            using (var bd = new BDPasajeEntities()) {
                listMarca = (from marca in bd.Marca
                             where marca.BHABILITADO == 1
                             select new MarcaCLS
                             {
                                iidmarca = marca.IIDMARCA,
                                nombre = marca.NOMBRE,
                                descripcion = marca.DESCRIPCION,
                             }).ToList();
            }
            return View(listMarca);
        }

        public ActionResult AgregarMarca() {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarMarca(MarcaCLS marcaCLS) {
            if(ModelState.IsValid) {
                using(var bd = new BDPasajeEntities()) {
                    Marca marca = new Marca();
                    marca.NOMBRE = marcaCLS.nombre;
                    marca.DESCRIPCION = marcaCLS.descripcion;
                    marca.BHABILITADO = 1;
                    bd.Marca.Add(marca);
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            //Retorna los datos ya escritos en el formulario
            return View(marcaCLS);
        }
    }
}
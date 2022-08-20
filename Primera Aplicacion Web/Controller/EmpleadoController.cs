using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Primera_Aplicacion_Web.Models;

namespace Primera_Aplicacion_Web.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index(EmpleadoCLS empleadoCLS)
        {
            List<EmpleadoCLS> listaEmpleado = new List<EmpleadoCLS>();
            using(var bd = new BDPasajeEntities()) {
                if(empleadoCLS.searchName == null) {
                    listaEmpleado = (from empleado in bd.Empleado
                                     join tipousuario in bd.TipoUsuario
                                     on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                     join tipocontrato in bd.TipoContrato
                                     on empleado.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO
                                     where empleado.BHABILITADO == 1
                                     select new EmpleadoCLS {
                                         iidempleado = empleado.IIDEMPLEADO,
                                         nombre = empleado.NOMBRE,
                                         appaterno = empleado.APPATERNO,
                                         apmaterno = empleado.APMATERNO,
                                         nombretipousuario = tipousuario.NOMBRE,
                                         nombretipocontrato = tipocontrato.NOMBRE,
                                         sueldo = (decimal)empleado.SUELDO
                                     }).ToList();
                } else {
                    listaEmpleado = (from empleado in bd.Empleado
                                     join tipousuario in bd.TipoUsuario
                                     on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                     join tipocontrato in bd.TipoContrato
                                     on empleado.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO
                                     where empleado.BHABILITADO == 1 &&
                                     (empleadoCLS.searchName.Contains(empleado.NOMBRE) ||
                                     empleadoCLS.searchName.Contains(empleado.APPATERNO) ||
                                     empleadoCLS.searchName.Contains(empleado.APMATERNO) ||
                                     empleado.NOMBRE.Contains(empleadoCLS.searchName) ||
                                     empleado.APPATERNO.Contains(empleadoCLS.searchName) ||
                                     empleado.APMATERNO.Contains(empleadoCLS.searchName))
                                     select new EmpleadoCLS {
                                         iidempleado = empleado.IIDEMPLEADO,
                                         nombre = empleado.NOMBRE,
                                         appaterno = empleado.APPATERNO,
                                         apmaterno = empleado.APMATERNO,
                                         nombretipousuario = tipousuario.NOMBRE,
                                         nombretipocontrato = tipocontrato.NOMBRE,
                                         sueldo = (decimal)empleado.SUELDO
                                     }).ToList();
                }
            }
            return View(listaEmpleado);
        }

        //DropDowns: Sexo, TipoContrato, TipoUsuario
        public void sexoDropDown() {
            using(var bd = new BDPasajeEntities()) {
                ViewBag.listaSexo = (from sexo in bd.Sexo
                             where sexo.BHABILITADO == 1
                             select new SelectListItem {
                                 Text = sexo.NOMBRE,
                                 Value = sexo.IIDSEXO.ToString(),
                             }).ToList();
            }
        }

        public void contratoDropDown() {
            using(var bd = new BDPasajeEntities()) {
                ViewBag.listaContrato = (from contrato in bd.TipoContrato
                                 where contrato.BHABILITADO == 1
                                 select new SelectListItem {
                                     Text = contrato.NOMBRE,
                                     Value = contrato.IIDTIPOCONTRATO.ToString(),
                                 }).ToList();
            }
        }

        public void usuarioDropDown() {
            using(var bd = new BDPasajeEntities()) {
                ViewBag.listaUsuario = (from usuario in bd.TipoUsuario
                                where usuario.BHABILITADO == 1
                                select new SelectListItem {
                                    Text = usuario.NOMBRE,
                                    Value = usuario.IIDTIPOUSUARIO.ToString(),
                                }).ToList();
            }
        }

        public ActionResult AgregarEmpleado() {
            sexoDropDown();
            contratoDropDown();
            usuarioDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult AgregarEmpleado(EmpleadoCLS empleadoCLS) {
            if(ModelState.IsValid) {
                //Validacion
                bool repeated = false;
                using(var bd = new BDPasajeEntities()) {
                    repeated = bd.Empleado.Where(m => m.NOMBRE.Equals(empleadoCLS.nombre) &&
                    m.APPATERNO.Equals(empleadoCLS.appaterno) &&
                    m.APMATERNO.Equals(empleadoCLS.apmaterno)).Count() > 0;
                }
                if(repeated) {
                    empleadoCLS.errorMessage = "El nombre del empleado ya existe.";
                    sexoDropDown();
                    contratoDropDown();
                    usuarioDropDown();
                    return View(empleadoCLS);
                }


                Empleado empleado = new Empleado();
                empleado.NOMBRE = empleadoCLS.nombre;
                empleado.APPATERNO = empleadoCLS.appaterno;
                empleado.APMATERNO = empleadoCLS.apmaterno;
                empleado.FECHACONTRATO = empleadoCLS.fechacontrato;
                empleado.SUELDO = empleadoCLS.sueldo;
                empleado.IIDTIPOUSUARIO = empleadoCLS.iidsexo;
                empleado.IIDTIPOCONTRATO = empleadoCLS.iidtipocontrato;
                empleado.IIDSEXO = empleadoCLS.iidsexo;
                empleado.BHABILITADO = 1;
                using(var bd = new BDPasajeEntities()) {
                    bd.Empleado.Add(empleado);
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            sexoDropDown();
            contratoDropDown();
            usuarioDropDown();
            return View(empleadoCLS);
        }

        public ActionResult EditarEmpleado(int id) {
            EmpleadoCLS empleadoCLS = new EmpleadoCLS();
            using(var bd = new BDPasajeEntities()) {
                Empleado empleado = bd.Empleado.Where(m => m.IIDEMPLEADO.Equals(id)).FirstOrDefault();
                empleadoCLS.iidempleado = empleado.IIDEMPLEADO;
                empleadoCLS.nombre = empleado.NOMBRE;
                empleadoCLS.appaterno = empleado.APPATERNO;
                empleadoCLS.apmaterno = empleado.APMATERNO;
                empleadoCLS.fechacontrato = (DateTime)empleado.FECHACONTRATO;
                empleadoCLS.sueldo = (decimal)empleado.SUELDO;
                empleadoCLS.iidtipousuario = (int)empleado.IIDTIPOUSUARIO;
                empleadoCLS.iidtipocontrato = (int)empleado.IIDTIPOCONTRATO;
                empleadoCLS.iidsexo = (int)empleado.IIDSEXO;
            }
            sexoDropDown();
            contratoDropDown();
            usuarioDropDown();
            return View(empleadoCLS);
        }

        [HttpPost]
        public ActionResult EditarEmpleado(EmpleadoCLS empleadoCLS) {
            if(ModelState.IsValid) {
                //Validacion
                bool repeated = false;
                using(var bd = new BDPasajeEntities()) {
                    repeated = bd.Empleado.Where(m => m.NOMBRE.Equals(empleadoCLS.nombre) &&
                    m.APPATERNO.Equals(empleadoCLS.appaterno) &&
                    m.APMATERNO.Equals(empleadoCLS.apmaterno) &&
                    m.IIDEMPLEADO != empleadoCLS.iidempleado).Count() > 0;
                }
                if(repeated) {
                    empleadoCLS.errorMessage = "El nombre del empelado ya existe.";
                    sexoDropDown();
                    contratoDropDown();
                    usuarioDropDown();
                    return View(empleadoCLS);
                }

                using(var bd = new BDPasajeEntities()) {
                    Empleado empleado = bd.Empleado.Where(m => m.IIDEMPLEADO.Equals(empleadoCLS.iidempleado)).First();
                    empleado.NOMBRE = empleadoCLS.nombre;
                    empleado.APPATERNO = empleadoCLS.appaterno;
                    empleado.APMATERNO = empleadoCLS.apmaterno;
                    empleado.FECHACONTRATO = empleadoCLS.fechacontrato;
                    empleado.SUELDO = empleadoCLS.sueldo;
                    empleado.IIDTIPOUSUARIO = empleadoCLS.iidsexo;
                    empleado.IIDTIPOCONTRATO = empleadoCLS.iidtipocontrato;
                    empleado.IIDSEXO = empleadoCLS.iidsexo;
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            sexoDropDown();
            contratoDropDown();
            usuarioDropDown();
            return View(empleadoCLS);
        }

        [HttpPost]
        public ActionResult borrarEmpleado(int idEmpleado) {
            if(ModelState.IsValid) {
                using(var bd = new BDPasajeEntities()) {
                    Empleado empleado = bd.Empleado.Where(m => m.IIDEMPLEADO.Equals(idEmpleado)).First();
                    empleado.BHABILITADO = 0;
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
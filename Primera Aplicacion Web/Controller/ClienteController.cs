using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Primera_Aplicacion_Web.Models;

namespace Primera_Aplicacion_Web.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index(ClienteCLS clienteCLS)
        {
            List<ClienteCLS> listaCliente = new List<ClienteCLS>();
            using(var db = new BDPasajeEntities()) {
                if(clienteCLS.searchName == null && clienteCLS.iidsexo == 0) {
                    listaCliente = (from cliente in db.Cliente
                                    where cliente.BHABILITADO == 1
                                    select new ClienteCLS {
                                        iidcliente = cliente.IIDCLIENTE,
                                        nombre = cliente.NOMBRE,
                                        appaterno = cliente.APPATERNO,
                                        apmaterno = cliente.APMATERNO,
                                        telefonofijo = cliente.TELEFONOFIJO,
                                    }).ToList();
                } else if(clienteCLS.iidsexo == 0) {
                    listaCliente = (from cliente in db.Cliente
                                    where cliente.BHABILITADO == 1 &&
                                    (clienteCLS.searchName.Contains(cliente.NOMBRE) ||
                                     clienteCLS.searchName.Contains(cliente.APPATERNO) ||
                                     clienteCLS.searchName.Contains(cliente.APMATERNO) ||
                                     cliente.NOMBRE.Contains(clienteCLS.searchName) ||
                                     cliente.APPATERNO.Contains(clienteCLS.searchName) ||
                                     cliente.APMATERNO.Contains(clienteCLS.searchName))
                                    select new ClienteCLS {
                                        iidcliente = cliente.IIDCLIENTE,
                                        nombre = cliente.NOMBRE,
                                        appaterno = cliente.APPATERNO,
                                        apmaterno = cliente.APMATERNO,
                                        telefonofijo = cliente.TELEFONOFIJO,
                                    }).ToList();
                } else if(clienteCLS.searchName == null) {
                    listaCliente = (from cliente in db.Cliente
                                    where cliente.BHABILITADO == 1 &&
                                    clienteCLS.iidsexo == cliente.IIDSEXO
                                    select new ClienteCLS {
                                        iidcliente = cliente.IIDCLIENTE,
                                        nombre = cliente.NOMBRE,
                                        appaterno = cliente.APPATERNO,
                                        apmaterno = cliente.APMATERNO,
                                        telefonofijo = cliente.TELEFONOFIJO,
                                    }).ToList();
                } else {
                    listaCliente = (from cliente in db.Cliente
                                    where cliente.BHABILITADO == 1 &&
                                    clienteCLS.iidsexo == cliente.IIDSEXO &&
                                    (clienteCLS.searchName.Contains(cliente.NOMBRE) ||
                                     clienteCLS.searchName.Contains(cliente.APPATERNO) ||
                                     clienteCLS.searchName.Contains(cliente.APMATERNO) ||
                                     cliente.NOMBRE.Contains(clienteCLS.searchName) ||
                                     cliente.APPATERNO.Contains(clienteCLS.searchName) ||
                                     cliente.APMATERNO.Contains(clienteCLS.searchName))
                                    select new ClienteCLS {
                                        iidcliente = cliente.IIDCLIENTE,
                                        nombre = cliente.NOMBRE,
                                        appaterno = cliente.APPATERNO,
                                        apmaterno = cliente.APMATERNO,
                                        telefonofijo = cliente.TELEFONOFIJO,
                                    }).ToList();
                }
            }
            sexoDropDown();
            return View(listaCliente);
        }

        private void sexoDropDown() {
            using(var bd = new BDPasajeEntities()) {
                ViewBag.listaSexo = (from sexo in bd.Sexo
                        where sexo.BHABILITADO == 1
                        select new SelectListItem {
                            Text = sexo.NOMBRE,
                            Value = sexo.IIDSEXO.ToString(),
                        }).ToList();
            }
        }

        public ActionResult AgregarCliente() {
            sexoDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult AgregarCliente(ClienteCLS clienteCLS) {
            if(ModelState.IsValid) {
                //Validacion
                bool repeated = false;
                using(var bd = new BDPasajeEntities()) {
                    repeated = bd.Cliente.Where(m => m.NOMBRE.Equals(clienteCLS.nombre) &&
                    m.APPATERNO.Equals(clienteCLS.appaterno) && 
                    m.APMATERNO.Equals(clienteCLS.apmaterno)).Count() > 0;
                }
                if(repeated) {
                    clienteCLS.errorMessage = "El nombre del cliente ya existe.";
                    sexoDropDown();
                    return View(clienteCLS);
                }

                Cliente cliente = new Cliente();
                cliente.NOMBRE = clienteCLS.nombre;
                cliente.APPATERNO = clienteCLS.appaterno;
                cliente.APMATERNO = clienteCLS.apmaterno;
                cliente.EMAIL = clienteCLS.email;
                cliente.DIRECCION = clienteCLS.direccion;
                cliente.IIDSEXO = clienteCLS.iidsexo;
                cliente.TELEFONOFIJO = clienteCLS.telefonofijo;
                cliente.TELEFONOCELULAR = clienteCLS.telefonocelular;
                cliente.BHABILITADO = 1;
                using(var bd = new BDPasajeEntities()) {
                    sexoDropDown();
                    bd.Cliente.Add(cliente);
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            sexoDropDown();
            return View(clienteCLS);
        }

        public ActionResult EditarCliente(int id) {
            ClienteCLS clienteCLS = new ClienteCLS();
            using(var bd = new BDPasajeEntities()) { 
                Cliente cliente = bd.Cliente.Where(m => m.IIDCLIENTE.Equals(id)).First();
                clienteCLS.iidcliente = cliente.IIDCLIENTE;
                clienteCLS.nombre = cliente.NOMBRE;
                clienteCLS.appaterno = cliente.APPATERNO;
                clienteCLS.apmaterno = cliente.APMATERNO;
                clienteCLS.email = cliente.EMAIL;
                clienteCLS.direccion = cliente.DIRECCION;
                clienteCLS.iidsexo = (int)cliente.IIDSEXO;
                clienteCLS.telefonofijo = cliente.TELEFONOFIJO;
                clienteCLS.telefonocelular = cliente.TELEFONOCELULAR;
            }
            sexoDropDown();
            return View(clienteCLS);
        }

        [HttpPost]
        public ActionResult EditarCliente(ClienteCLS clienteCLS) {
            if(ModelState.IsValid) {
                //Validacion
                bool repeated = false;
                using(var bd = new BDPasajeEntities()) {
                    repeated = bd.Cliente.Where(m => m.NOMBRE.Equals(clienteCLS.nombre) &&
                    m.APPATERNO.Equals(clienteCLS.appaterno) &&
                    m.APMATERNO.Equals(clienteCLS.apmaterno) &&
                    m.IIDCLIENTE != clienteCLS.iidcliente).Count() > 0;
                }
                if(repeated) {
                    clienteCLS.errorMessage = "El nombre del cliente ya existe.";
                    sexoDropDown();
                    return View(clienteCLS);
                }

                using(var bd = new BDPasajeEntities()) {
                    Cliente cliente = bd.Cliente.Where(m => m.IIDCLIENTE.Equals(clienteCLS.iidcliente)).First();
                    cliente.NOMBRE = clienteCLS.nombre;
                    cliente.APPATERNO = clienteCLS.appaterno;
                    cliente.APMATERNO = clienteCLS.apmaterno;
                    cliente.EMAIL = clienteCLS.email;
                    cliente.DIRECCION = clienteCLS.direccion;
                    cliente.IIDSEXO = clienteCLS.iidsexo;
                    cliente.TELEFONOFIJO = clienteCLS.telefonofijo;
                    cliente.TELEFONOCELULAR = clienteCLS.telefonocelular;
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            sexoDropDown();
            return View(clienteCLS);
        }

        [HttpPost]
        public ActionResult borrarCliente(int idCliente) {
            if(ModelState.IsValid) {
                using(var bd = new BDPasajeEntities()) {
                    Cliente cliente = bd.Cliente.Where(m => m.IIDCLIENTE.Equals(idCliente)).First();
                    cliente.BHABILITADO = 0;
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
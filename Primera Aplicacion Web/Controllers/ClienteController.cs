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
        public ActionResult Index()
        {
            List<ClienteCLS> listaCliente = new List<ClienteCLS>();
            using(var db = new BDPasajeEntities()) {
                listaCliente = (from cliente in db.Cliente
                               where cliente.BHABILITADO == 1
                               select new ClienteCLS {
                                   iidcliente = cliente.IIDCLIENTE,
                                   nombre = cliente.NOMBRE,
                                   appaterno = cliente.APPATERNO,
                                   apmaterno = cliente.APMATERNO,
                                   telefonofijo = cliente.TELEFONOFIJO,
                               }).ToList();
            }
            return View(listaCliente);
        }

        List<SelectListItem> listaSexo;
        private void setSexosToSelect() {
            using(var bd = new BDPasajeEntities()) {
                listaSexo = (from sexo in bd.Sexo
                        where sexo.BHABILITADO == 1
                        select new SelectListItem {
                            Text = sexo.NOMBRE,
                            Value = SqlFunctions.StringConvert((double)sexo.IIDSEXO)
                        }).ToList();
            }
        }

        public ActionResult AgregarCliente() {
            setSexosToSelect();
            ViewBag.listaSexo = listaSexo;
            return View();
        }

        [HttpPost]
        public ActionResult AgregarCliente(ClienteCLS clienteCLS) {
            if(ModelState.IsValid) {
                using(var bd = new BDPasajeEntities()) {
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
                    bd.Cliente.Add(cliente);
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(clienteCLS);
        }
    }
}
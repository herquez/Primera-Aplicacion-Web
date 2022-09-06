using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Primera_Aplicacion_Web.Models;

namespace Primera_Aplicacion_Web.Controllers
{
    public class BusController : Controller
    {
        // GET: Bus
        public ActionResult Index(BusCLS busCLS)
        {
            List<BusCLS> listaBus = new List<BusCLS>();
            using(var bd = new BDPasajeEntities()) {
                if(busCLS.placa == null) {
                    listaBus = (from bus in bd.Bus
                                join sucursal in bd.Sucursal
                                on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL
                                join tipobus in bd.TipoBus
                                on bus.IIDTIPOBUS equals tipobus.IIDTIPOBUS
                                join modelo in bd.Modelo
                                on bus.IIDMODELO equals modelo.IIDMODELO
                                join marca in bd.Marca
                                on bus.IIDMARCA equals marca.IIDMARCA
                                where bus.BHABILITADO == 1
                                select new BusCLS {
                                    iidbus = bus.IIDBUS,
                                    placa = bus.PLACA,
                                    nombresucursal = sucursal.NOMBRE,
                                    nombretipobus = tipobus.NOMBRE,
                                    nombremodelo = modelo.NOMBRE,
                                    nombremarca = marca.NOMBRE
                                }).ToList();
                } else {
                    listaBus = (from bus in bd.Bus
                                join sucursal in bd.Sucursal
                                on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL
                                join tipobus in bd.TipoBus
                                on bus.IIDTIPOBUS equals tipobus.IIDTIPOBUS
                                join modelo in bd.Modelo
                                on bus.IIDMODELO equals modelo.IIDMODELO
                                join marca in bd.Marca
                                on bus.IIDMARCA equals marca.IIDMARCA
                                where bus.BHABILITADO == 1 &&
                                bus.PLACA.Contains(busCLS.placa)
                                select new BusCLS {
                                    iidbus = bus.IIDBUS,
                                    placa = bus.PLACA,
                                    nombresucursal = sucursal.NOMBRE,
                                    nombretipobus = tipobus.NOMBRE,
                                    nombremodelo = modelo.NOMBRE,
                                    nombremarca = marca.NOMBRE
                                }).ToList();
                }
            }
            return View(listaBus);
        }

        public void sucursalDropDown() {
            List<SelectListItem> listaSucursal = new List<SelectListItem>();
            using(var bd = new BDPasajeEntities()) {
                listaSucursal = (from sucursal in bd.Sucursal
                                where sucursal.BHABILITADO == 1
                                select new SelectListItem {
                                    Text = sucursal.NOMBRE,
                                    Value = sucursal.IIDSUCURSAL.ToString(),
                                }).ToList();
            }
            ViewBag.listaSucursal = listaSucursal;
        }

        public void tipoBusDropDown() {
            List<SelectListItem> listaTipoBus = new List<SelectListItem>();
            using(var bd = new BDPasajeEntities()) {
                listaTipoBus = (from busType in bd.TipoBus
                                 where busType.BHABILITADO == 1
                                 select new SelectListItem {
                                     Text = busType.NOMBRE,
                                     Value = busType.IIDTIPOBUS.ToString(),
                                 }).ToList();
            }
            ViewBag.listaTipoBus = listaTipoBus;
        }

        public void modeloDropDown() {
            List<SelectListItem> listaModelo = new List<SelectListItem>();
            using(var bd = new BDPasajeEntities()) {
                listaModelo = (from modelo in bd.Modelo
                                where modelo.BHABILITADO == 1
                                select new SelectListItem {
                                    Text = modelo.NOMBRE,
                                    Value = modelo.IIDMODELO.ToString(),
                                }).ToList();
            }
            ViewBag.listaModelo = listaModelo;
        }

        public void marcaDropDown() {
            List<SelectListItem> listaMarca = new List<SelectListItem>();
            using(var bd = new BDPasajeEntities()) {
                listaMarca = (from marca in bd.Marca
                                where marca.BHABILITADO == 1
                                select new SelectListItem {
                                    Text = marca.NOMBRE,
                                    Value = marca.IIDMARCA.ToString(),
                                }).ToList();
            }
            ViewBag.listaMarca = listaMarca;
        }

        public ActionResult AgregarBus() {
            sucursalDropDown();
            tipoBusDropDown();
            modeloDropDown();
            marcaDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult AgregarBus(BusCLS busCLS) {
            if(ModelState.IsValid) {
                //Validation
                bool repeated;
                using(var bd = new BDPasajeEntities()) {
                    repeated = bd.Bus.Where(b => b.PLACA.Equals(busCLS.placa)).Count() > 0;
                }
                if(repeated) {
                    busCLS.errorMessage = "El bus con la placa " + busCLS.placa + " ya esta registrado.";
                    sucursalDropDown();
                    tipoBusDropDown();
                    modeloDropDown();
                    marcaDropDown();
                    return View(busCLS);
                }

                Bus bus = new Bus();
                bus.IIDSUCURSAL = busCLS.iidsucursal;
                bus.IIDTIPOBUS = busCLS.iidtipobus;
                bus.PLACA = busCLS.placa;
                bus.FECHACOMPRA = busCLS.fechacompra;
                bus.IIDMODELO = busCLS.iidmodelo;
                bus.NUMEROFILAS = busCLS.numerofilas;
                bus.NUMEROCOLUMNAS = busCLS.numerocolumnas;
                bus.DESCRIPCION = busCLS.descripcion;
                bus.OBSERVACION = busCLS.observacion;
                bus.IIDMARCA = busCLS.iidmarca;
                bus.BHABILITADO = 1;
                using(var bd = new BDPasajeEntities()) {
                    bd.Bus.Add(bus);
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            sucursalDropDown();
            tipoBusDropDown();
            modeloDropDown();
            marcaDropDown();
            return View(busCLS);
        }

        public ActionResult EditarBus(int id) {
            BusCLS busCLS = new BusCLS();
            using(var bd = new BDPasajeEntities()) {
                Bus bus = bd.Bus.Where(m => m.IIDBUS.Equals(id)).First();
                busCLS.iidbus = bus.IIDBUS;
                busCLS.iidsucursal = (int)bus.IIDSUCURSAL;
                busCLS.iidtipobus = (int)bus.IIDTIPOBUS;
                busCLS.placa = bus.PLACA;
                busCLS.fechacompra = (DateTime)bus.FECHACOMPRA;
                busCLS.iidmodelo = (int)bus.IIDMODELO;
                busCLS.numerofilas = (int)bus.NUMEROFILAS;
                busCLS.numerocolumnas = (int)bus.NUMEROCOLUMNAS;
                busCLS.descripcion = bus.DESCRIPCION;
                busCLS.observacion = bus.OBSERVACION;
                busCLS.iidmarca = (int)bus.IIDMARCA;
            }
            sucursalDropDown();
            tipoBusDropDown();
            modeloDropDown();
            marcaDropDown();
            return View(busCLS);
        }

        [HttpPost]
        public ActionResult EditarBus(BusCLS busCLS) {
            if(ModelState.IsValid) {
                //Validation
                bool repeated;
                using(var bd = new BDPasajeEntities()) {
                    repeated = bd.Bus.Where(b => b.IIDBUS != busCLS.iidbus &&
                        b.BHABILITADO == 1 &&
                        b.PLACA.Equals(busCLS.placa)).Count() > 0;
                }
                if(repeated) {
                    busCLS.errorMessage = "El bus con la placa " + busCLS.placa + " ya esta registrado.";
                    sucursalDropDown();
                    tipoBusDropDown();
                    modeloDropDown();
                    marcaDropDown();
                    return View(busCLS);
                }

                using(var bd = new BDPasajeEntities()) {
                    Bus bus = bd.Bus.Where(m => m.IIDBUS.Equals(busCLS.iidbus)).First();
                    bus.IIDSUCURSAL = busCLS.iidsucursal;
                    bus.IIDTIPOBUS = busCLS.iidtipobus;
                    bus.PLACA = busCLS.placa;
                    bus.FECHACOMPRA = busCLS.fechacompra;
                    bus.IIDMODELO = busCLS.iidmodelo;
                    bus.NUMEROFILAS = busCLS.numerofilas;
                    bus.NUMEROCOLUMNAS = busCLS.numerocolumnas;
                    bus.DESCRIPCION = busCLS.descripcion;
                    bus.OBSERVACION = busCLS.observacion;
                    bus.IIDMARCA = busCLS.iidmarca;
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            sucursalDropDown();
            tipoBusDropDown();
            modeloDropDown();
            marcaDropDown();
            return View(busCLS);
        }

        [HttpPost]
        public ActionResult borrarBus(int idBus) {
            if(ModelState.IsValid) {
                using(var bd = new BDPasajeEntities()) {
                    Bus bus = bd.Bus.Where(b => b.IIDBUS.Equals(idBus)).FirstOrDefault();
                    bus.BHABILITADO = 0;
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
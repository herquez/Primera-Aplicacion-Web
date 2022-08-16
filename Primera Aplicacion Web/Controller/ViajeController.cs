using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Primera_Aplicacion_Web.Models;

namespace Primera_Aplicacion_Web.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult Index()
        {
            List<ViajeCLS> listaViaje = new List<ViajeCLS>();
            using(var bd = new BDPasajeEntities()) {
                listaViaje = (from viaje in bd.Viaje
                              join lugarDestino in bd.Lugar
                              on viaje.IIDLUGARDESTINO equals lugarDestino.IIDLUGAR
                              join lugarOrigen in bd.Lugar
                              on viaje.IIDLUGARORIGEN equals lugarOrigen.IIDLUGAR
                              join bus in bd.Bus
                              on viaje.IIDBUS equals bus.IIDBUS
                              where viaje.BHABILITADO == 1
                              select new ViajeCLS {
                                  iidviaje = viaje.IIDVIAJE,
                                  placabus = bus.PLACA,
                                  nombrelugarorigen = lugarOrigen.NOMBRE,
                                  nombrelugardestino = lugarDestino.NOMBRE,
                                  fechaviaje = (DateTime)viaje.FECHAVIAJE,
                                  precio = (decimal)viaje.PRECIO
                              }).ToList();
            }
            return View(listaViaje);
        }

        public void lugarOrigenDropDown() {
            using(var bd = new BDPasajeEntities()) {
                ViewBag.listaLugarOrigen = (from lugar in bd.Lugar
                                            where lugar.BHABILITADO == 1
                                            select new SelectListItem {
                                                Text = lugar.NOMBRE,
                                                Value = lugar.IIDLUGAR.ToString(),
                                            }).ToList();
            }
        }
        public void lugarDestinoDropDown() {
            using(var bd = new BDPasajeEntities()) {
                ViewBag.listaLugarDestino = (from lugar in bd.Lugar
                                            where lugar.BHABILITADO == 1
                                            select new SelectListItem {
                                                Text = lugar.NOMBRE,
                                                Value = lugar.IIDLUGAR.ToString(),
                                            }).ToList();
            }
        }

        public void busDropDown() {
            using(var bd = new BDPasajeEntities()) {
                ViewBag.listaBus = (from bus in bd.Bus
                                    where bus.BHABILITADO == 1
                                    select new SelectListItem {
                                        Text = bus.PLACA,
                                        Value = bus.IIDBUS.ToString(),
                                    }).ToList();
            }
        }

        public ActionResult AgregarViaje() {
            lugarOrigenDropDown();
            lugarDestinoDropDown();
            busDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult AgregarViaje(ViajeCLS viajeCLS) {
            if(ModelState.IsValid) {
                //Validation
                bool repeated = false;
                using(var bd = new BDPasajeEntities()) {
                    repeated = bd.Viaje.Where(v => v.IIDBUS.Equals(viajeCLS.iidbus) &&
                    v.FECHAVIAJE.Equals(viajeCLS.fechaviaje)).Count() > 0;
                }
                if(repeated) {
                    viajeCLS.errorMessage = "El viaje ya esta registrado.";
                    lugarOrigenDropDown();
                    lugarDestinoDropDown();
                    busDropDown();
                    return View(viajeCLS);
                }

                Viaje viaje = new Viaje();
                viaje.IIDLUGARORIGEN = viajeCLS.iidlugarorigen;
                viaje.IIDLUGARDESTINO = viajeCLS.iidlugardestino;
                viaje.PRECIO = viajeCLS.precio;
                viaje.FECHAVIAJE = viajeCLS.fechaviaje;
                viaje.IIDBUS = viajeCLS.iidbus;
                viaje.NUMEROASIENTOSDISPONIBLES = viajeCLS.numeroasientosdisponibles;
                viaje.BHABILITADO = 1;
                using(var bd = new BDPasajeEntities()) {
                    bd.Viaje.Add(viaje);
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            lugarOrigenDropDown();
            lugarDestinoDropDown();
            busDropDown();
            return View(viajeCLS);
        }

        public ActionResult EditarViaje(int id) {
            ViajeCLS viajeCLS = new ViajeCLS();
            using(var bd = new BDPasajeEntities()) {
                Viaje viaje = bd.Viaje.Where(m => m.IIDVIAJE.Equals(id)).First();
                viajeCLS.iidviaje = viaje.IIDVIAJE;
                viajeCLS.iidlugarorigen = (int)viaje.IIDLUGARORIGEN;
                viajeCLS.iidlugardestino = (int)viaje.IIDLUGARDESTINO;
                viajeCLS.precio = (decimal)viaje.PRECIO;
                viajeCLS.fechaviaje = (DateTime)viaje.FECHAVIAJE;
                viajeCLS.iidbus = (int)viaje.IIDBUS;
                viajeCLS.numeroasientosdisponibles = (int)viaje.NUMEROASIENTOSDISPONIBLES;
            }
            lugarOrigenDropDown();
            lugarDestinoDropDown();
            busDropDown();
            return View(viajeCLS);
        }

        [HttpPost]
        public ActionResult EditarViaje(ViajeCLS viajeCLS) {
            if(ModelState.IsValid) {
                using(var bd = new BDPasajeEntities()) {
                    Viaje viaje = bd.Viaje.Where(m => m.IIDVIAJE.Equals(viajeCLS.iidviaje)).First(); ;
                    viaje.IIDLUGARORIGEN = viajeCLS.iidlugarorigen;
                    viaje.IIDLUGARDESTINO = viajeCLS.iidlugardestino;
                    viaje.PRECIO = viajeCLS.precio;
                    viaje.FECHAVIAJE = viajeCLS.fechaviaje;
                    viaje.IIDBUS = viajeCLS.iidbus;
                    viaje.NUMEROASIENTOSDISPONIBLES = viajeCLS.numeroasientosdisponibles;
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            lugarOrigenDropDown();
            lugarDestinoDropDown();
            busDropDown();
            return View(viajeCLS);
        }

        [HttpPost]
        public ActionResult borrarViaje(int idViaje) {
            if(ModelState.IsValid) {
                using(var bd = new BDPasajeEntities()) {
                    Viaje viaje = bd.Viaje.Where(v => v.IIDVIAJE.Equals(idViaje)).FirstOrDefault();
                    viaje.BHABILITADO = 0;
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
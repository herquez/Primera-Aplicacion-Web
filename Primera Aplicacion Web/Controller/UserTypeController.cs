using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Primera_Aplicacion_Web.Models;

namespace Primera_Aplicacion_Web.Controllers
{
    public class UserTypeController : Controller
    {
        // GET: UserType
        public ActionResult Index(UserTypeCLS userTypeCLS)
        {
            List<UserTypeCLS> userTypeList = new List<UserTypeCLS>();
            using(var bd = new BDPasajeEntities()) {
                if(userTypeCLS.nombre == null) {
                    userTypeList = (from userType in bd.TipoUsuario
                                  where userType.BHABILITADO == 1
                                  select new UserTypeCLS {
                                      iidtipousuario = userType.IIDTIPOUSUARIO,
                                      nombre = userType.NOMBRE,
                                      descripcion = userType.DESCRIPCION,
                                  }).ToList();
                } else {
                    userTypeList = (from userType in bd.TipoUsuario
                                  where userType.BHABILITADO == 1 &&
                                  userType.NOMBRE.Contains(userTypeCLS.nombre)
                                  select new UserTypeCLS {
                                      iidtipousuario = userType.IIDTIPOUSUARIO,
                                      nombre = userType.NOMBRE,
                                      descripcion = userType.DESCRIPCION,
                                  }).ToList();
                }
            }
            return View(userTypeList);
        }

        public ActionResult Create() {
            return View();
        }
    }
}
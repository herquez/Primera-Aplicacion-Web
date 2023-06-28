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
        private UserTypeCLS globalUserType;

        // GET: UserType
        public ActionResult Index(UserTypeCLS userTypeCLS)
        {
            globalUserType = userTypeCLS;
            List<UserTypeCLS> userTypeList = new List<UserTypeCLS>();
            List<UserTypeCLS> userTypeFiltered = new List<UserTypeCLS>();
            using (var bd = new BDPasajeEntities()) {
                userTypeList = (from userType in bd.TipoUsuario
                                where userType.BHABILITADO == 1
                                select new UserTypeCLS
                                {
                                    iidtipousuario = userType.IIDTIPOUSUARIO,
                                    nombre = userType.NOMBRE,
                                    descripcion = userType.DESCRIPCION,
                                }).ToList();

                if (userTypeCLS.iidtipousuario == 0 && 
                    userTypeCLS.nombre == null && 
                    userTypeCLS.descripcion == null) {
                    userTypeFiltered = userTypeList;
                } else {
                    Predicate<UserTypeCLS> pred = new Predicate<UserTypeCLS>(searchUserType);
                    userTypeFiltered = userTypeList.FindAll(pred);
                }
            }
            return View(userTypeFiltered);
        }

        private bool searchUserType(UserTypeCLS userTypeCLS)
        {
            bool searchId = true;
            bool searchName = true;
            bool searchDescription = true;

            if(globalUserType.iidtipousuario > 0)
            {
                searchId = userTypeCLS.iidtipousuario.ToString().Contains(globalUserType.iidtipousuario.ToString());
            }
            if(globalUserType.nombre != null)
            {
                searchName = userTypeCLS.nombre.ToString().Contains(globalUserType.nombre);
            }
            if(globalUserType.descripcion != null)
            {
                searchDescription = userTypeCLS.descripcion.ToString().Contains(globalUserType.descripcion);
            }

            return (searchId && searchName && searchDescription);
        }

        public ActionResult Create() {
            return View();
        }
    }
}
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create() {
            return View();
        }
    }
}
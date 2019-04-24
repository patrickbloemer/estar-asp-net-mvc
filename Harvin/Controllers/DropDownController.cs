using Harvin.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Harvin.Controllers
{
    public class DropDownController : Controller
    {
        // GET: DropDown
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Resultado(int ddl) {
            
            ViewBag.a = AutomovelDAO.BuscarAutomovelPorId(ddl);
            return View();
        }
    }
}
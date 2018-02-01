using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samy.Controllers
{
    public class HomeController : Controller
    {
        SamyContext db = new SamyContext();
        public ActionResult Index()
        {
            var examenes = db.Examens.ToList();
            ViewBag.examenes = examenes;
            var examensHoy = new List<Examen>();
            foreach (var item in examenes)
            {
                if (item.FechaExamen == DateTime.Today)
                {
                    examensHoy.Add(item);
                }
            }
            return View(examensHoy);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
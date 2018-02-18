using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Modelo.Modelos;

namespace Samy.Controllers
{
    public class RelacionsController : Controller
    {
        private SamyContext db = new SamyContext();

        // GET: Relacions
        public ActionResult Index()
        {
            return View(db.Relacions.OrderBy(r => r.Descripcion).ToList());
        }

        // GET: Relacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relacion relacion = db.Relacions.Find(id);
            if (relacion == null)
            {
                return HttpNotFound();
            }
            return View(relacion);
        }

        // GET: Relacions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Relacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] Relacion relacion)
        {
            if (ModelState.IsValid)
            {
                var relacionAux = db.Relacions.Where(t => t.Descripcion.ToLower() ==
               relacion.Descripcion).SingleOrDefault();
                if (relacionAux == null)
                {
                    db.Relacions.Add(relacion);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.mensaje = "Esta relación ya existe";
                }
            }

            return View(relacion);
        }

        // GET: Relacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relacion relacion = db.Relacions.Find(id);
            if (relacion == null)
            {
                return HttpNotFound();
            }
            return View(relacion);
        }

        // POST: Relacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] Relacion relacion)
        {
            if (ModelState.IsValid)
            {
                var relacionAux = db.Relacions.Where(t => t.Descripcion.ToLower() ==
              relacion.Descripcion && t.Id != relacion.Id).SingleOrDefault();
                if (relacionAux == null)
                {
                    db.Entry(relacion).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.mensaje = "Esta relación ya existe";
                }
            }
            return View(relacion);
        }

        // GET: Relacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relacion relacion = db.Relacions.Find(id);
            if (relacion == null)
            {
                return HttpNotFound();
            }
            return View(relacion);
        }

        // POST: Relacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Relacion relacion = db.Relacions.Find(id);
            db.Relacions.Remove(relacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public JsonResult ListarRelaciones(string sidx, string sord, int page, int rows)
        {

            int pageIndex = page - 1;
            int pageSize = rows;

            List<Relacion> relaciones = new List<Relacion>();
            if (sord.ToUpper() == "DESC")
            {
                relaciones = db.Relacions.
                      OrderByDescending(c => c.Descripcion).
                      Skip(pageIndex * pageSize).
                    Take(pageSize).ToList();
            }
            else
            {
                relaciones = db.Relacions.
                     OrderBy(c => c.Descripcion).
                     Skip(pageIndex * pageSize).
                  Take(pageSize).ToList();
            }

            int totalRecords = db.Relacions.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var jsonR = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = relaciones.Select(r => new {
                    Id=r.Id,
                    Descripcion=r.Descripcion
                })
            };

            return Json(jsonR, JsonRequestBehavior.AllowGet);
        }
    }
}

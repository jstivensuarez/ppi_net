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
    public class SedesController : Controller
    {
        private SamyContext db = new SamyContext();

        // GET: Sedes
        public ActionResult Index()
        {
            return View(db.Sedes.ToList());
        }

        // GET: Sedes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sede sede = db.Sedes.Find(id);
            if (sede == null)
            {
                return HttpNotFound();
            }
            return View(sede);
        }

        // GET: Sedes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sedes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sede sede)
        {
            if (ModelState.IsValid)
            {
                if (db.Sedes.Where(s => s.Nombre.ToLower() == sede.Nombre).SingleOrDefault() == null)
                {
                    db.Sedes.Add(sede);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.mensaje = "Esta sede ya existe";
            }
            return View(sede);
        }

        // GET: Sedes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sede sede = db.Sedes.Find(id);
            if (sede == null)
            {
                return HttpNotFound();
            }
            return View(sede);
        }

        // POST: Sedes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] Sede sede)
        {
            if (ModelState.IsValid)
            {
                if (db.Sedes.Where(s => s.Nombre.ToLower() == sede.Nombre && s.Id != sede.Id).SingleOrDefault() == null)
                {
                    db.Entry(sede).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.mensaje = "Esta sede ya existe";
            }
            return View(sede);
        }

        // GET: Sedes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sede sede = db.Sedes.Find(id);
            if (sede == null)
            {
                return HttpNotFound();
            }
            return View(sede);
        }

        // POST: Sedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sede sede = db.Sedes.Find(id);
            db.Sedes.Remove(sede);
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
        public JsonResult ListarSedes(string sidx, string sord, int page, int rows)
        {

            int pageIndex = page - 1;
            int pageSize = rows;

            List<Sede> sedes = new List<Sede>();
            if (sord.ToUpper() == "DESC")
            {
                sedes = db.Sedes.
                      OrderByDescending(c => c.Nombre).
                      Skip(pageIndex * pageSize).
                    Take(pageSize).ToList();
            }
            else
            {
                sedes = db.Sedes.
                     OrderBy(c => c.Nombre).
                     Skip(pageIndex * pageSize).
                  Take(pageSize).ToList();
            }

            int totalRecords = db.Sedes.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var jsonR = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = sedes.Select(s => new {
                    Id= s.Id,
                    Nombre= s.Nombre
                })
            };

            return Json(jsonR, JsonRequestBehavior.AllowGet);
        }
    }
}

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
    public class ProfesorsController : Controller
    {
        private SamyContext db = new SamyContext();

        // GET: Profesors
        public ActionResult Index()
        {
            var profesors = db.Profesors.Include(p => p.TipoDocumento);
            return View(profesors.ToList());
        }

        // GET: Profesors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesor profesor = db.Profesors.Find(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        // GET: Profesors/Create
        public ActionResult Create()
        {

            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            return View();
        }

        // POST: Profesors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Profesor profesor)
        {
            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            if (profesor.TipoDocumentoId == 0)
            {
                ViewBag.mensaje = "Seleccione un tipo de documento";
                return View(profesor);
            }
            if (ModelState.IsValid)
            {
                if (db.Profesors.Where(p => p.Documento == profesor.Documento).SingleOrDefault() == null)
                {
                    db.Profesors.Add(profesor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.mensaje = "Este profesor ya existe";
            }
            return View(profesor);
        }

        // GET: Profesors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesor profesor = db.Profesors.Find(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            return View(profesor);
        }

        // POST: Profesors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Profesor profesor)
        {
            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            if (profesor.TipoDocumentoId == 0)
            {
                ViewBag.mensaje = "Seleccione un tipo de documento";
                return View(profesor);
            }
            if (ModelState.IsValid)
            {
                if (db.Profesors.Where(p => p.Documento == profesor.Documento && p.Id != profesor.Id).SingleOrDefault() == null)
                {
                    db.Entry(profesor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.mensaje = "Este profesor ya existe";
            }
          
            return View(profesor);
        }

        // GET: Profesors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesor profesor = db.Profesors.Find(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        // POST: Profesors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profesor profesor = db.Profesors.Find(id);
            db.Profesors.Remove(profesor);
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
    }
}

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
        public ActionResult Edit(Profesor profesor)
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

        [HttpGet]
        public JsonResult ListarProfesores(string sidx, string sord, int page, int rows,
            bool _search, string searchField, string searchOper, string searchString)
        {

            int pageIndex = page - 1;
            int pageSize = rows;

            List<Profesor> profesores = new List<Profesor>();
            if (sord.ToUpper() == "DESC")
            {
                profesores = db.Profesors.
                      OrderByDescending(c => c.Nombre).
                      Skip(pageIndex * pageSize).
                    Take(pageSize).ToList();
            }
            else
            {
                profesores = db.Profesors.
                     OrderBy(c => c.Nombre).
                     Skip(pageIndex * pageSize).
                  Take(pageSize).ToList();
            }

            if (_search)
            {
                switch (searchField)
                {
                    case "Nombre":
                        profesores = profesores.Where(a => a.Nombre.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "Documento":
                        profesores = profesores.Where(a => a.Documento.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "Correo":
                        profesores = profesores.Where(a => a.Correo != null &&
                         a.Correo.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "PrimerApellido":
                        profesores = profesores.Where(a => a.PrimerApellido.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "SegundoApellido":
                        profesores = profesores.Where(a => a.SegundoApellido != null &&
                        a.SegundoApellido.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                }
            }

            int totalRecords = db.Profesors.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var jsonR = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = profesores.Select(a => new
                {
                    a.Id,
                    a.Nombre,
                    a.Documento,
                    a.PrimerApellido,
                    a.SegundoApellido,
                    Correo = a.Correo
                })
            };

            return Json(jsonR, JsonRequestBehavior.AllowGet);
        }
    }
}

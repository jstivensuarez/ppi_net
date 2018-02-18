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
    public class PreguntasController : Controller
    {
        private SamyContext db = new SamyContext();

        // GET: Preguntas
        public ActionResult Index()
        {
            var preguntas = db.Preguntas.Include(p => p.Categoria);
            return View(preguntas.ToList());
        }

        // GET: Preguntas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Preguntas.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            pregunta.Categoria = db.Categorias.Find(pregunta.CategoriaId);
            return View(pregunta);
        }

        // GET: Preguntas/Create
        public ActionResult Create()
        {
            var categorias = db.Categorias.ToList();
            categorias.Add(new Categoria { Id = 0, Descripcion = "[Seleccione la categoria]" });
            ViewBag.CategoriaId = new SelectList(categorias.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            return View();
        }

        // POST: Preguntas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pregunta pregunta)
        {
            var categorias = db.Categorias.ToList();
            categorias.Add(new Categoria { Id = 0, Descripcion = "[Seleccione la categoria]" });
            ViewBag.CategoriaId = new SelectList(categorias.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            if (pregunta.CategoriaId == 0)
            {
                ViewBag.mensaje = "Seleccione una categoría";
                return View(pregunta);
            }
            if (ModelState.IsValid)
            {
                if (db.Preguntas.Where(p => p.Descripcion == pregunta.Descripcion).SingleOrDefault() == null)
                {
                    db.Preguntas.Add(pregunta);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.mensaje = "Esta pregunta ya existe";
            }
            return View(pregunta);
        }

        // GET: Preguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Preguntas.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            pregunta.Categoria = db.Categorias.Find(pregunta.CategoriaId);
            var categorias = db.Categorias.ToList();
            categorias.Add(new Categoria { Id = 0, Descripcion = "[Seleccione la categoria]" });
            ViewBag.CategoriaId = new SelectList(categorias.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            return View(pregunta);
        }

        // POST: Preguntas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pregunta pregunta)
        {
            var categorias = db.Categorias.ToList();
            categorias.Add(new Categoria { Id = 0, Descripcion = "[Seleccione la categoria]" });
            ViewBag.CategoriaId = new SelectList(categorias.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            if (pregunta.CategoriaId == 0)
            {
                ViewBag.mensaje = "Seleccione una categoría";
                return View(pregunta);
            }
            if (ModelState.IsValid)
            {
                if (db.Preguntas.Where(p => p.Descripcion == pregunta.Descripcion && p.Id != pregunta.Id).SingleOrDefault() == null)
                {
                    db.Entry(pregunta).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.mensaje = "Esta pregunta ya existe";
            }
            return View(pregunta);
        }

        // GET: Preguntas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Preguntas.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            pregunta.Categoria = db.Categorias.Find(pregunta.CategoriaId);
            return View(pregunta);
        }

        // POST: Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pregunta pregunta = db.Preguntas.Find(id);
            db.Preguntas.Remove(pregunta);
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
        public JsonResult ListarPreguntas(string sidx, string sord, int page, int rows)
        {

            int pageIndex = page - 1;
            int pageSize = rows;

            List<Pregunta> preguntas = new List<Pregunta>();
            if (sord.ToUpper() == "DESC")
            {
                preguntas = db.Preguntas.
                      OrderByDescending(c => c.Descripcion).
                      Skip(pageIndex * pageSize).
                    Take(pageSize).ToList();
            }
            else
            {
                preguntas = db.Preguntas.
                     OrderBy(c => c.Descripcion).
                     Skip(pageIndex * pageSize).
                  Take(pageSize).ToList();
            }

            int totalRecords = db.Preguntas.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var jsonR = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = preguntas.Select(p => new
                {
                    Id = p.Id,
                    Descripcion = p.Descripcion
                })
            };

            return Json(jsonR, JsonRequestBehavior.AllowGet);
        }

    }
}

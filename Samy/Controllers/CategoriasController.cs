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
    public class CategoriasController : Controller
    {
        private SamyContext db = new SamyContext();

        // GET: Categorias
        public ActionResult Index()
        {
            return View(db.Categorias.OrderBy(c=>c.Descripcion).ToList());
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            Categoria cat = new Categoria();
            return View(cat);
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var categoriaAux = db.Categorias.Where(t => t.Descripcion.ToLower() ==
              categoria.Descripcion).SingleOrDefault();
                if (categoriaAux == null)
                {
                    db.Categorias.Add(categoria);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.mensaje = "Esta categoria ya existe";
                }
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            ViewBag.SubCategorias = db.SubCategorias.Where(s => s.CategoriaId == id).ToList();
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var categoriaAux = db.Categorias.Where(t => t.Descripcion.ToLower() ==
             categoria.Descripcion && t.Id != categoria.Id).SingleOrDefault();
                if (categoriaAux == null)
                {
                    db.Entry(categoria).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.mensaje = "Esta categoria ya existe";
                }
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            db.Categorias.Remove(categoria);
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

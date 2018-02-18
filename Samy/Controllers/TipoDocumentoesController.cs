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
    public class TipoDocumentoesController : Controller
    {
        private SamyContext db = new SamyContext();

        // GET: TipoDocumentoes
        public ActionResult Index()
        {
            return View(db.TipoDocumentos.OrderBy(t => t.Descripcion) .ToList());
        }

        // GET: TipoDocumentoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocumento tipoDocumento = db.TipoDocumentos.Find(id);
            if (tipoDocumento == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocumento);
        }

        // GET: TipoDocumentoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDocumentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] TipoDocumento tipoDocumento)
        {
            if (ModelState.IsValid)
            {
                var documento = db.TipoDocumentos.Where(t => t.Descripcion.ToLower() ==
                tipoDocumento.Descripcion).SingleOrDefault();
                if (documento == null)
                {
                    db.TipoDocumentos.Add(tipoDocumento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.mensaje = "Este tipo ya existe";
                }
            }

            return View(tipoDocumento);
        }

        // GET: TipoDocumentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocumento tipoDocumento = db.TipoDocumentos.Find(id);
            if (tipoDocumento == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocumento);
        }

        // POST: TipoDocumentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] TipoDocumento tipoDocumento)
        {
            if (ModelState.IsValid)
            {
                var documento = db.TipoDocumentos.Where(t => t.Descripcion.ToLower() ==
               tipoDocumento.Descripcion && t.Id != tipoDocumento.Id).SingleOrDefault();
                if (documento == null)
                {
                    db.Entry(tipoDocumento).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.mensaje = "Este tipo ya existe";
                }
            }
            return View(tipoDocumento);
        }

        // GET: TipoDocumentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocumento tipoDocumento = db.TipoDocumentos.Find(id);
            if (tipoDocumento == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocumento);
        }

        // POST: TipoDocumentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDocumento tipoDocumento = db.TipoDocumentos.Find(id);
            db.TipoDocumentos.Remove(tipoDocumento);
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
        public JsonResult ListarTiposDocumentos(string sidx, string sord, int page, int rows)
        {

            int pageIndex = page - 1;
            int pageSize = rows;

            List<TipoDocumento> tipo = new List<TipoDocumento>();
            if (sord.ToUpper() == "DESC")
            {
                tipo = db.TipoDocumentos.
                      OrderByDescending(c => c.Descripcion).
                      Skip(pageIndex * pageSize).
                    Take(pageSize).ToList();
            }
            else
            {
                tipo = db.TipoDocumentos.
                     OrderBy(c => c.Descripcion).
                     Skip(pageIndex * pageSize).
                  Take(pageSize).ToList();
            }

            int totalRecords = db.TipoDocumentos.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var jsonR = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = tipo.Select(r => new {
                    Id = r.Id,
                    Descripcion = r.Descripcion
                })
            };

            return Json(jsonR, JsonRequestBehavior.AllowGet);
        }
    }
}

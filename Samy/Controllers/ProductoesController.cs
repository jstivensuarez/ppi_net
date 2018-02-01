using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Samy.Controllers
{
    [Authorize(Roles = "administrador")]
    public class ProductoesController : Controller
    {
        private SamyContext db = new SamyContext();

        // GET: Productoes
        public ActionResult Index()
        {
            var productos = db.Productos.Include(p => p.Proveedor).ToList();
            return View(productos);
        }

        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            ViewBag.ProveedorId = new SelectList(db.Proveedors, "Id", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            ViewBag.ProveedorId = new SelectList(db.Proveedors, "Id", "Nombre", producto.ProveedorId);
            if (ModelState.IsValid)
            {
                if (producto.file != null)
                {
                    producto.Imagen = "/Content/Img/" + producto.file.FileName;
                    try
                    {
                        producto.file.SaveAs(Server.MapPath("~/Content/Img/" + producto.file.FileName));
                        db.Productos.Add(producto);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch
                    {

                        return View(producto);
                    }
                }
               
            }
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProveedorId = new SelectList(db.Proveedors, "Id", "Nombre", producto.ProveedorId);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProveedorId = new SelectList(db.Proveedors, "Id", "Nombre", producto.ProveedorId);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
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

        public string CargarImagen(HttpPostedFileBase data) {
            byte[] buffer;
            var request = HttpContext.Request;
            if (request.Files.Count > 0)
            {
                foreach (string archivo in request.Files)
                {
                    var postedFile = request.Files[archivo];
                    int length = postedFile.ContentLength;
                    buffer = new byte[length];
                    postedFile.InputStream.Read(buffer, 0, length);

                    return Convert.ToBase64String(buffer);
                }
            }
            return "";
        }
    }
}

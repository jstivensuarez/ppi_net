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
    public class AlumnoesController : Controller
    {
        private SamyContext db = new SamyContext();

        // GET: Alumnoes
        public ActionResult Index()
        {
            var alumnos = db.Alumnos.Include(a => a.Categoria).Include(a => a.TipoDocumento)
                .Include(a => a.Sede);
            return View(alumnos.OrderBy(a => a.Nombre).ToList());
        }

        // GET: Alumnoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.Alumnos.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // GET: Alumnoes/Create
        public ActionResult Create()
        {
            var categorias = db.Categorias.ToList();
            categorias.Add(new Categoria { Id = 0, Descripcion = "[Seleccione la categoria]" });
            ViewBag.CategoriaId = new SelectList(categorias.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            var sedes = db.Sedes.ToList();
            sedes.Add(new Sede { Id = 0, Nombre = "[Seleccione la sede]" });
            ViewBag.SedeId = new SelectList(sedes.OrderBy(l => l.Nombre), "Id", "Nombre");
            return View();
        }

        // POST: Alumnoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Alumno alumno)
        {

            var categorias = db.Categorias.ToList();
            categorias.Add(new Categoria { Id = 0, Descripcion = "[Seleccione la categoria]" });
            ViewBag.CategoriaId = new SelectList(categorias.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            var sedes = db.Sedes.ToList();
            sedes.Add(new Sede { Id = 0, Nombre = "[Seleccione la sede]" });
            ViewBag.SedeId = new SelectList(sedes.OrderBy(l => l.Nombre), "Id", "Nombre");
            if (alumno.SedeId == 0)
            {
                ViewBag.mensaje = "Seleccione una sede";
                return View(alumno);
            }
            if (alumno.FechaNacimiento >= DateTime.Now || alumno.FechaNacimiento.Year < 1900)
            {
                ViewBag.mensaje = "Selecccione una fecha válida";
                return View(alumno);
            }
            if (alumno.CategoriaId == 0)
            {
                ViewBag.mensaje = "Seleccione una categoría";
                return View(alumno);
            }
            if (alumno.TipoDocumentoId == 0)
            {
                ViewBag.mensaje = "Seleccione un tipo de documento";
                return View(alumno);
            }
            categorias = db.Categorias.ToList();
            categorias.Add(new Categoria { Id = 0, Descripcion = "[Seleccione la categoria]" });
            ViewBag.CategoriaId = new SelectList(categorias.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            sedes = db.Sedes.ToList();
            sedes.Add(new Sede { Id = 0, Nombre = "[Seleccione la sede]" });
            ViewBag.SedeId = new SelectList(sedes.OrderBy(l => l.Nombre), "Id", "Nombre");
            if (ModelState.IsValid)
            {
                Alumno al = db.Alumnos.Where(a => a.Documento == alumno.Documento).SingleOrDefault();
                if (al == null)
                {
                    db.Alumnos.Add(alumno);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.mensaje = "Este alumno ya existe";
                }
                return View(alumno);
            }
            return View(alumno);
        }

        // GET: Alumnoes/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.Alumnos.Find(id);
            alumno.FechaNacimientoAux = (DateTime)alumno.FechaNacimiento;
            if (alumno == null)
            {
                return HttpNotFound();
            }
            var categorias = db.Categorias.Where(c => c.Id != alumno.CategoriaId)
                .OrderBy(c => c.Descripcion).ToList();
            categorias.Insert(0, new Categoria { Id = alumno.CategoriaId, Descripcion = alumno.Categoria.Descripcion });
            ViewBag.CategoriaId = new SelectList(categorias, "Id", "Descripcion");
            var sedes = db.Sedes.Where(c => c.Id != alumno.SedeId)
              .OrderBy(c => c.Nombre).ToList();
            sedes.Insert(0, new Sede { Id = (int)alumno.SedeId, Nombre = alumno.Sede.Nombre });
            ViewBag.SedeId = new SelectList(sedes, "Id", "Nombre");
            var lista = db.TipoDocumentos.
                Where(t => t.Id != alumno.TipoDocumentoId).
                OrderBy(t => t.Descripcion).ToList();
            lista.Insert(0, new TipoDocumento { Id = alumno.TipoDocumentoId, Descripcion = alumno.TipoDocumento.Descripcion });
            ViewBag.TipoDocumentoId = new SelectList(lista, "Id", "Descripcion");
            return View(alumno);
        }

        // POST: Alumnoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Alumno alumno)
        {
            var categorias = db.Categorias.ToList();
            categorias.Add(new Categoria { Id = 0, Descripcion = "[Seleccione la categoria]" });
            ViewBag.CategoriaId = new SelectList(categorias.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            var sedes = db.Sedes.ToList();
            sedes.Add(new Sede { Id = 0, Nombre = "[Seleccione la sede]" });
            ViewBag.SedeId = new SelectList(sedes.OrderBy(l => l.Nombre), "Id", "Nombre");

            if (alumno.FechaNacimiento.ToString().Contains("1/01/0001") && !string.IsNullOrEmpty(Request.Form["FechaNacimiento"]))
            {
                DateTime dat = Convert.ToDateTime(Request.Form["FechaNacimiento"]);
                alumno.FechaNacimiento = alumno.FechaNacimientoAux;
            }
            if (alumno.SedeId == 0)
            {
                ViewBag.mensaje = "Seleccione una sede";
                return View(alumno);
            }
            if (alumno.CategoriaId == 0)
            {
                ViewBag.mensaje = "Seleccione una categoría";
                return View(alumno);
            }
            if (alumno.FechaNacimiento >= DateTime.Now || alumno.FechaNacimiento.Year < 1900)
            {
                ViewBag.mensaje = "Selecccione una fecha válida";
                return View(alumno);
            }
            if (alumno.TipoDocumentoId == 0)
            {
                ViewBag.mensaje = "Seleccione un tipo de documento";
                return View(alumno);
            }
            if (ModelState.IsValid)
            {
                Alumno al = db.Alumnos.Where
                    (a => a.Documento == alumno.Documento
                     && a.Id != alumno.Id).SingleOrDefault();
                if (al == null)
                {

                    var entity = db.Alumnos.Find(alumno.Id);
                    entity.CategoriaId = alumno.CategoriaId;
                    entity.Correo = alumno.Correo;
                    entity.Direccion = alumno.Direccion;
                    entity.Documento = alumno.Documento;
                    entity.FechaNacimiento = alumno.FechaNacimiento;
                    entity.IsChecked = alumno.IsChecked;
                    entity.Nombre = alumno.Nombre;
                    entity.PrimerApellido = alumno.PrimerApellido;
                    entity.SedeId = alumno.SedeId;
                    entity.SegundoApellido = alumno.SegundoApellido;
                    entity.Telefono = alumno.Telefono;
                    entity.TipoDocumentoId = alumno.TipoDocumentoId;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.mensaje = "Este alumno ya existe";
                }
            }
            categorias = db.Categorias.Where(c => c.Id != alumno.CategoriaId)
               .OrderBy(c => c.Descripcion).ToList();
            categorias.Insert(0, new Categoria { Id = alumno.CategoriaId, Descripcion = db.Categorias.Find(alumno.CategoriaId).Descripcion });
            ViewBag.CategoriaId = new SelectList(categorias, "Id", "Descripcion");
            sedes = db.Sedes.Where(c => c.Id != alumno.SedeId)
             .OrderBy(c => c.Nombre).ToList();
            sedes.Insert(0, new Sede { Id = (int)alumno.SedeId, Nombre = db.Sedes.Find(alumno.SedeId).Nombre });
            ViewBag.SedeId = new SelectList(sedes, "Id", "Nombre");
            lista = db.TipoDocumentos.
               Where(t => t.Id != alumno.TipoDocumentoId).
               OrderBy(t => t.Descripcion).ToList();
            lista.Insert(0, new TipoDocumento { Id = alumno.TipoDocumentoId, Descripcion = db.TipoDocumentos.Find(alumno.TipoDocumentoId).Descripcion });
            ViewBag.TipoDocumentoId = new SelectList(lista, "Id", "Descripcion");
            return View(alumno);
        }

        // GET: Alumnoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.Alumnos.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // POST: Alumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alumno alumno = db.Alumnos.Find(id);
            db.Alumnos.Remove(alumno);
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
        public JsonResult ListarAlumnos(string sidx, string sord, int page, int rows,
            bool _search, string searchField, string searchOper, string searchString)
        {

            int pageIndex = page - 1;
            int pageSize = rows;

            List<Alumno> alumnos = new List<Alumno>();
            if (sord.ToUpper() == "DESC")
            {
                alumnos = db.Alumnos.
                      OrderByDescending(c => c.Nombre).
                      Skip(pageIndex * pageSize).
                    Take(pageSize).ToList();
            }
            else
            {
                alumnos = db.Alumnos.
                     OrderBy(c => c.Nombre).
                     Skip(pageIndex * pageSize).
                  Take(pageSize).ToList();
            }


            if (_search)
            {
                switch (searchField)
                {
                    case "Nombre":
                        alumnos = alumnos.Where(a => a.Nombre.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "Documento":
                        alumnos = alumnos.Where(a => a.Documento.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "Correo":
                        alumnos = alumnos.Where(a => a.Correo != null &&
                         a.Correo.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "PrimerApellido":
                        alumnos = alumnos.Where(a => a.PrimerApellido.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "SegundoApellido":
                        alumnos = alumnos.Where(a => a.SegundoApellido != null &&
                        a.SegundoApellido.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                }
            }

            int totalRecords = db.Alumnos.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var jsonR = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = alumnos.Select(a => new
                {
                    a.Id,
                    a.Nombre,
                    a.Documento,
                    a.PrimerApellido,
                    a.SegundoApellido,
                    a.Correo
                })
            };

            return Json(jsonR, JsonRequestBehavior.AllowGet);
        }
    }
}

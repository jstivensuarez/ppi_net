using Modelo.Dto;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Samy.Controllers
{
    public class AcudientesController : Controller
    {
        SamyContext db = new SamyContext();
        // GET: Acudientes
        public ActionResult Index()
        {
            var list = db.Acudientes.OrderBy(a=>a.Nombre).ToList();
            return View(list);
        }



        public ActionResult RegresarCreate()
        {
            AcudienteDto acudiente = Session["acudiente"] as AcudienteDto;
            acudiente.Acudiente = Session["dto"] as Acudiente;
            var lista = db.TipoDocumentos.Where(t => t.Id != acudiente.Acudiente.TipoDocumentoId).ToList();
            if (acudiente.Acudiente.TipoDocumentoId != 0)
            {
                lista.Insert(0, new TipoDocumento { Id = acudiente.Acudiente.TipoDocumentoId, Descripcion = db.TipoDocumentos.Find(acudiente.Acudiente.TipoDocumentoId).Descripcion });
                ViewBag.TipoDocumentoId = new SelectList(lista, "Id", "Descripcion");

            }
            else
            {
                lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el tipo de documento]" });
                ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(t => t.Descripcion).ToList(), "Id", "Descripcion");

            }
            return View("Create", acudiente);
        }

       
        [HttpGet]
        public ActionResult Create()
        {
            AcudienteDto acd = new AcudienteDto();
            acd.Acudiente = new Acudiente();
            acd.Relaciones = new List<RelacionAlumno>();
            Session["acudiente"] = acd;
            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion).ToList(), "Id", "Descripcion");
            return View(acd);
        }

        [HttpPost]
        public ActionResult Create(AcudienteDto dto)
        {
            try
            {
                AcudienteDto acudienteSesion = Session["acudiente"] as AcudienteDto;
                var lista = db.TipoDocumentos.ToList();
                lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
                ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion).ToList(), "Id", "Descripcion");
                Acudiente al = db.Acudientes.Where(a => a.Documento == dto.Acudiente.Documento).SingleOrDefault();
                if (al != null)
                {
                    ViewBag.mensaje = "Este acudiente ya existe";
                    dto.Relaciones = acudienteSesion.Relaciones;
                    return View(dto);
                }

                dto.Acudiente.TipoDocumentoId = Convert.ToInt32(Request.Form["TipoDocumentoId"]);
                if (dto.Acudiente.TipoDocumentoId == 0)
                {
                    ViewBag.mensaje = "Seleccione una tipo de documento";
                    dto.Relaciones = acudienteSesion.Relaciones;
                    return View(dto);
                }
                db.Acudientes.Add(dto.Acudiente);
                db.SaveChanges();
                foreach (var item in acudienteSesion.Relaciones)
                {
                    db.AcudienteAlumnos.Add(new AcudienteAlumno { AcudienteId = dto.Acudiente.Id, AlumnoId = item.Alumno.Id, RelacionId = item.RelacionId });
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch 
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult AddNewAlumno(Acudiente acudiente)
        {
            Session["dto"] = acudiente;
            return Json("True");
        }
        [HttpGet]
        public ActionResult AddAlumno()
        {
            var listaAlumnos = db.Alumnos.ToList();
            var listarelaciones = db.Relacions.ToList();
            listaAlumnos.Add(new Alumno { Id = 0, Nombre = "[Seleccione un alumno]" });
            listarelaciones.Add(new Relacion { Id = 0, Descripcion = "[Seleccione la relación]" });
            ViewBag.AlumnoId = new SelectList(listaAlumnos.OrderBy(a => a.Nombre), "Id", "Nombre");
            ViewBag.RelacionId = new SelectList(listarelaciones.OrderBy(r => r.Descripcion), "Id", "Descripcion");
            return View(new RelacionAlumno());
        }

        public ActionResult AddAlumnoEdit()
        {
           
            var listaAlumnos = db.Alumnos.ToList();
            var listarelaciones = db.Relacions.ToList();
            listaAlumnos.Add(new Alumno { Id = 0, Nombre = "[Seleccione un alumno]" });
            listarelaciones.Add(new Relacion { Id = 0, Descripcion = "[Seleccione la relación]" });
            ViewBag.AlumnoId = new SelectList(listaAlumnos.OrderBy(a => a.Nombre), "Id", "Nombre");
            ViewBag.RelacionId = new SelectList(listarelaciones.OrderBy(r => r.Descripcion), "Id", "Descripcion");
            return View(new RelacionAlumno());
        }

        [HttpPost]
        public ActionResult AddAlumno(RelacionAlumno dto)
        {
            var listaAlumnos = db.Alumnos.ToList();
            var listarelaciones = db.Relacions.ToList();
            listaAlumnos.Add(new Alumno { Id = 0, Nombre = "[Seleccione un alumno]" });
            listarelaciones.Add(new Relacion { Id = 0, Descripcion = "[Seleccione la relación]" });
            ViewBag.AlumnoId = new SelectList(listaAlumnos.OrderBy(a => a.Nombre), "Id", "Nombre");
            ViewBag.RelacionId = new SelectList(listarelaciones.OrderBy(r => r.Descripcion), "Id", "Descripcion");
            if (dto.RelacionId == 0)
            {
                ViewBag.mensaje = "Seleccione una relación";
                return View(dto);
            }
            if (dto.AlumnoId == 0)
            {
                ViewBag.mensaje = "Seleccione un alumno";
                return View(dto);
            }
            AcudienteDto acudiente = Session["acudiente"] as AcudienteDto;
            acudiente.Acudiente = Session["dto"] as Acudiente;
            Session.Remove("dto");
            var lista = db.TipoDocumentos.Where(t => t.Id != acudiente.Acudiente.TipoDocumentoId).ToList();
            if (acudiente.Acudiente.TipoDocumentoId != 0)
            {
                lista.Insert(0, new TipoDocumento { Id = acudiente.Acudiente.TipoDocumentoId, Descripcion = db.TipoDocumentos.Find(acudiente.Acudiente.TipoDocumentoId).Descripcion });
                ViewBag.TipoDocumentoId = new SelectList(lista, "Id", "Descripcion");

            }
            else
            {
                lista.Add( new TipoDocumento { Id = 0, Descripcion = "[Seleccione el tipo de documento]" });
                ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(t => t.Descripcion).ToList(), "Id", "Descripcion");

            }
            
            if (acudiente.Relaciones != null)
            {
                if (acudiente.Relaciones.Find(r => r.AlumnoId == dto.AlumnoId) == null)
                {
                    dto.Alumno = db.Alumnos.Find(dto.AlumnoId);
                    acudiente.Relaciones.Add(dto);
                    return View("Create", acudiente);
                }
                ViewBag.mensaje = "Ya se ha relacionado este alumno";
                return View(dto);
            }
            return View("Create", acudiente);
        }

        [HttpPost]
        public ActionResult AddAlumnoEdit(RelacionAlumno dto)
        {
            var listaAlumnos = db.Alumnos.ToList();
            var listarelaciones = db.Relacions.ToList();
            listaAlumnos.Add(new Alumno { Id = 0, Nombre = "[Seleccione un alumno]" });
            listarelaciones.Add(new Relacion { Id = 0, Descripcion = "[Seleccione la relación]" });
            ViewBag.AlumnoId = new SelectList(listaAlumnos.OrderBy(a => a.Nombre), "Id", "Nombre");
            ViewBag.RelacionId = new SelectList(listarelaciones.OrderBy(r => r.Descripcion), "Id", "Descripcion");
            if (dto.RelacionId == 0)
            {
                ViewBag.mensaje = "Seleccione una relación";
                return View(dto);
            }
            if (dto.AlumnoId == 0)
            {
                ViewBag.mensaje = "Seleccione un alumno";
                return View(dto);
            }

            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            AcudienteDto acudiente = Session["acudiente"] as AcudienteDto;
            if (acudiente.Relaciones != null)
            {
                if (dto.AlumnoId != 0 && acudiente.Relaciones.Find(r => r.AlumnoId == dto.AlumnoId) == null)
                {
                    dto.Alumno = db.Alumnos.Find(dto.AlumnoId);
                    acudiente.Relaciones.Add(dto);
                    return View("Edit", acudiente);
                }
                ViewBag.mensaje = "Ya se ha relacionado este alumno";
                return View(dto);
            }
            return View("Edit", acudiente);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acudiente acudiente = db.Acudientes.Find(id);
            if (acudiente == null)
            {
                return HttpNotFound();
            }
            return View(acudiente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Acudiente acudiente = db.Acudientes.Find(id);
            db.Acudientes.Remove(acudiente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acudiente acudiente = db.Acudientes.Find(id);
            if (acudiente == null)
            {
                return HttpNotFound();
            }
            return View(acudiente);
        }


        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acudiente acudiente = db.Acudientes.Find(id);

            AcudienteDto dto = new AcudienteDto
            {
                Acudiente = acudiente,
                Relaciones = (from rel in db.AcudienteAlumnos
                              join a in db.Acudientes on
                              rel.AcudienteId equals a.Id
                              join al in db.Alumnos
                              on rel.AlumnoId equals al.Id
                              where rel.AcudienteId == acudiente.Id
                              select new RelacionAlumno
                              {
                                  Alumno = al,
                                  AlumnoId = rel.AlumnoId,
                                  RelacionId = rel.RelacionId
                              }).ToList()
            };
            Session["acudiente"] = dto;
            if (acudiente == null)
            {
                return HttpNotFound();
            }

            var lista = db.TipoDocumentos.
                 Where(t => t.Id != dto.Acudiente.TipoDocumentoId).
                 OrderBy(t => t.Descripcion).ToList();
            lista.Insert(0,new TipoDocumento { Id = dto.Acudiente.TipoDocumentoId, Descripcion = dto.Acudiente.TipoDocumento.Descripcion });
            ViewBag.TipoDocumentoId = new SelectList(lista, "Id", "Descripcion");
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Acudiente acudiente)
        {
            acudiente.TipoDocumentoId = Convert.ToInt32(Request.Form["TipoDocumentoId"]);
            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            AcudienteDto dto = Session["acudiente"] as AcudienteDto;
            dto.Acudiente = acudiente;
            var ac = db.Acudientes.Find(acudiente.Id);
            ac.Nombre = acudiente.Nombre;
            ac.PrimerApellido = acudiente.PrimerApellido;
            ac.SegundoApellido = acudiente.SegundoApellido;
            ac.Telefono = acudiente.Telefono;
            ac.TipoDocumento = acudiente.TipoDocumento;
            ac.Correo = acudiente.Correo;
            ac.Direccion = ac.Direccion;
            ac.Documento = acudiente.Documento;
            ac.TipoDocumento = db.TipoDocumentos.Find(ac.TipoDocumentoId);
            db.SaveChanges();
            if (acudiente.TipoDocumentoId == 0)
            {
                ViewBag.mensaje = "Seleccione un tipo de documento";
                return View(dto);
            }
            db.AcudienteAlumnos.RemoveRange(db.AcudienteAlumnos.Where(a => a.AcudienteId == acudiente.Id).ToList());
            foreach (var item in dto.Relaciones)
            {
                db.AcudienteAlumnos.Add(new AcudienteAlumno { AlumnoId = item.Alumno.Id, RelacionId = item.RelacionId, AcudienteId = dto.Acudiente.Id });
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAlumno(int? id)
        {
            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            AcudienteDto dto = Session["acudiente"] as AcudienteDto;
            dto.Relaciones.Remove(dto.Relaciones.Where(r => r.AlumnoId == id).SingleOrDefault());
            return View("Create", dto);
        }

        public ActionResult DeleteAlumnoEdit(int? id)
        {
            var lista = db.TipoDocumentos.ToList();
            lista.Add(new TipoDocumento { Id = 0, Descripcion = "[Seleccione el documento]" });
            ViewBag.TipoDocumentoId = new SelectList(lista.OrderBy(l => l.Descripcion), "Id", "Descripcion");
            AcudienteDto dto = Session["acudiente"] as AcudienteDto;
            dto.Relaciones.Remove(dto.Relaciones.Where(r => r.AlumnoId == id).SingleOrDefault());
            return View("Edit", dto);
        }

        [HttpGet]
        public JsonResult ListarAcudientes(string sidx, string sord, int page, int rows,
            bool _search, string searchField, string searchOper, string searchString)
        {

            int pageIndex = page - 1;
            int pageSize = rows;

            List<Acudiente> acudientes = new List<Acudiente>();
            if (sord.ToUpper() == "DESC")
            {
                acudientes = db.Acudientes.
                      OrderByDescending(c => c.Nombre).
                      Skip(pageIndex * pageSize).
                    Take(pageSize).ToList();
            }
            else
            {
                acudientes = db.Acudientes.
                     OrderBy(c => c.Nombre).
                     Skip(pageIndex * pageSize).
                  Take(pageSize).ToList();
            }

            if (_search)
            {
                switch (searchField)
                {
                    case "Nombre":
                        acudientes = acudientes.Where(a => a.Nombre.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "Documento":
                        acudientes = acudientes.Where(a => a.Documento.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "Correo":
                        acudientes = acudientes.Where(a => a.Correo != null &&
                         a.Correo.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "PrimerApellido":
                        acudientes = acudientes.Where(a => a.PrimerApellido.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                    case "SegundoApellido":
                        acudientes = acudientes.Where(a => a.SegundoApellido != null &&
                        a.SegundoApellido.ToLower().Contains(searchString.ToLower())).ToList();
                        break;
                }
            }

            int totalRecords = db.Acudientes.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var jsonR = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = acudientes.Select(a => new
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Documento = a.Documento,
                    Apellidos = a.PrimerApellido+ " "+a.SegundoApellido,
                    Correo = a.Correo
                })
            };

            return Json(jsonR, JsonRequestBehavior.AllowGet);
        }
    }
}
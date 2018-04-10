using Modelo.Dto;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samy.Controllers
{
    public class ExamenController : Controller
    {
        SamyContext db = new SamyContext();
        // GET: Examen
        public ActionResult Index()
        {
            var lista = (from ex in db.Examens
                         select new ExamenDto
                         {
                             Alumnos = (from p in db.Alumnos
                                        join h in db.ExamenPregunta
                                        on p.Id equals h.AlumnoId
                                        where h.ExamenId == ex.Id
                                        select p
                                          ).ToList(),
                             //Usuarios = (from p in db.Usuarios
                             //            join h in db.ExamenUsuarios
                             //            on p.Id equals h.ApplicationUserID
                             //            where h.ExamenId == ex.Id
                             //            select p
                             //             ).ToList(),
                             Preguntas = (from p in db.Preguntas
                                          join h in db.ExamenPregunta
                                          on p.Id equals h.PreguntaId
                                          where h.ExamenId == ex.Id
                                          select p
                                          ).ToList(),
                             Id = ex.Id,
                             Descripcion = ex.Descripcion,
                             FechaExamen = ex.FechaExamen,
                             Calificado = ex.Calificado
                         }).ToList().Distinct();
            return View(lista.OrderBy(e => e.Descripcion).ToList());
        }

        // GET: Examen/Details/5
        public ActionResult Details(int id)
        {
            var examen = db.Examens.Find(id);
            ExamenDto dto = new ExamenDto
            {
                Descripcion = examen.Descripcion,
                Id = examen.Id,
                FechaExamen = examen.FechaExamen,
                Calificado = examen.Calificado,
                //Usuarios = (from p in db.Usuarios
                //            join h in db.ExamenUsuarios
                //            on p.Id equals h.ApplicationUserID
                //            where h.ExamenId == examen.Id
                //            select p).ToList(),
                ExamenPreguntas = (from p in db.ExamenPregunta
                                   where p.ExamenId == examen.Id
                                   select p).ToList()
            };
            dto.Alumnos = (from aux in dto.ExamenPreguntas
                           select aux.Alumno).Distinct().ToList();
            dto.Preguntas = (from aux in dto.ExamenPreguntas
                             select aux.Pregunta).Distinct().ToList();
            return View(dto);
        }

        public ActionResult Calificar(int id)
        {

            var examen = db.Examens.Find(id);
            ExamenDto dto = new ExamenDto
            {
                Descripcion = examen.Descripcion,
                Id = examen.Id,
                FechaExamen = examen.FechaExamen,
                Calificado = examen.Calificado,
                //Usuarios = (from p in db.Usuarios
                //            join h in db.ExamenUsuarios
                //            on p.Id equals h.ApplicationUserID
                //            where h.ExamenId == examen.Id
                //            select p).ToList(),
                ExamenPreguntas = (from p in db.ExamenPregunta
                                   where p.ExamenId == examen.Id
                                   select p).ToList()
            };
            dto.Alumnos = (from aux in dto.ExamenPreguntas
                           select aux.Alumno).Distinct().ToList();
            dto.Preguntas = (from aux in dto.ExamenPreguntas
                             select aux.Pregunta).Distinct().ToList();

            return View(dto);
        }

        public ActionResult CalificarAlumno(int id, int e)
        {
            var examen = db.Examens.Find(e);

            ExamenDto dto = new ExamenDto
            {
                Descripcion = examen.Descripcion,
                Id = examen.Id,
                FechaExamen = examen.FechaExamen,
                Calificado = examen.Calificado,
                //Usuarios = (from p in db.Usuarios
                //            join h in db.ExamenUsuarios
                //            on p.Id equals h.ApplicationUserID
                //            where h.ExamenId == examen.Id
                //            select p).ToList(),
                ExamenPreguntas = (from p in db.ExamenPregunta
                                   where p.ExamenId == examen.Id
                                   && p.AlumnoId == id
                                   select p).ToList()
            };
            dto.Alumno = db.Alumnos.Find(id);
            dto.Alumnos = (from aux in dto.ExamenPreguntas
                           select aux.Alumno).Distinct().ToList();
            dto.Preguntas = (from aux in dto.ExamenPreguntas
                             select aux.Pregunta).Distinct().ToList();

            foreach (var item in dto.Preguntas)
            {
                item.Nota = dto.ExamenPreguntas.Where(ep => ep.ExamenId == e
                                                && ep.PreguntaId == item.Id)
                                                .SingleOrDefault().Nota;
            }
            Session["idExamen"] = e;
            Session["idAlumno"] = id;
            return View(dto);
        }

        [HttpPost]
        public ActionResult Calificar(ExamenDto examen)
        {

            try
            {
                var e = db.Examens.Find(examen.Id);
                int numDes = db.ExamenPregunta.Where(et => et.ExamenId == examen.Id && et.Nota == 0)
                    .Count();
                if (numDes == 0)
                {
                    e.Calificado = true;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.mensaje = "Todos los alumnos deben tener una calificación";
                    var ex = db.Examens.Find(examen.Id);
                    ExamenDto dto = new ExamenDto
                    {
                        Descripcion = ex.Descripcion,
                        Id = ex.Id,
                        FechaExamen = ex.FechaExamen,
                        Calificado = ex.Calificado,
                        //Usuarios = (from p in db.Usuarios
                        //            join h in db.ExamenUsuarios
                        //            on p.Id equals h.ApplicationUserID
                        //            where h.ExamenId == examen.Id
                        //            select p).ToList(),
                        ExamenPreguntas = (from p in db.ExamenPregunta
                                           where p.ExamenId == ex.Id
                                           select p).ToList()
                    };
                    dto.Alumnos = (from aux in dto.ExamenPreguntas
                                   select aux.Alumno).ToList();
                    dto.Preguntas = (from aux in dto.ExamenPreguntas
                                     select aux.Pregunta).Distinct().ToList();
                    return View(dto);
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }


        }

        [HttpPost]
        public ActionResult CalificarPregunta(Pregunta[] preguntas)
        {
            int idExamen = (Int32)Session["idExamen"];
            int idAlumno = (Int32)Session["idAlumno"];
            Session.Remove("idExamen");
            Session.Remove("idAlumno");
            foreach (var item in preguntas)
            {
                db.ExamenPregunta.Where(ep => ep.AlumnoId == idAlumno
                                && ep.ExamenId == idExamen
                                && ep.PreguntaId == item.Id)
                                .SingleOrDefault().Nota = item.Nota;
            }
            db.SaveChanges();
            var examen = db.Examens.Find(idExamen);
            ExamenDto dto = new ExamenDto
            {
                Descripcion = examen.Descripcion,
                Id = examen.Id,
                FechaExamen = examen.FechaExamen,
                Calificado = examen.Calificado,
                //Usuarios = (from p in db.Usuarios
                //            join h in db.ExamenUsuarios
                //            on p.Id equals h.ApplicationUserID
                //            where h.ExamenId == examen.Id
                //            select p).ToList(),
                ExamenPreguntas = (from p in db.ExamenPregunta
                                   where p.ExamenId == examen.Id
                                   select p).ToList()
            };
            dto.Alumnos = (from aux in dto.ExamenPreguntas
                           select aux.Alumno).Distinct().ToList();
            dto.Preguntas = (from aux in dto.ExamenPreguntas
                             select aux.Pregunta).Distinct().ToList();

            return View("Calificar", dto);
        }

        // GET: Examen/Create
        public ActionResult Create()
        {
            var lista = new ExamenDto();
            //lista.Usuarios = db.Usuarios.ToList();
            lista.Alumnos = db.Alumnos.ToList();
            lista.Preguntas = db.Preguntas.ToList();
            lista.FechaExamen = new DateTime();
            return View(lista);
        }

        // POST: Examen/Create
        [HttpPost]
        public ActionResult Create(ExamenDto examen)
        {
            try
            {
                //if (!examen.FechaExamen.ToString().Contains("01/01/0001"))
                //{
                //    var fecha = Request.Form["FechaExamen"];
                //    if (fecha != null)
                //    {
                //        var vecFecha = fecha.Split('/');
                //        examen.FechaExamen = new DateTime(Convert.ToInt32(vecFecha[2]), Convert.ToInt32(vecFecha[1]), Convert.ToInt32(vecFecha[0]));
                //    }
                //}

                var preguntas = db.Preguntas.ToList();
                var alumnos = db.Alumnos.ToList();
                //var usuarios = db.Usuarios.ToList();

                foreach (var item in examen.AlumnosSelected != null ? examen.AlumnosSelected : new List<int>())
                {
                    alumnos.Where(p => p.Id == item).SingleOrDefault().IsChecked = true;
                }
                //foreach (var item in examen.UsuariosSelected != null ? examen.UsuariosSelected : new List<int>())
                //{
                //    usuarios.Where(p => p.Id == item).SingleOrDefault().IsChecked = true;
                //}
                foreach (var item in examen.PreguntasSelected != null ? examen.PreguntasSelected : new List<int>())
                {
                    preguntas.Where(p => p.Id == item).SingleOrDefault().IsChecked = true;
                }
                examen.Alumnos = alumnos;
                //examen.Usuarios = usuarios;
                examen.Preguntas = preguntas;
                List<Examen> examenes = db.Examens.ToList();
                if (examen.AlumnosSelected == null)
                {
                    ViewBag.mensaje = "Seleccione por lo menos un alumno";
                    return View(examen);
                }
                if (examen.UsuariosSelected == null)
                {
                    ViewBag.mensaje = "Seleccione por lo menos un usuario";
                    return View(examen);
                }
                if (examen.PreguntasSelected == null)
                {
                    ViewBag.mensaje = "Seleccione por lo menos una pregunta";
                    return View(examen);
                }
                if (examen.Descripcion == null)
                {
                    ViewBag.mensaje = "Escriba una breve descripción del examen";
                    return View(examen);
                }
                if (examen.FechaExamen == null || examen.FechaExamen < DateTime.Today)
                {
                    ViewBag.mensaje = "Asigne una fecha válida";
                    return View(examen);
                }
                Examen ex = new Examen
                {
                    Descripcion = examen.Descripcion,
                    FechaExamen = examen.FechaExamen,
                };
                db.Examens.Add(ex);
                db.SaveChanges();
                foreach (var item in examen.UsuariosSelected)
                {
                    db.ExamenUsuarios.Add(new ExamenUsuario
                    {
                        ExamenId = ex.Id,
                        ApplicationUserID = item
                    });
                }
                for (int i = 0; i < examen.PreguntasSelected.Count; i++)
                {
                    for (int j = 0; j < examen.PreguntasSelected.Count; j++)
                    {
                        db.ExamenPregunta.Add(new ExamenPregunta
                        {
                            ExamenId = ex.Id,
                            AlumnoId = examen.AlumnosSelected[j],
                            PreguntaId = examen.PreguntasSelected[i]
                        });
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = "Error: " + ex.Message + " Traza: " + ex.StackTrace;
                return View(examen);
            }
        }

        // GET: Examen/Edit/5
        public ActionResult Edit(int id)
        {
            var examen = db.Examens.Find(id);
            ExamenDto dto = new ExamenDto
            {
                Descripcion = examen.Descripcion,
                Id = examen.Id,
                FechaExamen = examen.FechaExamen,
                Calificado = examen.Calificado,
                //Usuarios = (from p in db.Usuarios
                //            join h in db.ExamenUsuarios
                //            on p.Id equals h.ApplicationUserID
                //            where h.ExamenId == examen.Id
                //            select p).ToList(),
                ExamenPreguntas = (from p in db.ExamenPregunta
                                   where p.ExamenId == examen.Id
                                   select p).ToList()
            };
            dto.Alumnos = (from aux in dto.ExamenPreguntas
                           select aux.Alumno).Distinct().ToList();
            dto.Preguntas = (from aux in dto.ExamenPreguntas
                             select aux.Pregunta).Distinct().ToList();
            List<Pregunta> preguntas = db.Preguntas.ToList();
            List<Alumno> alumnos = db.Alumnos.ToList();
            //List<Usuario> usuarios = db.Usuarios.ToList();
            foreach (var item in dto.Alumnos)
            {
                alumnos.Where(p => p.Id == item.Id).SingleOrDefault().IsChecked = true;
            }
            foreach (var item in dto.Usuarios)
            {
                //usuarios.Where(p => p.Id == item.Id).SingleOrDefault().IsChecked = true;
            }
            foreach (var item in dto.Preguntas)
            {
                preguntas.Where(p => p.Id == item.Id).SingleOrDefault().IsChecked = true;
            }
            dto.Alumnos = alumnos;
            //dto.Usuarios = usuarios;
            dto.Preguntas = preguntas;
            dto.FechaExamenAux = dto.FechaExamen;
            return View(dto);
        }

        // POST: Examen/Edit/5
        [HttpPost]
        public ActionResult Edit(ExamenDto examen)
        {

            if (examen.FechaExamen.ToString().Contains("1/1/0001"))
            {
                examen.FechaExamen = examen.FechaExamenAux;
            }
            var preguntas = db.Preguntas.ToList();
            var alumnos = db.Alumnos.ToList();
            //var usuarios = db.Usuarios.ToList();

            foreach (var item in examen.AlumnosSelected != null ? examen.AlumnosSelected : new List<int>())
            {
                alumnos.Where(p => p.Id == item).SingleOrDefault().IsChecked = true;
            }
            foreach (var item in examen.UsuariosSelected != null ? examen.UsuariosSelected : new List<int>())
            {
                //usuarios.Where(p => p.Id == item).SingleOrDefault().IsChecked = true;
            }
            foreach (var item in examen.PreguntasSelected != null ? examen.PreguntasSelected : new List<int>())
            {
                preguntas.Where(p => p.Id == item).SingleOrDefault().IsChecked = true;
            }
            examen.Alumnos = alumnos;
            //examen.Usuarios = usuarios;
            examen.Preguntas = preguntas;
            if (examen.AlumnosSelected == null)
            {
                ViewBag.mensaje = "Seleccione por lo menos un alumno";
                return View(examen);
            }
            if (examen.UsuariosSelected == null)
            {
                ViewBag.mensaje = "Seleccione por lo menos un usuario";
                return View(examen);
            }
            if (examen.PreguntasSelected == null)
            {
                ViewBag.mensaje = "Seleccione por lo menos una pregunta";
                return View(examen);
            }
            if (examen.FechaExamen == null || examen.FechaExamen < DateTime.Today)
            {
                ViewBag.mensaje = "Asigne una fecha válida";
                return View(examen);
            }


            try
            {
                Examen ex = db.Examens.Find(examen.Id);
                ex.Descripcion = examen.Descripcion;
                ex.FechaExamen = examen.FechaExamen;
                db.SaveChanges();
                var ExamenPreguntas = db.ExamenPregunta.Where(e => e.ExamenId == ex.Id).ToList();
                var ExamenAlumnos = db.ExamenPregunta.Where(e => e.ExamenId == ex.Id).ToList();
                var ExamenUsuarios = db.ExamenUsuarios.Where(e => e.ExamenId == ex.Id).ToList();
                foreach (var item in ExamenPreguntas)
                {
                    db.ExamenPregunta.Remove(item);
                }
                foreach (var item in ExamenAlumnos)
                {
                    db.ExamenPregunta.Remove(item);
                }
                foreach (var item in ExamenUsuarios)
                {
                    db.ExamenUsuarios.Remove(item);
                }
                db.SaveChanges();
                foreach (var item in examen.UsuariosSelected)
                {
                    db.ExamenUsuarios.Add(new ExamenUsuario
                    {
                        ExamenId = ex.Id,
                        ApplicationUserID = item
                    });
                }

                for (int i = 0; i < examen.PreguntasSelected.Count; i++)
                {
                    for (int j = 0; j < examen.AlumnosSelected.Count; j++)
                    {
                        db.ExamenPregunta.Add(new ExamenPregunta
                        {
                            ExamenId = ex.Id,
                            AlumnoId = examen.AlumnosSelected[j],
                            PreguntaId = examen.PreguntasSelected[i]
                        });
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(examen);
            }
        }

        // GET: Examen/Delete/5
        public ActionResult Delete(int id)
        {
            ExamenDto dto = (from ex in db.Examens
                             where ex.Id == id
                             select new ExamenDto
                             {
                                 Id = ex.Id,
                                 Descripcion = ex.Descripcion,
                                 FechaExamen = ex.FechaExamen,
                             }
                             ).SingleOrDefault();
            return View(dto);
        }

        // POST: Examen/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                db.Examens.Remove(db.Examens.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult ListarExamenes(string sidx, string sord, int page, int rows)
        {

            int pageIndex = page - 1;
            int pageSize = rows;

            IEnumerable<ExamenDto> examenes = null;

            if (sord.ToUpper() == "DESC")
            {
                examenes = (from ex in db.Examens
                            select new ExamenDto
                            {
                                Alumnos = (from p in db.Alumnos
                                           join h in db.ExamenPregunta
                                           on p.Id equals h.AlumnoId
                                           where h.ExamenId == ex.Id
                                           select p
                                             ).ToList(),
                                //Usuarios = (from p in db.Usuarios
                                //            join h in db.ExamenUsuarios
                                //            on p.Id equals h.ApplicationUserID
                                //            where h.ExamenId == ex.Id
                                //            select p
                                //             ).ToList(),
                                Preguntas = (from p in db.Preguntas
                                             join h in db.ExamenPregunta
                                             on p.Id equals h.PreguntaId
                                             where h.ExamenId == ex.Id
                                             select p
                                             ).ToList(),
                                ExamenPreguntas = (from p in db.ExamenPregunta
                                                   where p.ExamenId == ex.Id
                                                   select p
                                          ).ToList(),
                                Id = ex.Id,
                                Descripcion = ex.Descripcion,
                                FechaExamen = ex.FechaExamen,
                                Calificado = ex.Calificado
                            }).OrderByDescending(e => e.Descripcion).ToList().Distinct();
            }
            else
            {
                examenes = (from ex in db.Examens
                            select new ExamenDto
                            {
                                Alumnos = (from p in db.Alumnos
                                           join h in db.ExamenPregunta
                                           on p.Id equals h.AlumnoId
                                           where h.ExamenId == ex.Id
                                           select p
                                             ).ToList(),
                                //Usuarios = (from p in db.Usuarios
                                //            join h in db.ExamenUsuarios
                                //            on p.Id equals h.ApplicationUserID
                                //            where h.ExamenId == ex.Id
                                //            select p
                                //             ).ToList(),
                                Preguntas = (from p in db.Preguntas
                                             join h in db.ExamenPregunta
                                             on p.Id equals h.PreguntaId
                                             where h.ExamenId == ex.Id
                                             select p
                                             ).ToList(),
                                ExamenPreguntas = (from p in db.ExamenPregunta
                                                   where p.ExamenId == ex.Id
                                                   select p
                                          ).ToList(),
                                Id = ex.Id,
                                Descripcion = ex.Descripcion,
                                FechaExamen = ex.FechaExamen,
                                Calificado = ex.Calificado
                            }).OrderBy(e => e.Descripcion).ToList().Distinct();
            }

            int totalRecords = db.Examens.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var jsonR = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = examenes.Select(e => new
                {
                    e.Id,
                    e.Descripcion,
                    e.FechaExamen,
                    e.Calificado
                })
            };

            return Json(jsonR, JsonRequestBehavior.AllowGet);
        }

    }
}

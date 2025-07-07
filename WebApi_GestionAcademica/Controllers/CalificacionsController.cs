using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using WebApi_GestionAcademica.Models;

namespace WebApi_GestionAcademica.Controllers
{
    [RoutePrefix("api/calificaciones")]
    public class CalificacionesController : ApiController
    {
        private SistemaGestionAcademicaContext db = new SistemaGestionAcademicaContext();

        // ✅ GET: api/calificaciones/estudiante/{estudianteId}
        [HttpGet]
        [Route("estudiante/{estudianteId}")]
        public IHttpActionResult GetCalificacionesPorEstudiante(int estudianteId)
        {
            var calificaciones = db.Calificaciones
                .Include(c => c.Asignacion)
                .Include(c => c.Asignacion.Curso)
                .Where(c => c.Asignacion.EstudianteID == estudianteId)
                .Select(c => new
                {
                    Curso = c.Asignacion.Curso.Codigo + " - " + c.Asignacion.Curso.Nombre,
                    c.Nota1,
                    c.Asistencia1,
                    c.Nota2,
                    c.Asistencia2,
                    c.Suplementario,
                    c.Observacion
                })
                .ToList();

            return Ok(calificaciones);
        }

        // ✅ GET: api/calificaciones
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCalificaciones()
        {
            var calificaciones = db.Calificaciones
                .Include(c => c.Asignacion)
                .Include(c => c.Asignacion.Estudiante)
                .Include(c => c.Asignacion.Curso)
                .ToList();

            return Ok(calificaciones);
        }

        // ✅ GET: api/calificaciones/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCalificacion(int id)
        {
            var calificacion = db.Calificaciones
                .Include(c => c.Asignacion)
                .Include(c => c.Asignacion.Estudiante)
                .Include(c => c.Asignacion.Curso)
                .FirstOrDefault(c => c.CalificacionID == id);

            if (calificacion == null)
                return NotFound();

            return Ok(calificacion);
        }

        // ✅ POST: api/calificaciones
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostCalificacion(Calificacion calificacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Calificaciones.Add(calificacion);
            db.SaveChanges();

            return Created($"api/calificaciones/{calificacion.CalificacionID}", calificacion);
        }

        // ✅ PUT: api/calificaciones/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult PutCalificacion(int id, Calificacion calificacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != calificacion.CalificacionID)
                return BadRequest("ID no coincide");

            db.Entry(calificacion).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(calificacion);
        }

        // ✅ DELETE: api/calificaciones/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteCalificacion(int id)
        {
            var calificacion = db.Calificaciones.Find(id);
            if (calificacion == null)
                return NotFound();

            db.Calificaciones.Remove(calificacion);
            db.SaveChanges();

            return Ok(calificacion);
        }

        // ✅ Liberar recursos del contexto
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}

using System.Linq;
using System.Web.Http;
using WebApi_GestionAcademica.Models;

namespace WebApi_GestionAcademica.Controllers
{
    [RoutePrefix("api/carreras")]
    public class CarrerasController : ApiController
    {
        private SistemaGestionAcademicaContext db = new SistemaGestionAcademicaContext();

        // GET: api/carreras
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCarreras()
        {
            // Devolver solo ID y Nombre (evita estudiantes)
            var carreras = db.Carreras
                             .Select(c => new
                             {
                                 c.CarreraID,
                                 c.Nombre
                             })
                             .ToList();

            return Ok(carreras);
        }

        // GET: api/carreras/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCarrera(int id)
        {
            var carrera = db.Carreras
                            .Where(c => c.CarreraID == id)
                            .Select(c => new
                            {
                                c.CarreraID,
                                c.Nombre
                            })
                            .FirstOrDefault();

            if (carrera == null)
                return NotFound();

            return Ok(carrera);
        }

        // GET: api/carreras/{id}/estudiantes
        [HttpGet]
        [Route("{id:int}/estudiantes")]
        public IHttpActionResult GetEstudiantesPorCarrera(int id)
        {
            var estudiantes = db.Estudiantes
                                .Where(e => e.CarreraID == id)
                                .Select(e => new
                                {
                                    e.EstudianteID,
                                    e.Nombres,
                                    e.Apellidos,
                                    e.Cedula,
                                    e.CorreoUTA
                                })
                                .ToList();

            return Ok(estudiantes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}

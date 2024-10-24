using api_ver2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_ver2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class StudentsController : ControllerBase
    {
        private readonly List<Student>? _students;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_students);
        }

        [HttpGet("{id?}")]
        public IActionResult GetAll(int? id)
        {
            if (id == null) return NotFound();

            var student = _students?.Find(x => x.Id == id);

            if (student == null) return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            return Created("Test", student);
        }

        [HttpPost("primitive-parameters")]
        public IActionResult PrimitiveTypes([FromBody] int some)
        {
            return Created("Test", some);
        }
    }
}
    
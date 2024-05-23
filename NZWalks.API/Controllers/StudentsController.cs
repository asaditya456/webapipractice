using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAlStudents()
        {
            string[] studentNames = new string[] { "John", "Jane", "Mark", "Mark", "Emily", "David" };
            return Ok(studentNames);
        }
    }
}

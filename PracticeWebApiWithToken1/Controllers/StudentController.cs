using DAL;
using Microsoft.AspNetCore.Mvc;
using PracticeWebApiWithToken1.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PracticeWebApiWithToken1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _student;
        public StudentController(IStudent student)
        {
                _student = student;
        }
        // GET: api/<StudentController>
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            try
            {
                if (_student.GetStudents() != null)
                {
                    return Ok(_student.GetStudents());
                }
                else
                {
                    return NotFound("Record not Exists");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieve Data");
            }
            //return _student.GetStudents();
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            var result = _student.GetStudentById(id);
            try
            {
                if (result != null)
                {
                    return _student.GetStudentById(id);
                }
                else
                {
                    return NotFound("Record not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieve Data");

            }

        }

        // POST api/<StudentController>
        [HttpPost]
        public ActionResult Post([FromBody] Student student)
        {
            try
            {
                var students = _student.AddStudent(student);
                if (students == null)
                {
                    return BadRequest();
                }
                else
                {
                   return Ok(students);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieve Data");

            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public ActionResult<Student> Put(int id, [FromBody] Student student)
        {
            var result = _student.UpdateStudent(student);
            try
            {
                if (result != null)
                {
                   return Ok(result); 
                }
                else
                {
                    return BadRequest("Record not exist");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error internal server");
            }
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _student.DeleteStudent(id);
            try
            {
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Record not exist");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Occur in delete Statement");
            }
        }
    }
}

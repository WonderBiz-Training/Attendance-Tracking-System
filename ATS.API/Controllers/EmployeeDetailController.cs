using ATS.DTO;
using ATS.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailController : ControllerBase
    {

        private readonly IEmployeeDetailServices _employeeDetailServices;
        public EmployeeDetailController(IEmployeeDetailServices employeeDetailServices)
        {
            _employeeDetailServices = employeeDetailServices;
        }


        // GET: api/<EmployeeDetailController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetEmployeeDetailDto>>> Get()
        {
            try
            {
                var employeeInfos = await _employeeDetailServices.GetEmployeeDetailsAsync();
                return Ok(employeeInfos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/<EmployeeDetailController>
        [HttpGet("filters")]
        public async Task<ActionResult<IEnumerable<GetEmployeeDetailDto>>> GetWithFilter([FromQuery] string? firstName, string? lastName, string? employeeCode, long designationId, long genderId, int start, int pageSize)
        {
            try
            {
                var employeeInfos = await _employeeDetailServices.GetEmployeeDetailsWithFilter(firstName, lastName, employeeCode, designationId, genderId, start, pageSize);
                return Ok(employeeInfos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<EmployeeDetailController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GetEmployeeDetailDto>>> Get(long id)
        {
            try
            {
                var employeeInfos = await _employeeDetailServices.GetEmployeeDetailAsync(id);
                return Ok(employeeInfos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<EmployeeDetailController>/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<GetEmployeeDetailDto>>> GetByUserId(long userId)
        {
            try
            {
                var employeeInfo = await _employeeDetailServices.GetEmployeeDetailByUserId(userId);
                return Ok(employeeInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<EmployeeDetailController>
        [HttpPost]
        public async Task<ActionResult<GetEmployeeDetailDto>> Post([FromBody] CreateEmployeeDetailDto createEmployeeDetailDto)
        {
            try
            {
                var createdEmployeeInfo = await _employeeDetailServices.CreateEmployeeDetailAsync(createEmployeeDetailDto);
                return CreatedAtAction(nameof(Get), new { id = createdEmployeeInfo.Id }, createdEmployeeInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<EmployeeDetailController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetEmployeeDetailDto>> Put(long id, [FromBody] UpdateEmployeeDetailDto updateEmployeeDetailDto)
        {
            try
            {
                var updatedEmployeeInfo = await _employeeDetailServices.UpdateEmployeeDetailAsync(id, updateEmployeeDetailDto);
                return Ok(updatedEmployeeInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<EmployeeDetailController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                var deleted = await _employeeDetailServices.DeleteEmployeeDetailAsync(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("face-encoding")]
        public async Task<ActionResult<IEnumerable<GetFaceEncodingDto>>> GetFaceEncoding()
        {
            try
            {
                var employeeInfos = await _employeeDetailServices.GetFaceEncodingsAsync();
                return Ok(employeeInfos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

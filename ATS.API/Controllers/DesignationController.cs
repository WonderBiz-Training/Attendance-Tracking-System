using ATS.DTO;
using ATS.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationServices _designationServices;
        public DesignationController(IDesignationServices DesignationServices)
        {
            _designationServices = DesignationServices;
        }

        // GET: api/<DesignationController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDesignationDto>>> Get()
        {
            try
            {
                var designations = await _designationServices.GetAllDesignationsAsync();
                return Ok(designations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<DesignationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDesignationDto>> Get(long id)
        {
            try
            {
                var Designation = await _designationServices.GetDesignationByIdAsync(id);
                return Ok(Designation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<DesignationController>
        [HttpPost]
        public async Task<ActionResult<GetDesignationDto>> Post([FromBody] CreateDesignationDto designationDto)
        {
            try
            {
                var createdDesignation = await _designationServices.CreateDesignationAsync(designationDto);
                return CreatedAtAction(nameof(Get), new { id = createdDesignation.Id }, createdDesignation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<DesignationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetDesignationDto>> Put(long id, [FromBody] UpdateDesignationDto DesignationDto)
        {
            try
            {
                var updatedDesignation = await _designationServices.UpdateDesignationAsync(id, DesignationDto);
                return Ok(updatedDesignation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<DesignationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                var deleted = await _designationServices.DeleteDesignationAsync(id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

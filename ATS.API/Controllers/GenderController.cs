using ATS.DTO;
using ATS.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderServices _genderServices;

        public GenderController(IGenderServices genderServices)
        {
            _genderServices = genderServices;
        }

        // GET: api/<GenderController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetGenderDto>>> Get()
        {
            try
            {
                var res = await _genderServices.GetAllGendersAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<GenderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetGenderDto>> Get(int id)
        {
            try
            {
                var res = await _genderServices.GetGenderAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<GenderController>
        [HttpPost]
        public async Task<ActionResult<GetGenderDto>> Post([FromBody] CreateGenderDto genderDto)
        {
            try
            {
                var res = await _genderServices.CreateGenderAsync(genderDto);
                return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<GenderController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetGenderDto>> Put(int id, [FromBody] UpdateGenderDto genderDto)
        {
            try
            {
                var res = await _genderServices.UpdateGenderAsync(id, genderDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<GenderController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var res = await _genderServices.DeleteGenderAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

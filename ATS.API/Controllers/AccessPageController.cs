using ATS.DTO;
using ATS.IServices;
using ATS.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessPageController : ControllerBase
    {
        private readonly IAccessPageServices _accessPageServices;

        public AccessPageController(IAccessPageServices accessPageServices)
        {
            _accessPageServices = accessPageServices;
        }

        // GET: api/<AccessPageController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAccessPageDto>>> Get()
        {
            try
            {
                var res = await _accessPageServices.GetAllAccessPagesAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<AccessPageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAccessPageDto>> Get(long id)
        {
            try
            {
                var res = await _accessPageServices.GetAccessPageAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET api/<AccessPageController>/role/5
        [HttpGet("role/{roleId}")]
        public async Task<ActionResult<GetAccessPageDto>> GetByRole(long roleId)
        {
            try
            {
                var res = await _accessPageServices.GetAccessPageByRoleId(roleId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<AccessPageController>
        [HttpPost]
        public async Task<ActionResult<GetAccessPageDto>> Post([FromBody] CreateAccessPageDto accessPageDto)
        {
            try
            {
                var res = await _accessPageServices.CreateAccessPageAsync(accessPageDto);
                return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<AccessPageController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetAccessPageDto>> Put(long id, [FromBody] UpdateAccessPageDto accessPageDto)
        {
            try
            {
                var res = await _accessPageServices.UpdateAccessPageAsync(id, accessPageDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("multiple")]
        public async Task<ActionResult<GetAccessPageDto>> Put([FromBody] List<UpdateMulitpleAccessPageDto> accessPageDto)
        {
            try
            {
                var res = await _accessPageServices.UpdateMultipleAccessPageAsync(accessPageDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        // DELETE api/<AccessPageController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                var res = await _accessPageServices.DeleteAccessPageAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

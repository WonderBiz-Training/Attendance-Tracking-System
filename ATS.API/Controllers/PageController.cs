using ATS.DTO;
using ATS.IServices;
using ATS.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly IPageServices _pageServices;

        public PageController(IPageServices pageServices)
        {
            _pageServices = pageServices;
        }

        // GET: api/<PageController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPageDto>>> Get()
        {
            try
            {
                var res = await _pageServices.GetAllPagesAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // GET api/<PageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPageDto>> Get(long id)
        {
            try
            {
                var res = await _pageServices.GetPageAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<PageController>
        [HttpPost]
        public async Task<ActionResult<GetPageDto>> Post([FromBody] CreatePageDto pageDto)
        {
            try
            {
                var res = await _pageServices.CreatePageAsync(pageDto);
                return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<PageController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetPageDto>> Put(long id, [FromBody] UpdatePageDto pageDto)
        {
            try
            {
                var res = await _pageServices.UpdatePageAsync(id, pageDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<PageController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                var res = await _pageServices.DeletePageAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

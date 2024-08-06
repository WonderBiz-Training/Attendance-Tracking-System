using ATS.DTO;
using ATS.IServices;
using ATS.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceLogController : ControllerBase
    {
        private readonly IAttendanceLogServices _attendanceLogServices;

        public AttendanceLogController(IAttendanceLogServices attendanceLogServices)
        {
            _attendanceLogServices = attendanceLogServices;
        }

        // GET: api/<AttendanceLogController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAttendanceLogDto>>> Get()
        {
            try
            {
                var res = await _attendanceLogServices.GetAllAttendanceLogsAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<AttendanceLogController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAttendanceLogDto>> Get(int id)
        {
            try
            {
                var res = await _attendanceLogServices.GetAttendanceLogAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<AttendanceLogController>
        [HttpPost]
        public async Task<ActionResult<GetAttendanceLogDto>> Post([FromBody] CreateAttendanceLogDto attendanceLogDto)
        {
            try
            {
                var res = await _attendanceLogServices.CreateAttendanceLogAsync(attendanceLogDto);
                return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<AttendanceLogController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetAttendanceLogDto>> Put(int id, [FromBody] UpdateAttendanceLogDto attendanceLogDto)
        {
            try
            {
                var res = await _attendanceLogServices.UpdateAttendanceLogAsync(id, attendanceLogDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<AttendanceLogController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var res = await _attendanceLogServices.DeleteAttendanceLogAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

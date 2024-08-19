using ATS.DTO;
using ATS.IServices;
using ATS.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> Get()
        {
            try
            {
                var res = await _userServices.GetAllUsersAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> Get(long id)
        {
            try
            {
                var res = await _userServices.GetUserAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<GetUserDto>> Post([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var res = await _userServices.CreateUserAsync(createUserDto);
                return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetUserDto>> Put(long id, [FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                var res = await _userServices.UpdateUserAsync(id, updateUserDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                var res = await _userServices.DeleteUserAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost("signup")]
        public async Task<ActionResult<GetSignUpDto>> Post([FromBody] SignUpDto signUpDto)
        {
            try
            {
                var res = await _userServices.SignUpUserAsync(signUpDto);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<UserController>/log-in
        [HttpPost("log-in")]
        public async Task<ActionResult<GetUserDto>> Post([FromBody] LogInDto logInDto)
        {
            try
            {
                var res = await _userServices.LogInUserAsync(logInDto);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgFlowManager.API.ModelViews;
using ProgFlowManager.API.Tools;
using ProgFlowManager.BLL.Models.Programs;
using ProgFlowManager.BLL.Models.Users;
using ProgFlowManager.BLL.Tools;
using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Interfaces.Users;
using ProgFlowManager.DAL.Models;
using ProgFlowManager.DAL.Models.Programs;
using ProgFlowManager.DAL.Models.Users;

namespace ProgFlowManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IUserService _userService;
        private readonly TokenManager _tokenManager;

        public UserController(IDataService dataService, IUserService userService, TokenManager tokenManager)
        {
            _dataService = dataService;
            _userService = userService;
            _tokenManager = tokenManager;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterForm registerForm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Console.WriteLine("Register form Password (Before BCrypt) : " + registerForm.PasswordHash);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerForm.PasswordHash);
                registerForm.PasswordHash = hashedPassword;
                Console.WriteLine("Register form Password (After BCrypt) : " + registerForm.PasswordHash);
                Console.WriteLine("To Model :" + registerForm.ToModel<User, UserRegisterForm>().PasswordHash);
                if (_dataService.Create(registerForm.ToModel<Data, UserRegisterForm>()) &&
                        _userService.Register(registerForm.ToModel<User, UserRegisterForm>(_dataService.GetLastId())))
                    return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return BadRequest();
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginForm loginForm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                if (!BCrypt.Net.BCrypt.Verify(loginForm.Password, _userService.Login(loginForm.Email).PasswordHash))
                    return BadRequest("Invalid Password");
                return Ok(_tokenManager.GenerateToken(_userService.Login(loginForm.Email)));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(GetUserDTOs());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (GetUserDTOs().First(u => u.Id == id) is null) return NotFound();

            return Ok(GetUserDTOs().First(u => u.Id == id));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try { if (_userService.Delete(id) && _dataService.Delete(id)) return Ok(); }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return NotFound();
        }
        private IEnumerable<UserDTO> GetUserDTOs()
        {
            IEnumerable<UserDTO> userDTOs = _userService.Models.ToDTO<UserDTO, User>()
                                                               .MergeWith(_dataService.Models.ToDTO<UserDTO, Data>());

            return userDTOs;
        }
    }
}

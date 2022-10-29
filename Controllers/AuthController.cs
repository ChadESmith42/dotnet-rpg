using dotnet_rpg.DTOs.User;
using dotnet_rpg.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Produces("application/json")]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _authRepo;

    public AuthController(IAuthRepository authRepo)
    {
      _authRepo = authRepo;
    }

    [HttpPost("register")]
    public async Task<ActionResult<int>> Register([FromBody] UserRegisterDto request)
    {
      var response = await _authRepo.Register(
          new User { Username = request.Username }, request.Password
      );
      return response == 0 ? BadRequest() : Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<int>> Login([FromBody] UserLoginDto request)
    {
      var response = await _authRepo.Login(
          request.Username, request.Password
      );
      return response == "The username and password combination is invalid." ? NotFound() : Ok(response);
    }
  }
}

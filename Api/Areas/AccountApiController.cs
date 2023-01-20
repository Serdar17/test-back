using Api.Models;
using AutoMapper;
using Logic.Models;
using Logic.Models.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Areas;

[ApiController]
[Route("api/account")]
public class AccountApiController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAccountManager _accountManager;
    
    public AccountApiController(IMapper mapper, IAccountManager accountManager)
    {
        _mapper = mapper;
        _accountManager = accountManager;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var user = _mapper.Map<User>(registerRequest);
        var response = await _accountManager.RegisterAsync(user);
        
        if (response.StatusCode == 200)
        {
            return Ok();
        }

        return BadRequest(new ErrorResponse()
        {
            Code = "400",
            Message = response.Description
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var user = _mapper.Map<User>(loginRequest);
        var response = await _accountManager.LogInAsync(user);
        
        if (response.StatusCode == 200)
        {
            HttpContext.Session.SetString("Token", response.Token);
            return Ok();
        }

        return BadRequest(new ErrorResponse()
        {
            Code = "400",
            Message = response.Description
        });
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Remove("Token");
        return Ok();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("get-my-info")]
    public async Task<IActionResult> GetInfo()
    {
        var claims = User.Claims.ToList()[1];
        var user = _accountManager.GetUserByEmail(claims.Value);
        var userDto = _mapper.Map<ResponseDto>(user);
        return Ok(userDto);
    }
    
}
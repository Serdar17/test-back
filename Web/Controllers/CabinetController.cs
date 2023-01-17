using Logic.Models.Repository.Interfaces;
using Logic.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("cabinet")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CabinetController : Controller
{
    private readonly IAccountManager _accountManager;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public CabinetController(IAccountManager accountManager, ITokenService tokenService, IConfiguration configuration)
    {
        _accountManager = accountManager;
        _tokenService = tokenService;
        _configuration = configuration;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var token = HttpContext.Session.GetString("Token");
        if (token == null || !_tokenService.IsTokenValid(_configuration["Jwt:Key"], _configuration["Jwt:Issuer"], token))
        {
            return RedirectToAction("Login", "Account");
        }
        var claims = User.Claims.ToList()[1];
        var user = _accountManager.GetUserByEmail(claims.Value);
        return View(user);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Remove("Token");
        return RedirectToAction("Login", "Account");
    }
}
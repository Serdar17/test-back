using System.Security.Claims;
using Api.Models;
using AutoMapper;
using Logic.DbContext;
using Logic.Models;
using Logic.Models.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;


[Route("account")]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;
    private readonly IAccountManager _accountManager;
    private readonly ApplicationDbContext _dbContext;
    
    public AccountController(ILogger<AccountController> logger, 
        IMapper mapper, IAccountManager accountManager, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _mapper = mapper;
        _accountManager = accountManager;
        _dbContext = dbContext;
    }
    
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost("register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] RegisterRequest registerRequest)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(registerRequest);
            var response = await _accountManager.RegisterAsync(user);

            if (response.StatusCode == 200)
            {
                HttpContext.Session.SetString("Token", response.Token);
                return RedirectToAction("Index", "Home");
            }
            
            if (response.PhoneDescription != null)
                ModelState.AddModelError("Phone", response.PhoneDescription);
            
            if (response.Description != null)
                ModelState.AddModelError("Email", response.Description);
        }
        return View(registerRequest);
    }
    
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(loginRequest);
            var response = await _accountManager.LogInAsync(user);
            
            if (response.StatusCode == 200)
            {
                HttpContext.Session.SetString("Token", response.Token);
                return RedirectToAction("Index", "Cabinet");
            }

            ModelState.AddModelError("Phone", response.Description);
            
        }
        return View(loginRequest);
    }
}
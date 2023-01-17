using System.Security.Claims;
using Logic.Helper;
using Logic.Models.Repository.Interfaces;
using Logic.Service;

namespace Logic.Models;

public class AccountManager : IAccountManager
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    
    public AccountManager(IUserRepository userRepository, ITokenService tokenService, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<BaseResponse<ClaimsIdentity>> RegisterAsync(User user)
    {
        var userByEmail = _userRepository.GetAll().FirstOrDefault(u => u.Email == user.Email);
        var userByPhone = _userRepository.GetAll().FirstOrDefault(u => u.Phone == user.Phone);

        if (userByPhone is not null)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                StatusCode = 400,
                PhoneDescription = "Пользователь с таким номером уже существует"
            };
        }
        
        if (userByEmail is not null)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                StatusCode = 400,
                Description = "Пользователь с email уже существует"
            };
        }
        
        
        user.Password = HashPasswordHelper.HashPassword(user.Password);
        _userRepository.Create(user);
        var generatedToken = _tokenService.BuildToken(_configuration["Jwt:Key"], _configuration["Jwt:Issuer"], user);

        return new BaseResponse<ClaimsIdentity>()
        {
            StatusCode = 200,
            Description = "Авторизация прошла успешно",
            Token = generatedToken,
        };
    }

    public async Task<BaseResponse<ClaimsIdentity>> LogInAsync(User user)
    {
        var userFromDb = _userRepository.GetAll().FirstOrDefault(u => u.Phone == user.Phone);
        // Провека юзера по номеру телефона и по хэшу пароля
        if (userFromDb is null || userFromDb.Password != HashPasswordHelper.HashPassword(user.Password))
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                StatusCode = 400,
                Description = "Ошибка авторизации"
            };
        }

        _userRepository.ChangeDateTime(userFromDb);
        var generatedToken = _tokenService.BuildToken(_configuration["Jwt:Key"],
            _configuration["Jwt:Issuer"], userFromDb);
        return new BaseResponse<ClaimsIdentity>()
        {
            StatusCode = 200,
            Description = "Авторизация прошла успешно",
            Token = generatedToken
        };
    }

    public User GetUserByEmail(string email)
    {
        return _userRepository.GetAll().FirstOrDefault(u => u.Email == email);
    }
}
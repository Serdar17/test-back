using System.Security.Claims;

namespace Logic.Models.Repository.Interfaces;


public interface IAccountManager
{
    public Task<BaseResponse<ClaimsIdentity>> RegisterAsync(User user);

    public Task<BaseResponse<ClaimsIdentity>> LogInAsync(User user);

    public User GetUserByEmail(string email);

}
using Logic.DbContext;
using Logic.Models.Repository.Interfaces;

namespace Logic.Models.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<User> GetAll()
    {
        var users = _dbContext.Users.ToList();
        return users;
    }

    public bool Create(User user)
    {
        _dbContext.AddAsync(user);
        _dbContext.SaveChangesAsync();
        return true;
    }

    public void ChangeDateTime(User user)
    {
        var item = _dbContext.Users.ToList().FirstOrDefault(u => u.Email == user.Email);
        item.LastLogin = DateTime.Now;
        _dbContext.SaveChangesAsync();
    }
}
namespace Logic.Models.Repository.Interfaces;

public interface IUserRepository
{
    public List<User> GetAll();

    public bool Create(User user);

    public void ChangeDateTime(User user);
}
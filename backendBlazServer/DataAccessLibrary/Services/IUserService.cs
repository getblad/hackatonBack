using DataAccessLibrary.Models;

namespace DataAccessLibrary.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();
    void AddUser(User newUser);
    void UpdateUser(long id, User user);
    User SingleUser(long id);
    void DeleteUser(long id);
}
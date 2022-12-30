using System.Threading.Tasks;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface IUserService
{
    Task<List<User>> GetUsers();
    void AddUser(User newUser);
    void UpdateUser( User user);
    UserPublic SingleUser(int id);
    void DeleteUser(int id);
}
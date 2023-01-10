using System.Threading.Tasks;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface IUserService
{
    List<User> GetUsersSys();
    void AddUser(User newUserDto);
    void UpdateUser( User user);
    User SingleUser(int id);
    void DeleteUser(int id);
}
using System.Threading.Tasks;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface IUserService
{
    List<UserDtoAdmin> GetUsersSys();
    void AddUser(UserDtoAdmin newUserDto);
    void UpdateUser( User user);
    User SingleUser(int id);
    void DeleteUser(int id);
}
using System.Threading.Tasks;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface IUserService
{
    List<UserAdmin> GetUsersSys();
    int AddUser(UserAdmin newUser);
    void UpdateUser( User user);
    UserPublic SingleUser(int id);
    void DeleteUser(int id);
}
using System.Threading.Tasks;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface IUserService
{
    List<UserAdmin> GetUsersSys();
    void AddUser(UserAdmin newUser);
    void UpdateUser( UserAdmin user);
    UserPublic SingleUser(int id);
    void DeleteUser(int id);
}
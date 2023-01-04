using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;


namespace DataAccessLibrary.Services;

public class UserService:IUserService
{
    private readonly HpContext _context;

    public UserService(HpContext context)
    {
        _context = context;
    }
    public List<UserAdmin> GetUsersSys()
    {
        try
        {
            var usersDb = _context.Users.Where(n => n.RowStatusId == (int)StatusEnums.Active).
                ToList();
            List<UserAdmin> userAdmins = new List<UserAdmin>();
            foreach (var user in usersDb)
            {
                userAdmins.Add(_dbUserToAdmin(user));
            }
            
            return userAdmins;
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public int AddUser(UserAdmin newUser)
    {
        try
        {
            
            User addingUser = new User
            {
                
                UserFirstName = newUser.UserFirstName,
                UserSecondName = newUser.UserSecondName,
                UserAvatar = newUser.UserAvatar,
                UserEmail = newUser.UserEmail,
                TeamId = newUser.TeamId,
                CreateUserId = newUser.UpdateUser,
                CreateUser = null,
                UpdateUserId = newUser.UpdateUser,
                UpdateUser = null,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                RowStatusId = (int)StatusEnums.Active,
                RowStatus = null,
                Team = null,

            };
            var a = _context.Add(addingUser);
            var id = addingUser.UserId;
            _context.SaveChanges();
            return id;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;

        }
    }

    public void UpdateUser(User user)
    {
        
            var local = _context.Set<User>().Local.FirstOrDefault(entry => entry.UserId.Equals(user.UserId));
            // check if local is not null
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            else
            {
                throw new Exception("No such user");
            }

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        
        
    }

    public UserPublic SingleUser(int id)
    {
        try
        {
            var userDb = _context.Users.Include(n => n.Team)
                .FirstOrDefault(n => n.UserId == id && n.RowStatusId == (int)StatusEnums.Active);
            if (userDb == null)
            {
                throw new Exception("No such user");
            }
            var user = _dbUserToPublic(userDb);
            
            return user;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteUser(int id)
    {
        try
        {
            var user = _context.Users.Find(id);
            user.RowStatusId = (int)StatusEnums.Delete;
            _context.SaveChanges();
            
        }
        catch(Exception e)
        {
            Console.WriteLine(e);

        }
    }

    private UserAdmin _dbUserToAdmin(User? user)
    {
        return new UserAdmin()
        {
            UserId = user.UserId,
            UserFirstName = user.UserFirstName,
            UserSecondName = user.UserSecondName,
            UserEmail = user.UserEmail,
            UserAvatar = user.UserAvatar,
            TeamId = user.Team?.TeamId,
            CreationTime = user.CreateTime,
            UpdateTime = user.UpdateTime,
            UpdateUser = user.UpdateUser.UserId,
        };
    }
    private UserPublic _dbUserToPublic(User user)
    {
        return new UserPublic()
        {
            UserFirstName = user.UserFirstName,
            UserSecondName = user.UserSecondName,
            UserEmail = user.UserEmail,
            UserAvatar = user.UserAvatar,
            TeamId = user.Team?.TeamId
        };
    }
}
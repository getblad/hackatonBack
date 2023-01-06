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

    public void AddUser(UserAdmin newUser)
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
             
            var a = _context.Users.Add(addingUser);
            
            _context.SaveChanges();
           

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;

        }
    }

    public  void UpdateUser(UserAdmin user)
    {
            
            // var local = _context.Set<User>().Local.FirstOrDefault(entry => entry.UserId == user.UserId);
            var local =  _context.Users.FirstOrDefault(entry => entry.UserId == user.UserId);
            // check if local is not null
            if (local == null)
            {
                // detach
                // var a = _context.Entry(local);
                // _context.Entry(local).State = EntityState.Detached;
                throw new Exception("No such user");
            }
            

            var dbUser = new User()
            {
                UserId = user.UserId,
                UserFirstName = user.UserFirstName,
                UserSecondName = user.UserSecondName,
                UserAvatar = user.UserAvatar,
                UserEmail = user.UserEmail,
                TeamId = user.TeamId,
                CreateUserId = user.UpdateUser,
                CreateUser = local.CreateUser,
                UpdateUserId = user.UpdateUser,
                UpdateUser = local.UpdateUser,
                CreateTime = local.CreateTime,
                UpdateTime = DateTime.Now,
                RowStatusId = local.RowStatusId,
                RowStatus = local.RowStatus,
                Team = local.Team,
            };
            var entry = _context.Entry(local);
            entry.CurrentValues.SetValues(dbUser);
            entry.State = EntityState.Modified;
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
            TeamId = user.TeamId,
            CreationTime = user.CreateTime,
            UpdateTime = user.UpdateTime,
            UpdateUser = user.UpdateUserId,
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
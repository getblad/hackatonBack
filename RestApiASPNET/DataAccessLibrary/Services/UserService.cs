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
    public List<UserDtoAdmin> GetUsersSys()
    {
        try
        {
            var usersDb = _context.Users.Where(n => n.RowStatusId == (int)StatusEnums.Active).
                ToList();
            List<UserDtoAdmin> userAdmins = new List<UserDtoAdmin>();
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

    public void AddUser(UserDtoAdmin newUserDto)
    {
        try
        {
            
            User addingUser = new User
            {
                
                UserFirstName = newUserDto.UserFirstName,
                UserSecondName = newUserDto.UserSecondName,
                UserAvatar = newUserDto.UserAvatar,
                UserEmail = newUserDto.UserEmail,
                TeamId = newUserDto.TeamId,
                CreateUserId = newUserDto.UpdateUserId,
                CreateUser = null,
                UpdateUserId = newUserDto.UpdateUserId,
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

    public  void UpdateUser(User user)
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

            user.CreateUserId = local.CreateUserId;
            user.CreateUser = local.CreateUser;
            user.UpdateUser = local.UpdateUser;
            user.CreateTime = local.CreateTime;
            user.UpdateTime = DateTime.Now;
            user.RowStatus = local.RowStatus;
            user.RowStatusId = local.RowStatusId;
            user.Team = local.Team;

            
            var entry = _context.Entry(local);
            entry.CurrentValues.SetValues(user);
            entry.State = EntityState.Modified;
             _context.SaveChanges();
        
        
    }

    public User SingleUser(int id)
    {
        try
        {
            var userDb = _context.Users.Include(n => n.Team)
                .FirstOrDefault(n => n.UserId == id && n.RowStatusId == (int)StatusEnums.Active);
            if (userDb == null)
            {
                throw new Exception("No such user");
            }

            return userDb;

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

    private UserDtoAdmin _dbUserToAdmin(User? user)
    {
        return new UserDtoAdmin()
        {
            UserId = user.UserId,
            UserFirstName = user.UserFirstName,
            UserSecondName = user.UserSecondName,
            UserEmail = user.UserEmail,
            UserAvatar = user.UserAvatar,
            TeamId = user.TeamId,
            CreateTime = user.CreateTime,
            UpdateTime = user.UpdateTime,
            UpdateUserId = user.UpdateUserId,
        };
    }
    private UserDtoPublic _dbUserToPublic(User user)
    {
        return new UserDtoPublic()
        {
            UserFirstName = user.UserFirstName,
            UserSecondName = user.UserSecondName,
            UserEmail = user.UserEmail,
            UserAvatar = user.UserAvatar,
            TeamId = user.Team?.TeamId
        };
    }
}
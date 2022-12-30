using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLibrary.Services;

public class UserService:IUserService
{
    private readonly HpContext _context;

    public UserService(HpContext context)
    {
        _context = context;
    }
    public Task<List<User>> GetUsers()
    {
        try
        {
            
            return Task.FromResult(_context.Users.ToList());
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddUser(User newUser)
    {
        try
        {
            _context.Add(newUser);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
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
            var user = _context.Users.Where(n => n.UserId == id).Select(n => new UserPublic()
            {
                UserFirstName = n.UserFirstName,
                UserSecondName = n.UserSecondName,
                UserEmail = n.UserEmail,
                RoleName = n.Role.RoleName
            })
                .FirstOrDefault();
            
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
            User user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            
        }
    }
}
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace DataAccessLibrary.Services.ServicesImplementation;

public class UserService:IUserService
{
    private HpContext _context;

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

    public void UpdateUser(long id, User user)
    {
        try
        {
            var local = _context.Set<User>().Local.FirstOrDefault(entry => entry.UserId.Equals(user.UserId));
            // check if local is not null
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
        catch
        {
            throw;
        }
    }

    public User SingleUser(long id)
    {
        try
        {
            User user = _context.Users.Find(id);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteUser(long id)
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
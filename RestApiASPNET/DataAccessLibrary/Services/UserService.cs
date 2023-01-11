using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using System;


namespace DataAccessLibrary.Services;

public class UserService:IUserService
{
    private readonly HpContext _context;
    

    public UserService(HpContext context)
    {
        _context = context;
    }
    public List<User> GetUsersSys()
    {
        try
        {
            var usersDb = _context.Users.Where(n => n.RowStatusId == (int)StatusEnums.Active).
                ToList();
            
            
            return usersDb!;
        }
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddUser(User newUser)
    {
        try
        {
            var a = _context.Users.Add(newUser);
            _context.SaveChanges();
           

        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw;

        }
    }

    public  void UpdateUser(User user)
    {
            
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

    
    
}
using DataAccessLibrary.CustomExceptions;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Services;

public class DbService<TModel>:IDbService<TModel> where TModel : class, IStatus
{
    private readonly HpContext _context;
    private readonly DbSet<TModel> _dbSet;

    public DbService(HpContext context)
    {
        _context = context;
        _dbSet = _context.Set<TModel>();
    }


    public TModel Create(TModel model)
    {
        try
        {
            _dbSet.Add(model);
            _context.SaveChanges();
            return model;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public TModel Update(int id,TModel model)
    {
        var local =  _dbSet.Find(id);
            
        // check if local is not null
        if (local == null)
        {
            // detach
            
            throw new NotFoundException("No such item");
        }
        _context.Entry(local).CurrentValues.SetValues(model);
        _context.SaveChanges();
        return local;

    }

    public List<TModel> GetAll()
    {
        var dbValues = _dbSet.Where(e => e.RowStatusId == (int)StatusEnums.Active)
            .ToList();
               
        return dbValues;
    }
    public void Delete(int id)
    {
        try
        {
            var entry = _dbSet.Find(id);
            if (entry != null) entry.RowStatusId = (int)StatusEnums.Delete;
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


    }
    public TModel GetOne(int id)
    {
        var entry = _dbSet.Find(id);
        switch (entry)
        {
            case { RowStatusId: (int)StatusEnums.Active }:
                return entry;
            case null:
                break;
        }

        throw new ("No such object");

    }
}
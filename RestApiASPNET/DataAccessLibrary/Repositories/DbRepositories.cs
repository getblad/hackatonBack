using System.Linq.Expressions;
using DataAccessLibrary.CustomExceptions;
using DataAccessLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace DataAccessLibrary.Repositories;

public class DbRepositories<TModel>:IDbRepositories<TModel> where TModel : class, IStatus  
{
    private readonly HpContext _context;
    private  IQueryable<TModel> _query;

    public DbRepositories(HpContext context)
    {
        _context = context;
        _query = _context.Set<TModel>();

    }


    public async Task<TModel> Create(TModel model)
    {
        try
        {
            await ((DbSet<TModel>)_query).AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        catch (DbUpdateException exception) when (exception.InnerException is SqlException)
        {
            switch (exception.InnerException.HResult)
            {
                case -2146232060:
                    throw new AlreadyExistingException("");
            }

            WriteLine();
            throw;
        }
        catch (Exception e)
        {
            WriteLine(e);
            throw;
        }
    }


    public async Task<TModel> Update<T>(int id, T model)
    {
        try
        {
            var local =  await ((DbSet<TModel>)_query).FindAsync(id);
            // check if local is not null
            if (local == null)
            {
                // detach
                throw new NotFoundException("No such item");
            }

            if (model != null) _context.Entry((object)local).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return local;
        }
        catch (Exception e)
        {
            WriteLine(e);
            throw;
        }

    }

    public DbRepositories<TModel> Get(List<string> includes)
    {
        includes.ForEach(i => _query = _query.Include(i));
        return this;
    }

    public DbRepositories<TModel> Get( params Expression<Func<TModel, object>>[] includes)
    {
        foreach (var expression in includes)
        {
            _query = _query.Include(expression);
        }
        return this;
    }
    // public DbRepositories<TModel> GetThenInclude()
    // {
    //     
    // }

    public DbRepositories<TModel> Where(Expression<Func<TModel,bool>> predicate)
    {
       _query = _query.Where(predicate);
       return this;
    }

    public DbRepositories<TModel> Where(List<Expression<Func<TModel, bool>>> predicates)
    {
        predicates.ForEach(predicate => _query = _query.Where(predicate));
        return this;
    }
    public async Task<List<TModel>> GetAll()
    {
        try
        {
            return await _query.Where(e => e.RowStatusId == (int)StatusEnums.Active).ToListAsync();
        }
        catch (Exception e)
        {
            WriteLine(e);
            throw;
        }
       
    }
    // public DbRepositories<TModel> GetAllThenInclude()

    public async Task Delete(int id)
    {
        try
        {
            var entry = await _context.Set<TModel>().FindAsync(id);
            if (entry != null) entry.RowStatusId = (int)StatusEnums.Delete;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            WriteLine(e);
            throw;
        }


    }

    public async Task<TModel> GetOne()
    {
        try
        {
            var model = await _query.FirstOrDefaultAsync();
            if (model != null) return model;
        }
        catch (Exception e)
        {
            WriteLine(e);
            throw;
        }
        
        throw new NotFoundException();
        
            
    }
    public  async Task<TModel> GetOne(int id, IEnumerable<string>? includes = null)
    {
        try
        {
            var entry = await _context.Set<TModel>().FindAsync(id);
            if (includes != null)
                foreach (var path in includes)
                {
                    await _context.Entry(entry).Reference(path).LoadAsync();
                }
            switch (entry)
            {
                case { RowStatusId: (int)StatusEnums.Active }:
                    return entry;
                case null:
                    break;
            }
        }
        catch (Exception e)
        {
            WriteLine(e);
            throw;
        }

        throw new NotFoundException();

    }
}
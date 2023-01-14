using DataAccessLibrary.CustomExceptions;
using DataAccessLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
        catch (DbUpdateException exception) when(exception.InnerException is SqlException)
        {
            Console.WriteLine();
            throw new AlreadyExistingException("");
        }
        
    }

    public async Task<TModel> Update(int id, TModel model)
    {
        
        var local =  await ((DbSet<TModel>)_query).FindAsync(id);
            
        // check if local is not null
        if (local == null)
        {
            // detach
            
            throw new NotFoundException("No such item");
        }
        _context.Entry(local).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
        return local;

    }

    public DbRepositories<TModel> Get(List<string> includes)
    {
        includes.ForEach(i => _query = _query.Include(i));
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
            Console.WriteLine(e);
            throw;
        }
       
    }
    // public DbRepositories<TModel> GetAllThenInclude()

    public async Task Delete(int id)
    {
        try
        {
            var entry = await ((DbSet<TModel>)_query).FindAsync(id);
            if (entry != null) entry.RowStatusId = (int)StatusEnums.Delete;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


    }
    public  async Task<TModel> GetOne(int id)
    {
        var entry = await _context.Set<TModel>().FindAsync(id);
        switch (entry)
        {
            case { RowStatusId: (int)StatusEnums.Active }:
                return entry;
            case null:
                break;
        }

        throw new NotFoundException();

    }
}
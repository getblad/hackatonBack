using DataAccessLibrary.CustomExceptions;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.Data.SqlClient;
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


    public async Task<TModel> Create(TModel model)
    {
        try
        {
            await _dbSet.AddAsync(model);
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
        var local =  await _dbSet.FindAsync(id);
            
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

    public async Task<List<TModel>> GetAll()
    {
        try
        {
            var dbValues = await _dbSet.Where(e => e.RowStatusId == (int)StatusEnums.Active)
            .ToListAsync();
            return dbValues;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task Delete(int id)
    {
        try
        {
            var entry = await _dbSet.FindAsync(id);
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
        var entry = await _dbSet.FindAsync(id);
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
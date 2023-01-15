﻿using System.Linq.Expressions;
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

    public DbRepositories<TModel> Get(List<Expression<Func<TModel, object>>> includes)
    {
        includes.ForEach(i => _query = _query.Include(i));
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
            Console.WriteLine(e);
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
            Console.WriteLine(e);
            throw;
        }


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
            Console.WriteLine(e);
            throw;
        }

        throw new NotFoundException();

    }
}
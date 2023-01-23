using System.Linq.Expressions;
using DataAccessLibrary.CustomExceptions;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace DataAccessLibrary.Repositories;

public class DbRepositories<TModel>:IDbRepositories<TModel> where TModel : class, IStatus  
{
    private readonly HpContext _context;
    private  IQueryable<TModel> _query;

    public DbRepositories(HpContext context, IQueryable<TModel>? query = null)
    {
        _context = context;
        _query = query ?? _context.Set<TModel>();

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

            if (model != null) _context.Entry(local).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return local;
        }
        catch (Exception e)
        {
            WriteLine(e);
            throw;
        }

    }

    public IDbRepositories<TModel> Get( params string[]? includes)
    {
        // includes.ForEach(i => _query = _query.Include(i));
        if (includes != null)
        {
            _query = includes.Aggregate(_query,
                (current, include) => current.Include(include));
        }
        return this;
    }

    public IDbRepositories<TModel> Get( params Expression<Func<TModel, object>>[] includes)
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

    public IDbRepositories<TModel> Where(Expression<Func<TModel,bool>> predicate)
    {
       _query = _query.Where(predicate);
       return this;
    }

    public IDbRepositories<TModel> Where(List<Expression<Func<TModel, bool>>> predicates)
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

    public async Task Delete(int id, int userId)
    {
        try
        {
            var entry = await _context.Set<TModel>().FindAsync(id);

            if (entry != null)
            {
                entry.RowStatusId = (int)StatusEnums.Delete;
                entry.UpdateUserId = userId;
            }
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            WriteLine(e);
            throw;
        }


    }

    public IDbRepositories<T> Selector<T>(Expression<Func<TModel, T>> selector) where T:class, IStatus
    {
        try
        {
            var query = _query.Select(selector);
            return new DbRepositories<T>(_context, query );
        }
        catch (Exception e)
        {
            WriteLine(e);
            throw;
        }
    }
    public IDbRepositories<TModel> GetWithEveryPropertyOnce()
    { 
        var properties = typeof(TModel).GetProperties();
        foreach (var property in properties)
        {
            var propertyType = property.PropertyType;
            if (propertyType is not { IsClass: true, IsAbstract: false } || propertyType == typeof(string)) continue;
            _query = _query.Include(property.Name);
        }
        return this;
    }

    public IDbRepositories<TModel> GetWithEveryProperty()
    {
        var properties = typeof(TModel).GetProperties();
        foreach (var property in properties)
        {
            var propertyType = property.PropertyType;
            if (propertyType is not { IsClass: true, IsAbstract: false } || propertyType == typeof(string)) continue;
            _query = _query.Include(property.Name);
            var nestedProperties = propertyType.GetProperties();
            foreach (var nestedProperty in nestedProperties)
            {
                if (nestedProperty.PropertyType is { IsClass: true, IsAbstract: false } &&
                    nestedProperty.PropertyType != typeof(string))
                {
                    _query = _query.Include($"{property.Name}.{nestedProperty.Name}");
                }
            }
        }
        return this;
    }  
        // {
        //         var navigationProperties = typeof(TModel)
        //             .GetProperties()
        //             .Where(x => x.PropertyType.IsClass && x.PropertyType != typeof(string)  || x.PropertyType.IsCollection());
        //         foreach (var navigationProperty in navigationProperties)
        //         {
        //             _query = _query.Include(navigationProperty.Name);
        //             // var includeQuery = _query.Include(navigationProperty.Name) as IIncludableQueryable<TModel, object >;
        //             var isManyToMany = navigationProperty.PropertyType.GetProperties()
        //                 ;
        //             foreach (var nestedProperty in isManyToMany)
        //             {
        //                 if (nestedProperty.PropertyType is { IsClass: true, IsAbstract: false } &&
        //                     nestedProperty.PropertyType != typeof(string))
        //                 {
        //                     _query = _query.Include($"{navigationProperty.Name}.{nestedProperty.Name}");
        //                 }
        //             }
        //             // _query = _query.Include(navigationProperty.Name);
        //         }
        //
        //         return this;
        // }
    // {
    //     var properties = typeof(TModel).GetProperties()
    //                  .Where(x => x.PropertyType.IsClass && x.PropertyType != typeof(string)  || x.PropertyType.IsCollection());
    //     
    //     foreach (var property in properties)
    //     {
    //         var propertyType = property.PropertyType;
    //             _query = _query.Include(property.Name);
    //             var nestedProperties = propertyType.IsGenericType ? propertyType.GetGenericArguments()[0].GetProperties() : propertyType.GetProperties();
    //
    //             foreach (var nestedProperty in nestedProperties)
    //             {
    //                 if (nestedProperty.PropertyType is { IsClass: true, IsAbstract: false } && nestedProperty.PropertyType != typeof(string) || propertyType.IsNested)
    //                 {
    //                     _query = _query.Include($"{property.Name}.{nestedProperty.Name}");
    //                 }
    //             }
    //         
    //     }
    //     return this;
    // }
    
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
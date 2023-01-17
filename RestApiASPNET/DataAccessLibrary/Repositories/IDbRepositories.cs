using System.Linq.Expressions;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface IDbRepositories<TModel> where TModel:class, IStatus
{

    public Task<TModel> Create(TModel model);

    public Task<TModel> Update<T>(int id, T model);

    public Task<List<TModel>> GetAll();

    public DbRepositories<TModel> Where(Expression<Func<TModel, bool>> predicate);
    public DbRepositories<TModel> Where(List<Expression<Func<TModel, bool>>> predicate);

    public Task<TModel> GetOne(int id, IEnumerable<string>? includes = null);

    public Task Delete(int id);

    public DbRepositories<TModel> Get(List<string> includes);
}
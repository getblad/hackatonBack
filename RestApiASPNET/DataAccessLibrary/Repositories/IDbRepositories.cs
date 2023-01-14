using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface IDbRepositories<TModel> where TModel:class, IStatus
{

    public Task<TModel> Create(TModel model);

    public Task<TModel> Update(int id, TModel model);

    public Task<List<TModel>> GetAll();


    public Task<TModel> GetOne(int id);

    public Task Delete(int id);

    public DbRepositories<TModel> Get(List<string> includes);
}
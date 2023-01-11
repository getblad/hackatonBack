using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface IDbService<TModel> where TModel:class
{

    public TModel Create(TModel model);

    public TModel Update(int id, TModel model);

    public List<TModel> GetAll();

    public TModel GetOne(int id);

    public void Delete(int id);
}
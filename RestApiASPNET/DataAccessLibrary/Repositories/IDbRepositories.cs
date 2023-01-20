﻿using System.Linq.Expressions;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public interface IDbRepositories<TModel> where TModel:class, IStatus
{

    public IDbRepositories<T> Selector<T>(Expression<Func<TModel, T>> selector) where T : class, IStatus;
    public IDbRepositories<TModel> GetWithEveryPropertyOnce();
    public IDbRepositories<TModel> GetWithEveryProperty();
    public IDbRepositories<TModel> Get(params Expression<Func<TModel, object>>[] includes);
    public Task<TModel> Create(TModel model);

    public Task<TModel> Update<T>(int id, T model);

    public Task<List<TModel>> GetAll();

    public IDbRepositories<TModel> Where(Expression<Func<TModel, bool>> predicate);
    public IDbRepositories<TModel> Where(List<Expression<Func<TModel, bool>>> predicate);

    public Task<TModel> GetOne(int id, IEnumerable<string>? includes = null);

    public Task Delete(int id, int userId);

    public Task<TModel> GetOne();
    public IDbRepositories<TModel> Get(params string[]? includes);
}
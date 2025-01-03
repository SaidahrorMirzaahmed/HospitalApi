﻿using HospitalApi.Domain.Commons;
using System.Linq.Expressions;

namespace HospitalApi.DataAccess.Repositories;

public interface IRepository<T> where T : Auditable
{
    Task<T> InsertAsync(T entity);
    Task<IEnumerable<T>> BulkInsertAsync(IEnumerable<T> entities);
    Task<T> UpdateAsync(T entity);
    Task<IEnumerable<T>> BulkUpdateAsync(IEnumerable<T> entities);
    Task<T> DeleteAsync(T entity);
    //Task BulkDeleteAsyn(IEnumerable<T> entities);
    Task<T> DropAsync(T entity);
    //Task BulkDropAsync(IEnumerable<T> entities);
    Task<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null, bool isTracked = true);
    Task<IEnumerable<T>> SelectAsEnumerableAsync(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true);
    IQueryable<T> SelectAsQueryable(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true);
}
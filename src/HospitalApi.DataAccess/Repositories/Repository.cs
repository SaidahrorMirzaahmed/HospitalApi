using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using HospitalApi.DataAccess.Contexts;
using HospitalApi.Domain.Commons;

namespace HospitalApi.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext context;
    private readonly DbSet<T> set;
    public Repository(AppDbContext context)
    {
        this.context = context;
        this.set = context.Set<T>();
    }

    public async Task<T> InsertAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        return (await set.AddAsync(entity)).Entity;
    }

    //public async Task BulkInsertAsync(IEnumerable<T> entities)
    //{
    //    await context.BulkInsertAsync(entities);
    //}

    public async Task<T> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        set.Entry(entity).State = EntityState.Modified;
        set.Update(entity);
        return await Task.FromResult(entity);
    }

    //public async Task BulkUpdateAsync(IEnumerable<T> entities)
    //{
    //    await context.BulkUpdateAsync(entities.Select(entity =>
    //        {
    //            entity.UpdatedAt = DateTime.UtcNow;
    //            return entity;
    //        }));
    //}

    public async Task<T> DeleteAsync(T entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        set.Update(entity);
        return await Task.FromResult(entity);
    }

    //public async Task BulkDeleteAsyn(IEnumerable<T> entities)
    //{
    //    await context.BulkUpdateAsync(entities.Select(entity =>
    //        {
    //            entity.DeletedAt = DateTime.UtcNow;
    //            entity.IsDeleted = true;
    //            return entity;
    //        }));
    //}

    public async Task<T> DropAsync(T entity)
    {
        return await Task.FromResult(set.Remove(entity).Entity);
    }

    //public async Task BulkDropAsync(IEnumerable<T> entities)
    //{
    //    await context.BulkDeleteAsync(entities);
    //}

    public async Task<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null)
    {
        var query = set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> SelectAsEnumerableAsync(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true)
    {
        var query = expression is null ? set : set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracked)
            query.AsNoTracking();

        return await query.ToListAsync();
    }

    public IQueryable<T> SelectAsQueryable(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true)
    {
        var query = expression is null ? set : set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracked)
            query.AsNoTracking();


        return query;
    }
}
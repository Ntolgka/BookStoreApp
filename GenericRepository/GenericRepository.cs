using System.Linq.Expressions;
using BookStoreApp.Base;
using BookStoreApp.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.GenericRepository;

internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _dbContext;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> GetById(int id, params string[] includes)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();
        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TEntity> Insert(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity is not null)
            _dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression, params string[] includes)
    {
        var query = _dbContext.Set<TEntity>().Where(expression).AsQueryable();
        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return await query.ToListAsync();
    }

    public async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> expression, params string[] includes)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();
        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return await query.FirstOrDefaultAsync(expression);
    }

    public async Task<List<TEntity>> GetAll(params string[] includes)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();
        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return await query.ToListAsync();
    }
}   
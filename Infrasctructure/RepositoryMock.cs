using Domain.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrasctructure;

public class RepositoryMock<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DemoDbContext _context;
    public RepositoryMock(DemoDbContext context)
    {
        _context = context;
    }
    public async Task Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
    }
    public async Task Add(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = Find(predicate);
        _context.Set<TEntity>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
    public TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
    {
        return Get(findOptions).FirstOrDefault(predicate)!;
    }
    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
    {
        return Get(findOptions).Where(predicate);
    }
    public IQueryable<TEntity> GetAll(FindOptions? findOptions = null)
    {
        return Get(findOptions);
    }
    public async Task Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }
    public bool Any(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().Any(predicate);
    }
    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().Count(predicate);
    }
    private DbSet<TEntity> Get(FindOptions? findOptions = null)
    {
        // Se findOptions for nulo, inicializa com um novo objeto FindOptions
        findOptions ??= new FindOptions();

        // Obtém o DbSet<TEntity> do contexto
        var entity = _context.Set<TEntity>();

        // Verifica as opções em findOptions e aplica configurações ao DbSet
        if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
        {
            entity.IgnoreAutoIncludes().AsNoTracking();
        }
        else if (findOptions.IsIgnoreAutoIncludes)
        {
            entity.IgnoreAutoIncludes();
        }
        else if (findOptions.IsAsNoTracking)
        {
            entity.AsNoTracking();
        }
        return entity;
    }
}

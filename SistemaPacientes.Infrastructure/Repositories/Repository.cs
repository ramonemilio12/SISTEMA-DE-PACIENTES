using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Core;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Context;

namespace SistemaPacientes.Infrastructure.Repositories;

/// <summary>
/// Implementación genérica del patrón repositorio sobre EF Core.
/// Sirve como clase base para todos los repositorios específicos del sistema.
/// </summary>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly SistemaPacientesContext Context;
    protected readonly DbSet<TEntity> Set;

    public Repository(SistemaPacientesContext context)
    {
        Context = context;
        Set = context.Set<TEntity>();
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().ToListAsync(cancellationToken);

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await Set.FindAsync(new object?[] { id }, cancellationToken);

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Set.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var existing = await Set.FindAsync(new object?[] { entity.Id }, cancellationToken);
        if (existing is null)
        {
            Set.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }
        else
        {
            Context.Entry(existing).CurrentValues.SetValues(entity);
        }

        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await Set.FindAsync(new object?[] { id }, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        Set.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public virtual Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => Set.AnyAsync(e => e.Id == id, cancellationToken);
}

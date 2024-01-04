using System.Reflection;

using Microsoft.EntityFrameworkCore;

using UserManagement.Application.Abstractions.Data;
using UserManagement.Domain.Primitives;
using UserManagement.Domain.Primitives.Maybe;

namespace UserManagement.Persistence;

public sealed class UserManagementDbContext : DbContext, IDbContext
{
    public UserManagementDbContext(DbContextOptions options) : base(options)
    {
    }

    public new DbSet<TEntity> Set<TEntity>()
        where TEntity : Entity => base.Set<TEntity>();

    public async Task<Maybe<TEntity>> GetBydIdAsync<TEntity>(string id)
        where TEntity : Entity
    {
        if (string.IsNullOrEmpty(id))
        {
            return Maybe<TEntity>.None;
        }

        return await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public void Insert<TEntity>(TEntity entity)
        where TEntity : Entity => Set<TEntity>().Add(entity);

    public new void Remove<TEntity>(TEntity entity)
        where TEntity : Entity => Set<TEntity>().Remove(entity);

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}

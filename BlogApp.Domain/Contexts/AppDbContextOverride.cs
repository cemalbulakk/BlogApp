using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BlogApp.Domain.Entities;

namespace BlogApp.Domain.Contexts;

public partial class AppDbContext
{
    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();
        var authenticatedUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        foreach (var entry in entries)
        {
            switch (entry.Entity)
            {
                case BaseEntity tractable:
                    {
                        var now = DateTime.Now;
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                tractable.Id = Guid.NewGuid().ToString("D");
                                tractable.CreateDate = now;
                                tractable.CreateBy = authenticatedUserId ?? throw new InvalidOperationException(); //1
                                tractable.IsActive = true;
                                break;

                            case EntityState.Modified:
                                tractable.UpdateDate = now;
                                tractable.UpdateBy = authenticatedUserId;//1
                                break;

                            case EntityState.Detached:
                            case EntityState.Unchanged:
                            case EntityState.Deleted:
                                tractable.IsActive = false;
                                tractable.UpdateDate = now;
                                tractable.UpdateBy = authenticatedUserId;//1
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
            }
        }
    }

    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }
}
namespace TourPlanner.DataAccessLayer.Repositories;

public abstract class RepositoryBase
{
    protected readonly AppdDbContext Context;

    protected RepositoryBase()
    {
        var dbContextFactory = new AppdDbContextFactory();
        Context = dbContextFactory.CreateDbContext();
    }
}
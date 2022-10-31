namespace BlogApp.Persistence.Repositories;

public interface IQuery<T>
{
    IQueryable<T> Query();
}
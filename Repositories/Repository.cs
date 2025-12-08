
using Microsoft.EntityFrameworkCore;
using ScuolaMVC.Data;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ScuolaContext _ctx;
    protected readonly DbSet<T> _set;

    public Repository(ScuolaContext ctx)
    {
        _ctx = ctx;
        _set = ctx.Set<T>();
    }

    public List<T> GetAll() => _set.ToList();
    public T? GetById(int id) => _set.Find(id);
    public void Add(T entity) => _set.Add(entity);
    public void Update(T entity) => _set.Update(entity);
    public void Delete(T entity) => _set.Remove(entity);
    public void Save() => _ctx.SaveChanges();
}

using Api.IRepository;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Repository;

public class GenericRepository<T, DTO> : IGenericRepository<T, DTO>
    where T : class
        where DTO : class
{
    protected readonly SoccerContext _context;
    private DbSet<T> table = null;

    public GenericRepository()
    {
    }

    public GenericRepository(SoccerContext context)
    {
        this._context = context;
    }

    public int Delete(int id)
    {
        var obj = this.getById(id);
        table.Remove(obj);
        return _context.SaveChanges();
    }

    public IEnumerable<T> GetAll()
    {
        return table.AsEnumerable();
    }

    public virtual IEnumerable<DTO> GetAllDto()
    {
        throw new NotImplementedException();
    }

    public T getById(int id)
    {
        return table.Find(id);
    }

    public virtual DTO getDTOById(int id)
    {
        throw new NotImplementedException();
    }

    public T Insert(T obj)
    {
        var result = table.Add(obj);
        _context.SaveChanges();
        return result.Entity;
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public T Update(T obj)
    {
        table.Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
        return obj;
    }
}

using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Data;
using Project.DataAccess.Repository.IRepository;

namespace Project.DataAccess.Repository;
public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>(); //  _db.Cateogries === dbSet
        //_db.Products.Include(u => u.category).Include(u => u.CategoryId); // for the foreign key relation

        _db.Products.Include(u => u.category); // This is a way to ensure that when you retrieve Products from the database, you also retrieve their associated category data in a single query.

        // Using this.dbSet = _db.Set<T>(); instead of this.dbSet = _db.Categories; provides more flexibility and adheres to a generic pattern.
        // Generality: _db.Set<T>() is a generic method that returns a DbSet<T> for any type T. This makes the code more generic and reusable across different scenarios.
        // When you assign this.dbSet to _db.Set<T>(), you're not copying the data from the Categories property of the ApplicationDbContext.
        // Instead, you're creating a reference to the DbSet<Category> stored in the Categories property.
        // This means that any changes made to this.dbSet will directly affect the data in the Categories table in the database,
        // because both this.dbSet and Categories point to the same underlying DbSet<Category> object

    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet; //  is implicitly converting the DbSet<T> (dbSet) to an IQueryable<T> (query).
                                     //Implicit Conversion: DbSet<T> implements the IQueryable<T> interface.
                                     //This means that a DbSet<T> can be treated as an IQueryable<T> without the need for an explicit cast.
                                     // you can assign a DbSet<T> object directly to a variable of type IQueryable< T >.

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        query = query.Where(filter); // deyman 7a traje3 one entity ya3ne category aw product wahd bs msh set of entities
        return query.FirstOrDefault(); 
    }

    // yaane fi hal hada mnl products aatana category id krml tale3 esm l category
    public IEnumerable<T> GetAll(string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        if(!string.IsNullOrEmpty(includeProperties))
        {
            foreach(var includeProp in includeProperties
                .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return query.ToList();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity); // RemoveRange is a built in method in AF Core. it takes a IEnumerable as input
    }
}


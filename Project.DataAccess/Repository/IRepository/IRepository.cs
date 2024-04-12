using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Project.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		// T - Category

		IEnumerable<T> GetAll(string? includeProperties = null);
		T Get(Expression<Func<T,bool>> filter, string? includeProperties = null); // function input howe T wl output boolean
		void Add(T entity);
        //void Update(T entity); hon ma rje3t shlta kermel logic lama a3ml update la category howe 8er 3an logic lama a3ml update lal product masaln
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}

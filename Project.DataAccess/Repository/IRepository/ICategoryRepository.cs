using System;
using Project.Models;

namespace Project.DataAccess.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void Update(Category obj);
		//void Save();
	}
}


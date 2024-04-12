 using System;
using Project.DataAccess.Data;
using Project.DataAccess.Repository.IRepository;
using Project.Models;

namespace Project.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository 
	{
		private ApplicationDbContext _db;

		public CategoryRepository(ApplicationDbContext db) : base(db) // we are passing the db context to the Repository<Category> class
		{
			_db = db;
		}

		//public void Save()
		//{
		//	_db.SaveChanges();
		//}

        public void Update(Category obj)
        {
			_db.Categories.Update(obj);
        }
    }
}


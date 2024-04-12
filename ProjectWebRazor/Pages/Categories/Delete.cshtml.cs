using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectWebRazor.Data;
using ProjectWebRazor.Models;

namespace ProjectWebRazor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }


        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category? cat = _db.Categories.Find(id);
                if (cat != null) Category = cat;
            }

        }

        public IActionResult OnPost()
        {
            Category? cat = _db.Categories.Find(Category.Id);

            if (cat == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(cat);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToPage("Index");
        }
    }
}

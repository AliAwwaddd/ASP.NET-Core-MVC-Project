using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.DataAccess.Data;
using Project.DataAccess.Repository.IRepository;
using Project.DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Project.Utility;

namespace MainProject2.Areas.Admin.Controllers;

[Area("Admin")] // l n2oul eno ha l ocntroller belongs to the admin area
[Authorize(Roles = SD.Role_Admin)]
public class CategoryController : Controller
{
    //private readonly ApplicationDbContext _db;
    //private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var objCategoryList = _unitOfWork.Category.GetAll().ToList();
        //var objCategoryList = _categoryRepo.GetAll().ToList();
        //var objCateogryList = _db.Categories.ToList();
        //List<Category> objCateogryList = _db.Categories.ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The display Order cannot exactly match the name"); // first parameter is the key where which tell us where to put the error message
            //ModelState.AddModelError("displayorder", "The display Order cannot exactly match the name"); // Same output
        }

        if (obj.Name.ToLower() == "test")
        {
            ModelState.AddModelError("", "Test is an invalid value"); // key is empty so it will display the validation in the global summary 
        }

        if (ModelState.IsValid) // check if the user inputs are valid according to the limits
        {
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            //_categoryRepo.Add(obj);
            //_categoryRepo.Save();

            //_db.Categories.Add(obj);
            //_db.SaveChanges();
            TempData["success"] = "Category created successfully.";
            return RedirectToAction("Index");
        }

        return View();

        //return RedirectToAction("Index, Category"); 
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
        //Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);

        //Category? categoryFromDb = _db.Categories.Find(id);
        //Category? categoryFromDb2 = _db.Categories.FirstOrDefault( u => u.Id == id);
        //Category? categoryFromDb3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault(); 
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The display Order cannot exactly match the name"); // first parameter is the key where which tell us where to put the error message
            //ModelState.AddModelError("displayorder", "The display Order cannot exactly match the name"); // Same output
        }

        if (obj.Name.ToLower() == "test")
        {
            ModelState.AddModelError("", "Test is an invalid value"); // key is empty so it will display the validation in the global summary 
        }

        if (ModelState.IsValid) // check if the user inputs are valid according to the limits
        {
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();

           //_categoryRepo.Update(obj);
           //_categoryRepo.Save();

           //_db.Categories.Update(obj);
           //_db.SaveChanges();
           TempData["success"] = "Category updated successfully.";
            return RedirectToAction("Index");
        }

        return View();

        //return RedirectToAction("Index, Category");
    }


    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
       //Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
        //Category? categoryFromDb = _db.Categories.Find(id);

        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    [HttpPost, ActionName("Delete")]

    public IActionResult DeletePOST(int? id)
    {
        Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
        //Category? obj = _categoryRepo.Get(u => u.Id == id);
        //Category? cat = _db.Categories.find(id);
        //Category? cat = _db.Categories.FirstOrDefault(obj => obj.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();

        //_categoryRepo.Remove(obj);
        //_categoryRepo.Save();

        //_db.Categories.Remove(cat);
        //_db.SaveChanges();
        TempData["success"] = "Category deleted successfully.";
        return RedirectToAction("Index");

    }
}


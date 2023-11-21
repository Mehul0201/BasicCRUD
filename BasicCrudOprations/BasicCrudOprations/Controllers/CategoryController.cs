using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using BasicCrudOprations.DAL;
using BasicCrudOprations.Models;
using PagedList.Mvc;
using PagedList;

namespace BasicCrudOprations.Controllers
{
    public class CategoryController : Controller
    {
        Category_DAL _category = new Category_DAL();

        // GET: Category
        public ActionResult Index(int ? i)
        {
            var productList = _category.GetAllCategories();

            if (productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently Categories are not avilable in the Database";
            }
            return View(productList.ToPagedList(i ?? 1, 3));
        }

        //GET : Category/CreateCategories
        [HttpGet]
        public ActionResult InsertCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertCategory(CategoryModel category)
        {
            bool IsInserted = false;

            try 
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _category.InsertCat(category);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Category details saved successfully!!!!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the category deatils ";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
  
        }

        public ActionResult EditCategories(int id) 
        {
            var category = _category.GetCategoryByID(id).FirstOrDefault();

            if(category == null) 
            {
                TempData["infoMessage"] = "No Record Found!!!!!"+id.ToString();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost, ActionName("EditCategories")]
        public ActionResult UpdateCategory(CategoryModel categorym)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isUpdated = _category.UpdateCategories(categorym);

                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Category Details Updated Successfully!!!!!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the Category details";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        public ActionResult DeleteCategory(int id)
        {
            try
            {
                var category = _category.GetCategoryByID(id).FirstOrDefault();

                if (category == null)
                {
                    TempData["infoMessage"] = "No Record Found!!!!!" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost,ActionName("DeleteCategory")]
        public ActionResult DeleteConfirmation(int id) 
        {
            try
            {
                string result = _category.DeleteCategory(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        public ActionResult Details(int id)
        {
            var category = _category.GetCategoryByID(id).FirstOrDefault();
            return View(category);
        }
    }
}
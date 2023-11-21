using BasicCrudOprations.DAL;
using BasicCrudOprations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace BasicCrudOprations.Controllers
{
    public class ProductController : Controller
    {
        Product_DAL _product = new Product_DAL();
        // GET: Product
        public ActionResult ListIndex(int ? i)
        {
            var productList = _product.GetAllProducts();
            return View(productList.ToPagedList(i ?? 1,3));
        }

        [HttpGet]
        public ActionResult InsertProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertProduct(ProductModel product)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _product.InsertProducts(product);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Category details saved successfully!!!!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the category deatils ";
                    }
                }
                return RedirectToAction("ListIndex");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var category = _product.GetProductByID(id).FirstOrDefault();

                if (category == null)
                {
                    TempData["infoMessage"] = "No Record Found!!!!!" + id.ToString();
                    return RedirectToAction("ListIndex");
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _product.DeleteProducts(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("ListIndex");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public ActionResult EditProduct(int id)
        {
            var category = _product.GetProductByID(id).FirstOrDefault();

            if (category == null)
            {
                TempData["infoMessage"] = "No Record Found!!!!!" + id.ToString();
                return RedirectToAction("ListIndex");
            }
            return View(category);
        }

        [HttpPost, ActionName("EditProduct")]
        public ActionResult UpdateProduct(ProductModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isUpdated = _product.UpdateCategories(product);

                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Category Details Updated Successfully!!!!!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the Category details";
                    }
                }
                return RedirectToAction("ListIndex");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var category = _product.GetProductByID(id).FirstOrDefault();
            return View(category);
        }

    }
}
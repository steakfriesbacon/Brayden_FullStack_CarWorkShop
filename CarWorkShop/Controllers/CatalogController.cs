using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarWorkShop.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace CarWorkShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly F201_Brayden_ProjectContext _context;

        public CatalogController(F201_Brayden_ProjectContext context)
        {
            _context = context;
        }

        // GET: Catalog?CategoryName=Travel
        public IActionResult Index()
        {
            
            string SQL = "SELECT ProductId, Product.CategoryId AS CategoryId, Name, ImageFileName, UnitCost"
                + ", SUBSTRING(Description, 1, 100) + '...' AS Description, CategoryName "
                + "FROM Product INNER JOIN Category ON Product.CategoryId = Category.CategoryId ";
            string categoryName = Request.Query["CategoryName"];

            if (categoryName != null)
            {
                
                if (categoryName.Length > 20 || categoryName.IndexOf("'") > -1 || categoryName.IndexOf("#") > -1)
                {
                    //TODO Code to log this event and send alert email to admin
                    return BadRequest(); // Http status code 400
                }

                //140903 JPC  Passed the above test so extend SQL
                //150807 JPC Security improvement @p0
                SQL += " WHERE CategoryName = @p0";
                //SQL += " WHERE CategoryName = '{0}'";
                //SQL = String.Format(SQL, CategoryName);
                //Send extra info to the view that this is the selected CategoryName
                ViewBag.CategoryName = categoryName;
            }

            
            //Error, Changed from "FromSql" to "FromSqlRaw" as the previous is obselete. Brayden Roberts 10/09/19
            var products = _context.CatalogViewModel.FromSqlRaw(SQL, categoryName);
            return View(products.ToList());
        }

        // GET: Catalog/Details?ProductId=1MORE4ME
        public IActionResult Details(string ProductId)
        {
            if (ProductId == null)
            {
                return BadRequest(); // Http status code 400
            }
            
            if (ProductId.Length > 20 || ProductId.IndexOf("'") > -1 || ProductId.IndexOf("#") > -1)
            {
                //TODO Code to log this event and send alert email to admin
                return BadRequest(); // Http status code 400
            }

            
            string SQL = "SELECT ProductId, Product.CategoryId AS CategoryId, Name, ImageFileName, UnitCost"
            + ", Description, CategoryName "
            + "FROM Product INNER JOIN Category ON Product.CategoryId = Category.CategoryId "
            + " WHERE ProductId = @p0";

            
            var product = _context.CatalogViewModel.FromSqlRaw(SQL, ProductId).ToList()[0];
            if (product == null)
            {
                return NotFound(); //Http status code 404
            }
            return View(product);
        }
















    }
}

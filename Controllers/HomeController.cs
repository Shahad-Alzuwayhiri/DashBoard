using Dashboard.Data;
using Dashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
           // var Name = HttpContext.User.Identity.Name;
            //CookieOptions options = new CookieOptions();
            //options.Expires = DateTime.Now.AddMinutes(10);
            //Response.Cookies.Append("Name", Name, options);
            //TempData["Name"] = Name;
            //HttpContext.Session.SetString("Name", Name);
           // ViewBag.Name = Name;
            var product = context.Products.ToList();
            return View(product);
        }
        public IActionResult PaymentAccept()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PaymentAccept(PaymentAccept PaymentAccept)
        {
            return View();
        }
        public IActionResult AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var product = context.Products.SingleOrDefault(x => x.Id == id);
            return View(product);
        }
        public IActionResult UpdateProducs(Product products)
        {

            context.Products.Update(products);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            var product = context.Products.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddProductDetails(ProductDetails productDetails)
        {
            context.ProductDetails.Add(productDetails);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
    
        public IActionResult EditProductDetails(int id)
        {
            var ProductDetails = context.ProductDetails.FirstOrDefault(x => x.ProductId == id);
            return View(ProductDetails);
        }
        public IActionResult UpdateProductDetails(ProductDetails productDetails)
        {

            context.ProductDetails.Update(productDetails);
            context.SaveChanges();
            return RedirectToAction("ProductDetails");


        }
        public IActionResult DeleteProductDetails(int id)
        {
            var ProductDetails = context.ProductDetails.FirstOrDefault(p => p.ProductId == id) ?? new ProductDetails();
            if (ProductDetails != null)
            {
                context.ProductDetails.Remove(ProductDetails);
                context.SaveChanges();
            }
            return RedirectToAction("ProductDetails");
        }
        [HttpPost]
        public IActionResult ProductDetails(int id)
        {

            var ProductDetails = context.ProductDetails.Where(p => p.ProductId == id).ToList();
            var product = context.Products.ToList();
            ViewBag.ProductDetails = ProductDetails;
            return View(product);


        }
        public IActionResult ProductDetails()
        {
            //ViewBag.Name = Request.Cookies["Name"];
            ViewBag.Name = HttpContext.Session.GetString("Name");
            var products = context.Products.ToList();
            var ProductDetails = context.ProductDetails.ToList();
            ViewBag.ProductDetails = ProductDetails;
            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
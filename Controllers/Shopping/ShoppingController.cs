using Dashboard.Data;
using Dashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient =MailKit.Net.Smtp.SmtpClient;
using Microsoft.CodeAnalysis;

namespace Dashboard.Controllers.Shopping
{
	public class ShoppingController : Controller

    {
        private readonly ApplicationDbContext context;
        public ShoppingController(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        public IActionResult ProductDetails(int id)
        {

            var ProductDetails = context.ProductDetails.Where(p => p.ProductId == id);
            return View(ProductDetails);


        }
        public IActionResult Index()
        {
            var ProductDetails = context.ProductDetails.ToList();
            return View(ProductDetails);

        }
        public async Task<string> SendMail()
        {
            //xxoiqetkzpzyuxzc
            //nzufhzxvbmvqyves

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("TestMessage", "Shshahd182@gmail.com"));
            message.To.Add(MailboxAddress.Parse("sahd5566@hotmail.com"));
            message.Subject = "test email from asp.net ";
            message.Body = new TextPart("plain")
            {
                Text = "Welcom"
            };
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("tuwiqst@gmail.com", "abcr jjdn wvls yjgl");
                    await client.SendAsync(message);
                    client.Disconnect(true);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }



            };

            return "ok";

        }




		public IActionResult CheckOut(int id)
        {
            var user = HttpContext.User.Identity.Name;
            var ProductDetails = context.ProductDetails.SingleOrDefault(p => p.ProductId == id);
            var cart = new Cart()
            {
                CustomerId = user,
                Id = ProductDetails.ProductId,
                Color = ProductDetails.Color,
                Image = ProductDetails.Image,
                Price = ProductDetails.Price,
                Total = ProductDetails.Price * (15 / 100) + ProductDetails.Price,
                Tax = (5/100)+ ProductDetails.Price,
                ProductName=ProductDetails.ProductName,



            };
            context.Carts.Add(cart);
            context.SaveChanges();
            return View(CheckOut);

        }
       
        public IActionResult Invoice(int id) {
            var user = HttpContext.User.Identity.Name;
            var ProductDetails = context.ProductDetails.SingleOrDefault(p => p.ProductId == id);
            var Invoice = new Invoice()
            {
                ProductId= ProductDetails.ProductId,
                Price = ProductDetails.Price,
                QTY= ProductDetails.Qty,
                Total = ProductDetails.Price,

            };
            context.Invoice.Add(Invoice);
            context.SaveChanges();
            return View(Invoice);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using PayperClip.Data;
using PayperClip.Models;
using Microsoft.AspNetCore.Authorization;

namespace PayperClip.Controllers
{
    [Authorize(Policy = "RestrictAdmin")]
    public class CartController : Controller
    {
        private readonly MyDbContext _context;

        public CartController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        public IActionResult Index()
        {
            string cartString = Request.Cookies["cart"];
            Dictionary<int, int> cart;
            if (cartString != null)
            {
                cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cartString);
            }
            else
            {
                cart = new Dictionary<int, int>();
            }


            var articles = _context.Article.ToList();
            var articlesQuantities = new List<(Article, int)>();
            ViewBag.Total = 0;
            foreach (var item in cart)
            {
                var article = articles.Where(a => a.ArticleId == item.Key).ToList();

                if (article != null && article.Count != 0)
                {
                    ViewBag.Total += article[0].Price * item.Value;
                    articlesQuantities.Add((article[0], item.Value));
                }
                else
                {
                    cart.Remove(item.Key);
                }
            }

            return View(articlesQuantities);
        }


        public IActionResult Add(int? id)
        {
            string cartString = Request.Cookies["cart"];
            Dictionary<int, int> cart;
            if (cartString != null)
            {
                cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cartString);
            }
            else
            {
                cart = new Dictionary<int, int>();
            }

            int article_id = Convert.ToInt32(id);
            if (cart.ContainsKey(article_id))
            {
                cart[article_id] += 1;
            }
            else
            {
                var articles = _context.Article.ToList();
                foreach (var item in articles)
                {
                    if (item.ArticleId == article_id)
                    {
                        cart[article_id] = 1;
                        break;
                    }
                }
            }

            cartString = JsonConvert.SerializeObject(cart);
            SetCookie("cart", cartString, 7);

            return Redirect("/cart/index");
        }

        public IActionResult IncreaseQuantity(int? id)
        {
            string cartString = Request.Cookies["cart"];
            Dictionary<int, int> cart;
            if (cartString != null)
            {
                cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cartString);
            }
            else
            {
                cart = new Dictionary<int, int>();
            }

            int article_id = Convert.ToInt32(id);
            if (cart.ContainsKey(article_id))
            {
                cart[article_id] += 1;
            }

            cartString = JsonConvert.SerializeObject(cart);
            SetCookie("cart", cartString, 7);

            return Redirect("/cart/index");
        }

        public IActionResult DecreaseQuantity(int? id)
        {
            string cartString = Request.Cookies["cart"];
            Dictionary<int, int> cart;
            if (cartString != null)
            {
                cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cartString);
            }
            else
            {
                cart = new Dictionary<int, int>();
            }

            int article_id = Convert.ToInt32(id);
            if (cart.ContainsKey(article_id))
            {
                if (cart[article_id] > 1)
                {
                    cart[article_id] -= 1;
                }
                else
                {
                    cart.Remove(article_id);
                }
            }

            cartString = JsonConvert.SerializeObject(cart);
            SetCookie("cart", cartString, 7);

            return Redirect("/cart/index");
        }

        public IActionResult Order()
        {
            //return View(articlesQuantities);
            return Redirect("/orders/create");
        }

        public void SetCookie(string key, string value, int? numberOfDays = null)
        {
            CookieOptions option = new CookieOptions();
            if (numberOfDays.HasValue)
                option.Expires = DateTime.Now.AddDays(numberOfDays.Value);
            Response.Cookies.Append(key, value, option);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

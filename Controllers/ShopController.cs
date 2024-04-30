using System.Linq;
using System.Threading.Tasks;
using PayperClip.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PayperClip.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        private readonly MyDbContext _context;

        public ShopController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Shop
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.Categories = _context.Category.ToList();
            ViewBag.Current = id;

            var myDbContext = _context.Article
                .Include(a => a.Category)
                .Where(a => a.CategoryId == id);

            return View(await myDbContext.ToListAsync());

        }
    }
}
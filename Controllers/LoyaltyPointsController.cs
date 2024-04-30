using Azure.Core;
using PayperClip.Data;
using PayperClip.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayperClip.Controllers
{
    public class LoyaltyPointsController : Controller
    {
      public IActionResult Index()
        {
            ViewBag.LoyaltyPoints = 0;
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VistasPostAdd.Models;

namespace VistasPostAdd.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContex dbContex;

        public HomeController(AppDbContex dbContex)
        {
            this.dbContex = dbContex;
        }
        public IActionResult Index()
        {
           
            return View(dbContex.Anuncios.Include(x => x.Imagens).ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

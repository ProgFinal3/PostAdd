using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostAds.Models;
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

        [AllowAnonymous]
        public IActionResult Index(int? id)
        {
            bool idcat (Anuncio x) => !id.HasValue || id.Value == x.CategoriaId;
            var Adsget = dbContex.Anuncio.Include(x => x.Imagen).Where(idcat).ToList();
            id = null;
            return View(Adsget);
        }

        public IActionResult Detalle()
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

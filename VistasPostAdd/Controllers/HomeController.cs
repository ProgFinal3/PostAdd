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

        public IActionResult Detalle(int? id)
        {
            var ads = dbContex.Anuncio.Include(x => x.Imagen).Where(x => x.Id == id).First();
            return View(ads);
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(dbContex.Anuncio.Include(x => x.Imagen).ToList());
        }

        public IActionResult Detalle()
        {
            return View();
        }

    }
}

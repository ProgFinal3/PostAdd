using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostAds.Models;
using VistasPostAdd.ViewModel;

namespace VistasPostAdd.Controllers
{
    public class RegistroController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public RegistroController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        
        // GET: Registro
        public ActionResult Index()
        {
            return View();
        }

        // GET: Registro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Registro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Email = model.Email
                };

                var CreateUser = await userManager.CreateAsync(user, model.Password);
                if (CreateUser.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            return RedirectToAction("Index", model);
        }

        // GET: Registro/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Registro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Registro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Registro/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
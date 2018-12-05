using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostAds.Models;
using VistasPostAdd.Models;

namespace VistasPostAdd.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly AppDbContex dbContex;
        private readonly UserManager<AppUser> userManager;

        public AdministradorController(AppDbContex dbContex, UserManager<AppUser> userManager)
        {
            this.dbContex = dbContex;
            this.userManager = userManager;
        }
        // GET: Administrador
        public ActionResult Index()
        {
            return View(dbContex.Users.Include(x => x.Anuncios).ToList());
        }

        public ActionResult Publicaciones()
        {
            return View(dbContex.Anuncio.ToList());

        }

        public ActionResult DeletePublicaciones(int id)
        {
            if (id != 0)
            {
                var Ads = dbContex.Anuncio.Where(x => x.Id == id).First();
                dbContex.Anuncio.Remove(Ads);
                dbContex.SaveChanges();
                return RedirectToAction();
            }
            return RedirectToAction();
        }

        public IActionResult LockPublicaciones(int id)
        {
            if (id != 0)
            {
                var user = dbContex.Anuncio.Where(x => x.Id == id).First();
                user.Bloqueado = !user.Bloqueado;
                dbContex.SaveChanges();
                return RedirectToAction();
            }
            return RedirectToAction();
        }

        public ActionResult Usuarios()
        {
            return View(dbContex.Users.ToList());
        }

        public IActionResult LockUsuario(string id)
        {
            if (id != null)
            {
                var user = dbContex.Users.Where(x => x.Id == id).First();
                user.Bloqueado = !user.Bloqueado;
                dbContex.SaveChanges();
                return RedirectToAction();
            }
            return RedirectToAction();
        }

        public async Task<IActionResult> DeleteUsuario(string id)
        {
            if (id != null)
            {
            var user = await userManager.FindByIdAsync(id);
            await userManager.DeleteAsync(user);
            return RedirectToAction();
            }
            return RedirectToAction();
        }

        public ActionResult Configuracion()
        {
            return View();
        }

    }
}
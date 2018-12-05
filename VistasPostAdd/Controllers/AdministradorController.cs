using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private readonly SignInManager<AppUser> signInManager;
        private readonly IHostingEnvironment env;

        public AdministradorController(
            AppDbContex dbContex, 
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            IHostingEnvironment env)
        {
            this.dbContex = dbContex;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.env = env;
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

            var Ads = dbContex.Anuncio.Include(x => x.Imagen).Where(x => x.Id == id).First();
            foreach (var item in Ads.Imagen)
            {
                var filename = Path.Combine(env.WebRootPath + "/images/Anuncios/", item.NombreArchivo);
                System.IO.File.Delete(filename);

            }

            dbContex.Anuncio.Remove(Ads);
            dbContex.SaveChanges();
            return RedirectToAction("Publicaciones");
        }
            return RedirectToAction("Publicaciones");
        }

        public IActionResult LockPublicaciones(int id)
        {
            if (id != 0)
            {
                var user = dbContex.Anuncio.Where(x => x.Id == id).First();
                user.Bloqueado = !user.Bloqueado;
                dbContex.SaveChanges();
                return RedirectToAction("Publicaciones");
            }
            return RedirectToAction("Publicaciones");
        }

        public ActionResult Usuarios()
        {
            return View(dbContex.Users.Include(x => x.Anuncios).ToList());
        }

        public IActionResult LockUsuario(string id)
        {
            if (id != null)
            {
                var user = dbContex.Users.Where(x => x.Id == id).First();
                user.Bloqueado = !user.Bloqueado;
                dbContex.SaveChanges();
                return RedirectToAction("Usuarios");
            }
            return RedirectToAction("Usuarios");
        }

        public async Task<IActionResult> DeleteUsuario(string id)
        {
            if (id != null)
            {
            var user = await userManager.FindByIdAsync(id);
            await userManager.DeleteAsync(user);
            return RedirectToAction("Usuarios");
            }
            return RedirectToAction("Usuarios");
        }

        public ActionResult Categorias()
        {
            return View(dbContex.Categoria.ToList());
        }

        [HttpPost]
        public IActionResult CreateCategoria(string Categoria)
        {
            if (Categoria != null)
            {
                var cat = new Categoria { Nombre = Categoria };
                dbContex.Categoria.Add(cat);
                dbContex.SaveChanges();
            }
            
            return RedirectToAction("Categorias");
        }

        public IActionResult DeleteCategoria(int id)
        {
            if (id != 0)
            {
                var cat = dbContex.Categoria.Where(x => x.Id == id).First();
                dbContex.Categoria.Remove(cat);
                dbContex.SaveChanges();
            }

            return RedirectToAction("Categorias");
        }

    }
}
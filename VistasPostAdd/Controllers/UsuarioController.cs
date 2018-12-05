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
using VistasPostAdd.ViewModel;

namespace VistasPostAdd.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContex dbContex;
        private readonly IHostingEnvironment env;

        public UsuarioController(UserManager<AppUser> userManager, AppDbContex dbContex, IHostingEnvironment env)
        {
            this.userManager = userManager;
            this.dbContex = dbContex;
            this.env = env;
        }

        // GET: Usuario
        public async Task<ActionResult> Index()
        {
           var getUser = await userManager.GetUserAsync(User);
            var user = dbContex.Users.Include(x => x.Anuncios).Where(x => x.Id == getUser.Id).First();

            return View(user);
        }

        public async Task<ActionResult> Publicaciones()
        {
            var getUser = await userManager.GetUserAsync(User);
            var anuncios = dbContex.Anuncio.Where(x => x.AppUserId == getUser.Id).ToList();
            return View(anuncios);
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

        public async Task<ActionResult> Perfil()
        {
            var getUser = await userManager.GetUserAsync(User);
            return View(getUser);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePerfil(string id, UpdateUserVM model)
        {
            if (id != null)
            {

            var getUser = await userManager.FindByIdAsync(id);
                getUser.Nombre = model.Nombre;
                getUser.Apellido = model.Apellido;
                getUser.Email = model.Email;

                await userManager.UpdateAsync(getUser);
                if (model.Password != null && model.NewPassword != null)
                {
                await userManager.ChangePasswordAsync(getUser, model.Password, model.NewPassword);

                }
                return RedirectToAction("Perfil");
            }

            return RedirectToAction("Perfil");
        }

    }
}
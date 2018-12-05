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
    public class PublicarController : Controller
    {
        private readonly IHostingEnvironment env;
        private readonly AppDbContex dbContex;
        private readonly UserManager<AppUser> userManager;

        public PublicarController(IHostingEnvironment env, AppDbContex dbContex, UserManager<AppUser> userManager)
        {
            this.env = env;
            this.dbContex = dbContex;
            this.userManager = userManager;
        }
        // GET: Publicar
        public ActionResult Index()
        {

            return View();
        }

        // GET: Publicar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Publicar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnuncioVM model, IEnumerable<IFormFile> Imagen)
        {
            if (ModelState.IsValid && Imagen != null)
            {
                var UserId = userManager.GetUserId(User);
                //Guardando Anuncio
                var anuncio = new Anuncio {
                    Titulo = model.Titulo,
                    PrecioVenta = model.PrecioVenta,
                    Descripcion = model.Descripcion,
                    Estado = model.Estado,
                    FechaPublicaccion = DateTime.Now,
                    Bloqueado = false,
                    CategoriaId = model.Categoria,
                    AppUserId = UserId
                };

                dbContex.Anuncio.Add(anuncio);
                dbContex.SaveChanges();

                //Obteniendo Id Anuncio actual y guardando imagenes en tabla y carpetas
                var Anuncio = dbContex.Anuncio.Where(x => x.Titulo == model.Titulo).FirstOrDefault();
                foreach (var item in Imagen)
                {
                var fileName = Path.Combine(env.WebRootPath + "/images/Anuncios", Path.GetFileName(item.FileName));
                item.CopyTo(new FileStream(fileName, FileMode.Create));
                var img = new Imagen { NombreArchivo = item.FileName, AnuncioId = Anuncio.Id};

                    dbContex.Imagen.Add(img);
                    dbContex.SaveChanges();

                }

                return RedirectToAction("Index", "home");
            }
                return RedirectToAction("Index", model);
        }

        // GET: Publicar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Publicar/Edit/5
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

        // GET: Publicar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Publicar/Delete/5
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
using IdentityForum.Models;
using IdentityForum.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IdentityForum.Controllers
{
    public class ContaController : Controller
    {
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registrar(ContaRegistrarViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var dbContext = new IdentityDbContext("defaultConnection");

                var userStore = new UserStore<Usuario>(dbContext);
                var userManager = new UserManager<Usuario>(userStore);

                var novoUsuario = new Usuario();
                novoUsuario.Email = modelo.Email;
                novoUsuario.UserName = modelo.Username;
                novoUsuario.NomeCompleto = modelo.NomeCompleto;

                await userManager.CreateAsync(novoUsuario, modelo.Senha);

                //Podemos incluir o usuário
                return RedirectToAction("Index", "Home");
            }

            //Alguma coisa aconteceu
            return View(modelo);
        }
    }
}
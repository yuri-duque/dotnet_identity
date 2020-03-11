using IdentityForum.Models;
using IdentityForum.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentityForum.Controllers
{
    public class ContaController : Controller
    {
        private UserManager<Usuario> _userManager;
        public UserManager<Usuario> UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    var contextOwin = HttpContext.GetOwinContext();
                    _userManager = contextOwin.GetUserManager<UserManager<Usuario>>();
                }
                return _userManager;
            }
            set
            {
                _userManager = value;
            }
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registrar(ContaRegistrarViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var novoUsuario = new Usuario();
                novoUsuario.Email = modelo.Email;
                novoUsuario.UserName = modelo.Username;
                novoUsuario.NomeCompleto = modelo.NomeCompleto;

                var resultado = await UserManager.CreateAsync(novoUsuario, modelo.Senha);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AdicionaErros(resultado);
                }                
            }

            return View(modelo);
        }

        private void AdicionaErros(IdentityResult resultado)
        {
            foreach (var erro in resultado.Errors)
            {
                ModelState.AddModelError("", erro);
            }
        }
    }
}
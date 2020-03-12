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

                var usuario = await UserManager.FindByEmailAsync(modelo.Email);
                var usuarioJaExiste = usuario != null;

                if (usuarioJaExiste)
                    return View("AguardandoConfirmacao");

                var resultado = await UserManager.CreateAsync(novoUsuario, modelo.Senha);

                if (resultado.Succeeded)
                {
                    await EnviarEmailDeConfirmacaoAsync(novoUsuario);
                    return View("AguardandoConfirmacao");
                }
                else
                {
                    AdicionaErros(resultado);
                }
            }

            return View(modelo);
        }

        public async Task EnviarEmailDeConfirmacaoAsync(Usuario usuario)
        {
            var token = await UserManager.GenerateEmailConfirmationTokenAsync(usuario.Id);

            var linkDeCallBack = Url.Action(
                "ConfirmacaoEmail",
                "Conta",
                new { usuarioId = usuario.Id, token },
                Request.Url.Scheme
                );

            await UserManager.SendEmailAsync(
                usuario.Id,
                "Fórum Identity - Confirmação de Email",
                $"Bem vindo ao fórum Identity, clique aqui {linkDeCallBack} para confirmar seu endereço de email!"
                );
        }

        public ActionResult ConfirmacaoEmail(string usuarioId, string token)
        {
            throw new NotImplementedException();
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
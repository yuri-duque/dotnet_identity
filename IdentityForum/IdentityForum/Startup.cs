using IdentityForum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using System.Data.Entity;
using IdentityForum.App_Start.Identity;

[assembly: OwinStartup(typeof(IdentityForum.Startup))]
namespace IdentityForum
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.CreatePerOwinContext<DbContext>(() => new IdentityDbContext<Usuario>("DefaultConnection")); //Pega a connection string

            builder.CreatePerOwinContext<IUserStore<Usuario>>(
                (opcoes, contextoOwin) =>
                {
                    var dbContext = contextoOwin.Get<DbContext>(); // inicializa o dbContext
                    return new UserStore<Usuario>(dbContext);
                });

            builder.CreatePerOwinContext<UserManager<Usuario>>(
                (opcoes, contextoOwin) =>
                {
                    var userStore = contextoOwin.Get<IUserStore<Usuario>>(); // pega a store
                    var userManager = new UserManager<Usuario>(userStore); // pega o manager

                    var userValidator = new UserValidator<Usuario>(userManager);
                    userValidator.RequireUniqueEmail = true; 

                    userManager.UserValidator = userValidator;
                    userManager.PasswordValidator = new SenhaValidador()
                    {
                        TamanhoRequerido = 6,
                        ObrigatorioCaracteresEspeciais = true,
                        ObrigatorioDigitos = true,
                        ObrigatorioLowerCase = true,
                        ObrigatorioUpperCase = true
                    };

                    userManager.EmailService = new EmailService();

                    return userManager;
                });
        }
    }
}
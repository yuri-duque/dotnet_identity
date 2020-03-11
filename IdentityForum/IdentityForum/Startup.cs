using IdentityForum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using System.Data.Entity;


[assembly: OwinStartup(typeof(IdentityForum.Startup))]
namespace IdentityForum
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.CreatePerOwinContext<DbContext>(() => new IdentityDbContext<Usuario>("defaultConnection"));

            builder.CreatePerOwinContext<UserManager<Usuario>>((opcoes, contextOwin) =>
            {
                var userStore = contextOwin.Get<IUserStore<Usuario>>();
                var userManager = new UserManager<Usuario>(userStore);

                var userValidator = new UserValidator<Usuario>(userManager);
                userValidator.RequireUniqueEmail = true;

                userManager.UserValidator = userValidator;

                return userManager;
            });
        }
    }
}
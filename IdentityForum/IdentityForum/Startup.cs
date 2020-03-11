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

            builder.CreatePerOwinContext<IUserStore<Usuario>>((opcoes, contextOwin) =>
            {
                var dbContex = contextOwin.Get<DbContext>();
                return new UserStore<Usuario>(dbContex);
            });
        }
    }
}
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IdentityForum.App_Start.Identity
{
    public class EmailServico : IIdentityMessageService
    {
        private readonly string EMAIL_ORIGEM = ConfigurationManager.AppSettings["emailServico:email_remetente"]; //pegando do arquivo de configuração
        private readonly string EMAIL_SENHA = ConfigurationManager.AppSettings["emailServico:email_senha"]; //pegando do arquivo de configuração

        public async Task SendAsync(IdentityMessage message)
        {
            
        }
    }
}
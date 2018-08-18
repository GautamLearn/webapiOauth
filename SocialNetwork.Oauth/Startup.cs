using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SocialNetwork.Oauth.Startup))]

namespace SocialNetwork.Oauth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var inMemoryManager= new InMemoryManager();
           // var certificate = Convert.FromBase64String(ConfigurationManager.AppSettings["SigningCertificate"]);

            var certificate = Convert.ToBase64String(File.ReadAllBytes(@"C:\certificates\sign\Democert.pfx"));

            var factory = new IdentityServerServiceFactory()
                .UseInMemoryUsers(inMemoryManager.GetUsers())
                .UseInMemoryScopes(inMemoryManager.GetScopes())
                .UseInMemoryClients(inMemoryManager.GetClients());

            var options = new IdentityServerOptions
            {
                  
                SigningCertificate = new X509Certificate2(Convert.FromBase64String(certificate), "Gajua@123"),
                RequireSsl = false, //do not do this in prod
                Factory = factory
                
            };

            app.UseIdentityServer(options);
        }
    }
}

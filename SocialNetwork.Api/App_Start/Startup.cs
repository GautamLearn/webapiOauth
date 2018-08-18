using System;
using System.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using SocialNetwork.Api.Autofac.Modules;

[assembly: OwinStartup(typeof(SocialNetwork.Api.Startup))]
namespace SocialNetwork.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = GlobalConfiguration.Configuration;

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<SocialNetworkModule>();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //var certificate = new X509Certificate2(Convert.FromBase64String("MIIC9TCCAd2gAwIBAgIQMlrWG4601pBI01XrMJ7txjANBgkqhkiG9w0BAQsFADAXMRUwEwYDVQQDEwxEaXZpbmVMb3ZlLTIwHhcNMTgwODE1MDM0NDI0WhcNMTkwODE0MDAwMDAwWjAXMRUwEwYDVQQDEwxEaXZpbmVMb3ZlLTIwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDeEZbbcn0plLQxS02KWAED401N+GWw2zhbQYWVXgOMQR0p83oIWrJV96bOa/IrM9xmRagPmHlqlMOAhjyYh9Nt4eult6pphCYFES9EFJnNsdlqlKDV0xqn1G4KeTW4xorrMYgJ8uyMIVxU+fy/9ulaLu0aydP4Zjao5Y2h6LfSzePXfUQoHt3dtsmj/j9g5DAc8aevGt4+tU03OZdV+fjBw+V7A81yxPq0SRZ80BRaVmGjEJNLXVVb5U9KhtbQOBi1YNUoSAi0aUgzHosAFiGfhA6GO8Ol870XOA9yJtDr/HnbiFqBdFS5YsdtGv/QKl+THe8Isyu2bp/MB7FxzKqdAgMBAAGjPTA7MAsGA1UdDwQEAwIEMDATBgNVHSUEDDAKBggrBgEFBQcDATAXBgNVHREEEDAOggxEaXZpbmVMb3ZlLTIwDQYJKoZIhvcNAQELBQADggEBAB4TEXbuiTdzUiFiT0GqNgf/WzCQF4DHfgziDUc8rk38jSWyNQcRXlAyiwtA0HwH0N0AYTBaALVvUt3ls0EX+zGG8TA1q4d7Y+zb4HmQ4su+EkLwFPCfgUlkDZ+YKsp5HfPQ0O9A0WC3XwfCj2bD9i7zipaPsCoD3hK78LpGMolqFdPgaPM71NXOatSxqgtaW73VklNRJF7JfUiBIEgL91LJIermlaGe3H4AgckKAu4Jx6jGjnyUEC4wcnB/N67l6zkNhPYKq2thVfdzpeGIX1S8e5ogUlKBt7IjmPidDwKAoNBHaNLsQtboBQLxHFl4+1t5II5QS8yl17UrsLNxLMM="));

            //app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            //{
            //    AllowedAudiences = new[] { "http://localhost:63409/resources" },
            //    TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidAudience = "http://localhost:63409/resources",
            //        ValidIssuer = "http://localhost:63409",
            //        IssuerSigningKey = new X509SecurityKey(certificate)
            //    }
            //});

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:63409",
                
            });

     
            

            app.UseWebApi(config);
        }
    }
}

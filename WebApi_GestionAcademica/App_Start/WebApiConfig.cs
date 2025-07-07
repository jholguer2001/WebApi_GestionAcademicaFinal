using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi_GestionAcademica
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // CORS configurado correctamente - orígenes separados por comas
            var corsAttr = new EnableCorsAttribute(
                origins: "http://localhost:3000,https://gestionuta.netlify.app,http://127.0.0.1:5500,http://localhost:5500",
                headers: "*",
                methods: "*"
            );
            config.EnableCors(corsAttr);

            // Configuración JSON
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Configuración de rutas
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
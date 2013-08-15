using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace NewsSystem.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{token}",
                defaults: new { token = RouteParameter.Optional }
            );
        }
    }
}

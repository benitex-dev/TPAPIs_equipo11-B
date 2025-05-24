﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TPAPIs_equipo11_B
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // config.Formatters.Remove(config.Formatters.JsonFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
     new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();


        }

    }
}

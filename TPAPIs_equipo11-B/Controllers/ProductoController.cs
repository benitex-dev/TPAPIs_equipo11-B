using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TPAPIs_equipo11_B.Controllers
{
    public class ProductoController : ApiController
    {
        ArticuloNegocio negocio;
        
        // GET: api/Producto
        public IEnumerable<Articulo> Get()
        {   
            negocio= new ArticuloNegocio(); 
            return negocio.listar();
        }

        // GET: api/Producto/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Producto
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Producto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
        }
    }
}

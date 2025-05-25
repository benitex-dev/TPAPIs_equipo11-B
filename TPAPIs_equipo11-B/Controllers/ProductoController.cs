using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TPAPIs_equipo11_B.DTOs;

namespace TPAPIs_equipo11_B.Controllers
{
    public class ProductoController : ApiController
    {
        ArticuloNegocio negocio;

        // GET: api/Producto
        public IEnumerable<Articulo> Get()
        {
            negocio = new ArticuloNegocio();
            return negocio.listar();
        }

        // GET: api/Producto/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Producto
        public HttpResponseMessage Post([FromBody] ArticuloDTO articulo)
        {


            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo nuevo = new Articulo();

                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                nuevo.CodArticulo = articulo.CodArticulo;
                nuevo.Nombre = articulo.Nombre;
                nuevo.Descripcion = articulo.Descripcion;
                nuevo.Marca = new Marca { Id = articulo.IdMarca };
                nuevo.Categoria = new Categoria { Id = articulo.IdCategoria };
                nuevo.Precio = articulo.Precio;

                if (string.IsNullOrEmpty(articulo.CodArticulo) ||
                    string.IsNullOrEmpty(articulo.Nombre) ||
                    string.IsNullOrEmpty(articulo.Descripcion) ||
                    articulo.Precio <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Todos los campos son requeridos");
                    

                Marca marca = marcaNegocio.listar().Find(x => x.Id == articulo.IdMarca);
                Categoria categoria = categoriaNegocio.listar().Find(x => x.Id == articulo.IdCategoria);

                if (marca == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La marca no existe");

                if (categoria == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoría no existe");

                negocio.agregarArticulo(nuevo);



                return Request.CreateResponse(HttpStatusCode.Created, "Articulo agregado");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }


        }



        // POST: api/Imagen
        [Route("api/Producto/Imagenes")]
        [HttpPost]
        public HttpResponseMessage PostImg([FromBody] ImgDTO imagen)
        {
            try
            {
                negocio = new ArticuloNegocio();
                ImagenNegocio imagenNegocio = new ImagenNegocio();
                //Creamos el articulo donde vamos a guardar nuestro listado de imagenes
                Articulo articulo = new Articulo();


                //listamos los productos
                List<Articulo> articulos = new List<Articulo>();
                articulos = negocio.listar();

                //en nuestro listado de productos buscamos por id el producto
                foreach (var art in articulos)
                {
                    //si encontramos un producto que coincida con el id que recibimos por parametro,
                    //guardamos ese producto en la variable de tipo de Articulo
                    if (art.Id == imagen.IdArticulo)
                    {
                        articulo = art;
                        break;
                    }

                }

                if (articulo.Id != 0)
                {
                    foreach (var urlImg in imagen.Imagenes)
                    {
                        Imagen nuevaImagen = new Imagen();
                        nuevaImagen.IdArticulo = imagen.IdArticulo;
                        nuevaImagen.URL = urlImg.ToString();
                        imagenNegocio.agregarImagenArticulo(nuevaImagen);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "Imagen/es agregada correctamente.");

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Por favor debes ingresar un ID de producto valido");
                }


            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }





        }

        // PUT: api/Producto/5
        public HttpResponseMessage Put(int id, [FromBody] ArticuloDTO articulo)
        {


            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo modificado = new Articulo();

                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                modificado.CodArticulo = articulo.CodArticulo;
                modificado.Nombre = articulo.Nombre;
                modificado.Descripcion = articulo.Descripcion;
                modificado.Marca = new Marca { Id = articulo.IdMarca };
                modificado.Categoria = new Categoria { Id = articulo.IdCategoria };
                modificado.Precio = articulo.Precio;
                modificado.Id = id;

                if (string.IsNullOrEmpty(articulo.CodArticulo) ||
                    string.IsNullOrEmpty(articulo.Nombre) ||
                    string.IsNullOrEmpty(articulo.Descripcion) ||
                    articulo.Precio <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Todos los campos son requeridos");


                Marca marca = marcaNegocio.listar().Find(x => x.Id == articulo.IdMarca);
                Categoria categoria = categoriaNegocio.listar().Find(x => x.Id == articulo.IdCategoria);

                if (marca == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La marca no existe");

                if (categoria == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoría no existe");

                negocio.modificar(modificado);



                return Request.CreateResponse(HttpStatusCode.OK, "Articulo modificado");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }

        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
        }
    }
}

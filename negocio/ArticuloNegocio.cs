using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ArticuloNegocio
    {

       public int getUltimoRegistroInsertado()
        {   AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                int ultimo_registro = 0;
                accesoDatos.setConsulta("select MAX(ID) AS ultimo_registro FROM ARTICULOS");
                accesoDatos.ejecutarLectura();
                accesoDatos.Lector.Read();

                if (!((int)accesoDatos.Lector["ultimo_registro"] is DBNull))
                ultimo_registro =(int)accesoDatos.Lector["ultimo_registro"];
                
                return ultimo_registro;
                    
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { accesoDatos.cerrarConexion(); }
        }



        public void agregarArticulo(Articulo articulo)
        {   AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                //accesoDatos.setConsulta("INSERT INTO ARTICULOS (Codigo,Nombre,Descripcion,Precio,IdCategoria,IdMarca)values('"+articulo.CodArticulo+"','"+articulo.Nombre+"','"+articulo.Descripcion+"',"+articulo.Precio+",@IdCategoria,@IdMarca)");
                accesoDatos.setConsulta("INSERT INTO ARTICULOS (Codigo,Nombre,Descripcion,Precio,IdCategoria,IdMarca)values(@Codigo, @Nombre, @Descripcion, @Precio, @IdCategoria,@IdMarca)");
                accesoDatos.setParametro("@Codigo", articulo.CodArticulo);
                accesoDatos.setParametro("@Nombre", articulo.Nombre);
                accesoDatos.setParametro("@Descripcion", articulo.Descripcion);
                accesoDatos.setParametro("@Precio", articulo.Precio);
                accesoDatos.setParametro("@IdCategoria", articulo.Categoria.Id);
                accesoDatos.setParametro("@IdMarca", articulo.Marca.Id);
                accesoDatos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        public void eliminarArticulo(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setConsulta("delete from ARTICULOS where id = @id");
                datos.setParametro("@Id", @id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Articulo> listar()
        {
            List<Articulo> articulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setConsulta("SELECT A.ID, CODIGO, NOMBRE, A.DESCRIPCION, PRECIO, M.Descripcion Marca, C.Descripcion Categoria, A.IdCategoria, A.IdMarca FROM ARTICULOS A, MARCAS M, CATEGORIAS C WHERE M.ID = A.IDMARCA AND C.ID = A.IdCategoria");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["ID"];
                    articulo.Descripcion = (string)datos.Lector["DESCRIPCION"];
                    articulo.CodArticulo = (string)datos.Lector["CODIGO"];
                    articulo.Nombre = (string)datos.Lector["NOMBRE"];
                    
                    if (!(datos.Lector["Precio"] is DBNull))
                        articulo.Precio = decimal.Parse(datos.Lector["PRECIO"].ToString());
                    
                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)datos.Lector["Marca"];
                    
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    ImagenNegocio ImagenNegocio = new ImagenNegocio();
                    articulo.Imagenes = ImagenNegocio.listarImagenesArticulo(articulo.Id);
                   /* if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
                    {
                        articulo.Imagen = articulo.Imagenes[0];
                    }
                    else
                    {
                        articulo.Imagen = null;
                    }*/
                   

                    articulos.Add(articulo);
                }

                return articulos;
            }
            
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
           

            try
            {
                datos.setConsulta("UPDATE ARTICULOS SET Codigo = @cod, Nombre = @nombre, Descripcion = @desc, IdMarca = @idMarca, IdCategoria = @idCat, Precio = @precio WHERE Id = @id");
                datos.setParametro("@cod", articulo.CodArticulo);
                datos.setParametro("@nombre", articulo.Nombre);
                datos.setParametro("@desc", articulo.Descripcion);
                datos.setParametro("@idMarca", articulo.Marca.Id);
                datos.setParametro("@idCat", articulo.Categoria.Id);
                datos.setParametro("@precio", articulo.Precio);
                datos.setParametro("@id", articulo.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos data = new AccesoDatos();
            try
            {
                string consulta = "SELECT Codigo, Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, Precio, A.IdMarca, A.IdCategoria, A.Id \r\nFROM ARTICULOS A,MARCAS M,CATEGORIAS C \r\nWHERE\r\nM.Id = A.IdMarca\r\nAND C.Id=A.IdCategoria\r\nAND ";
                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Precio < " + filtro;
                            break;
                        default:
                            consulta += "Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Código")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Codigo like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Codigo like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Codigo like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "A.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "A.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                data.setConsulta(consulta);
                data.ejecutarLectura();

                while (data.Lector.Read())
                {
                    Articulo articuloAuxiliar = new Articulo();
                    articuloAuxiliar.Id = (int)data.Lector["Id"];
                    articuloAuxiliar.CodArticulo = (string)data.Lector["Codigo"];
                    articuloAuxiliar.Nombre = (string)data.Lector["Nombre"];
                    articuloAuxiliar.Descripcion = (string)data.Lector["Descripcion"];

                    

                    if (!(data.Lector["Precio"] is DBNull))
                        articuloAuxiliar.Precio = decimal.Parse(data.Lector["Precio"].ToString());


                    articuloAuxiliar.Marca = new Marca();
                    articuloAuxiliar.Marca.Id = (int)data.Lector["IdMarca"];
                    articuloAuxiliar.Marca.Descripcion = (string)data.Lector["Marca"];
                    articuloAuxiliar.Categoria = new Categoria();
                    articuloAuxiliar.Categoria.Id = (int)data.Lector["IdCategoria"];
                    articuloAuxiliar.Categoria.Descripcion = (string)data.Lector["Categoria"];


                    lista.Add(articuloAuxiliar);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

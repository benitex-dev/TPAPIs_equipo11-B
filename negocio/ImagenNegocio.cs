using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> listarImagenesArticulo(int id)
        {
            List<Imagen> listaImagen = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("select ImagenUrl from IMAGENES WHERE IDARTICULO ='" + id + "'");
                datos.ejecutarLectura();


                while (datos.Lector.Read())
                {
                    Imagen imagen = new Imagen();
                  
                    imagen.URL = (string)datos.Lector["ImagenUrl"];

                    listaImagen.Add(imagen);

                }

                return listaImagen;

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

        public void agregarImagenArticulo(Imagen nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("INSERT INTO IMAGENES (IDARTICULO, ImagenUrl) VALUES ('" + nueva.IdArticulo + "', '" + nueva.URL + "')");
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

        public void modificarImagenArticulo(Imagen imagen)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE IMAGENES SET ImagenUrl = '" + imagen.URL + "' WHERE IDARTICULO = '" + imagen.IdArticulo + "'");
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

        public void eliminarImagenArticulo(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {                
                datos.setConsulta("delete from IMAGENES where id = '"+ id +"'");                
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

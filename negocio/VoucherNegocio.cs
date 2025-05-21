using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class VoucherNegocio
    {
        public List<Voucher> listar()
        {
            List<Voucher> vouchers = new List<Voucher>();
            AccesoDatos datos = new AccesoDatos();


            try
            {
                datos.setConsulta("SELECT CODIGOVOUCHER, FECHACANJE, IDCLIENTE, IDARTICULO  FROM VOUCHERS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Voucher voucher = new Voucher();
                    voucher.CodigoVoucher = (string)datos.Lector["CodigoVoucher"];
                    if (!(datos.Lector["FechaCanje"] is DBNull))
                        voucher.FechaCanje = (DateTime)datos.Lector["FechaCanje"];
                    if (!(datos.Lector["IdCliente"] is DBNull))
                        voucher.IdCliente = (int)datos.Lector["IdCliente"];
                    else voucher.IdCliente = 0;

                    if (!(datos.Lector["IdArticulo"] is DBNull))
                        voucher.IdArticulo = (int)datos.Lector["IdArticulo"];
                    else voucher.IdArticulo = 0;
                    
                    vouchers.Add(voucher);
                }

                return vouchers;
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

        public void asignarVoucher(Voucher voucher, int idCliente,int idArt)
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.setConsulta("UPDATE VOUCHERS SET IDCLIENTE = @IdCliente, IDARTICULO = @IdArticulo, FechaCanje = GETDATE() WHERE CodigoVoucher = @CodigoVoucher ");
                accesoDatos.setParametro("@CodigoVoucher", voucher.CodigoVoucher);
                accesoDatos.setParametro("@IdCliente", idCliente);
                accesoDatos.setParametro("@IdArticulo", idArt);
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

        public string verificarVoucher(string codigoVoucher)
        {
            List<Voucher> listaVoucher = listar();

            foreach (Voucher voucher in listaVoucher)
            {
                if (voucher.CodigoVoucher == codigoVoucher)
                {
                    if (voucher.IdCliente == 0 && voucher.IdArticulo == 0)
                        return "Código válido";

                    return "Código ya usado";
                }
            }

            return "Código inválido";
        }
    }

}

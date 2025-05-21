using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Voucher
    {
        [DisplayName("Código Voucher")]
        public string CodigoVoucher {  get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaCanje { get; set; }
        public int IdArticulo { get; set; }
        public Articulo Articulo { get; set; }
        public Cliente Cliente { get; set; }
    }
}

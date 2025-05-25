using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPAPIs_equipo11_B.DTOs
{
    public class ArticuloDTO
    {

        public string CodArticulo { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }
        public decimal Precio { get; set; }
     
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1201
{
    public class DetalleFactura
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }

        public DetalleFactura()
        {
        }

        public DetalleFactura(string codigo, string descripcion, int cantidad, decimal precio, decimal total)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio;
            Total = total;
        }
    }
}

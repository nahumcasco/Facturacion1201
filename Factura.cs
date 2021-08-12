using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1201
{
    public class Factura
    {
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }

        public Factura()
        {
        }

        public Factura(int idCliente, DateTime fecha, int idUsuario, decimal subTotal, decimal descuento, decimal impuesto, decimal total)
        {
            IdCliente = idCliente;
            Fecha = fecha;
            IdUsuario = idUsuario;
            SubTotal = subTotal;
            Descuento = descuento;
            Impuesto = impuesto;
            Total = total;
        }
    }
}

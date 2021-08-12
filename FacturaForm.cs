using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Facturacion1201
{
    public partial class FacturaForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public FacturaForm()
        {
            InitializeComponent();
        }

        public string CodigoUsuario;
        public int IdCliente;
        int IdUsuario = 0;

        BaseDatos bd = new BaseDatos();
        Producto miProducto;

        List<DetalleFactura> misDetalles = new List<DetalleFactura>();

        decimal subTotal = decimal.Zero;
        decimal isv = decimal.Zero;
        decimal totalAPagar = decimal.Zero;

        private void FacturaForm_Load(object sender, EventArgs e)
        {
            UsuarioTextBox.Text = bd.GetNombreUsuario(CodigoUsuario);
            IdUsuario = bd.GetIdUsuario(CodigoUsuario);
            DetalleDataGridView.DataSource = misDetalles;
        }

        private void IdentidadMaskedEditBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                var datosCliente = bd.GetClienteParaFactura(IdentidadMaskedEditBox.Text);
                foreach (var item in datosCliente)
                {
                    IdCliente = item.Key;
                    NombreClienteTextBox.Text = item.Value;
                }
            }
            else
            {
                IdCliente = 0;
                NombreClienteTextBox.Clear();
            }
            
        }

        private void BuscarClienteButton_Click(object sender, EventArgs e)
        {
            BuscarClienteForm formulario = new BuscarClienteForm();
            formulario.ShowDialog();
            IdCliente = formulario.Id;
            IdentidadMaskedEditBox.Text = formulario.Identidad;
            NombreClienteTextBox.Text = formulario.Nombre;
        }

        private void CodigoProductoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                miProducto = new Producto();
                miProducto = bd.GetProdoctoPorCodigo(CodigoProductoTextBox.Text);
                DescripcionProductoTextBox.Text = miProducto.Descripcion;
                CantidadTextBox.Focus();
            }
            else
            {
                miProducto = null;
                DescripcionProductoTextBox.Clear();
                CantidadTextBox.Clear();
            }
        }

        private void CantidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(CantidadTextBox.Text))
            {
                DetalleFactura miDetalle = new DetalleFactura();
                miDetalle.Codigo = miProducto.Codigo;
                miDetalle.Descripcion = miProducto.Descripcion;
                miDetalle.Cantidad = Convert.ToInt32(CantidadTextBox.Text);
                miDetalle.Precio = miProducto.Precio;
                miDetalle.Total = Convert.ToInt32(CantidadTextBox.Text) * miProducto.Precio;

                subTotal += miDetalle.Total;

                isv = subTotal * 0.15M;
                totalAPagar = subTotal + isv;

                misDetalles.Add(miDetalle);
                DetalleDataGridView.DataSource = null;
                DetalleDataGridView.DataSource = misDetalles;

                SubTotalTextBox.Text = subTotal.ToString("N2");
                ImpuestoTextBox.Text =  isv.ToString("N2");
                TotalPagarTextBox.Text = totalAPagar.ToString("N2"); ;
            }
        }

        //private void DescuentoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(DescuentoTextBox.Text))
        //    {
        //        totalAPagar = totalAPagar - Convert.ToDecimal(DescuentoTextBox.Text);
        //        TotalPagarTextBox.Text = string.Format("{0:C2}", totalAPagar);
        //    }
        //}

        private void DescuentoTextBox_TextChanged(object sender, EventArgs e)
        {
            decimal descuento = !string.IsNullOrEmpty(DescuentoTextBox.Text) ? Convert.ToDecimal(DescuentoTextBox.Text) : 0M;
            TotalPagarTextBox.Text = (totalAPagar - descuento).ToString("N2");   
        }

        private async void GuardarFacturaButton_Click(object sender, EventArgs e)
        {
            Factura _factura = new Factura();
            _factura.IdCliente = IdCliente;
            _factura.Fecha = DateTime.Now;
            _factura.IdUsuario = IdUsuario;
            _factura.SubTotal = subTotal;
            _factura.Impuesto = isv;
            _factura.Total = Convert.ToDecimal(TotalPagarTextBox.Text);

            bool inserto = await bd.InsertarFacturaAsync(_factura, misDetalles);
            if (inserto)
            {
                MessageBox.Show("Factura guardada con exito");
            }

        }
    }
}

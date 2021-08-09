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

        BaseDatos bd = new BaseDatos();
        private void FacturaForm_Load(object sender, EventArgs e)
        {
            UsuarioTextBox.Text = bd.GetNombreUsuario(CodigoUsuario);
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
    }
}

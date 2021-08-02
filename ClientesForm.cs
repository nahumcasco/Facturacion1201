using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Facturacion1201
{
    public partial class ClientesForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ClientesForm()
        {
            InitializeComponent();
        }

        BaseDatos bd = new BaseDatos();

        private void ClientesForm_Load(object sender, EventArgs e)
        {
            ListarClientes();
        }

        private void ListarClientes()
        {
            ClientesDataGridView.DataSource = bd.SeleccionarClientes();
        }

        private void Imagenbutton_Click(object sender, EventArgs e)
        {

        }
    }
}

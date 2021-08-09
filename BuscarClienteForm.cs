using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Facturacion1201
{
    public partial class BuscarClienteForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public BuscarClienteForm()
        {
            InitializeComponent();
        }
        BaseDatos bd = new BaseDatos();

        public int Id;
        public string Identidad;
        public string Nombre;



        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (ClientesDataGridView.RowCount > 0)
            {
                if (ClientesDataGridView.SelectedRows.Count > 0)
                {
                    Id = (int)ClientesDataGridView.CurrentRow.Cells["ID"].Value;
                    Identidad = ClientesDataGridView.CurrentRow.Cells["IDENTIDAD"].Value.ToString();
                    Nombre = ClientesDataGridView.CurrentRow.Cells["NOMBRE"].Value.ToString();
                    this.Close();
                }
            }
        }

        private void BuscarClienteForm_Load(object sender, EventArgs e)
        {
            ClientesDataGridView.DataSource = bd.SeleccionarClientes();
        }

        private void NombreClienteTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ClientesDataGridView.DataSource = bd.SeleccionarClientesPorNombre(NombreClienteTextBox.Text);
        }

        private void Cancelarbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

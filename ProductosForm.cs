using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Facturacion1201
{
    public partial class ProductosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ProductosForm()
        {
            InitializeComponent();
        }

        private string operacion = string.Empty;

        private void ProductosForm_Load(object sender, EventArgs e)
        {
            BaseDatos bd = new BaseDatos();
            CategoriaComboBox.DataSource = bd.CargarCategorias();
            CategoriaComboBox.DisplayMember = "DESCRIPCION";
            CategoriaComboBox.ValueMember = "ID";

            ListarProductos();
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            operacion = "Nuevo";
            HabilitarControles();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            BaseDatos bd = new BaseDatos();
            if (operacion == "Nuevo")
            {
                bd.InsertarProducto(CodigoTextBox.Text, DescripcionTextBox.Text, Convert.ToInt32(CategoriaComboBox.SelectedValue), Convert.ToDecimal(PrecioTextBox.Text), Convert.ToInt32(ExistenciaTextBox.Text));
                ListarProductos();
                LimpiarControles();
            }
        }

        private void ListarProductos()
        {
            BaseDatos bd = new BaseDatos();
            ProductosDataGridView.DataSource = bd.ListarProductos();
        }

        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            DescripcionTextBox.Enabled = true;
            CategoriaComboBox.Enabled = true;
            PrecioTextBox.Enabled = true;
            ExistenciaTextBox.Enabled = true;
        }

        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            DescripcionTextBox.Clear();
            PrecioTextBox.Clear();
            ExistenciaTextBox.Clear();
        }

    }
}

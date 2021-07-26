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
            if (string.IsNullOrEmpty(CodigoTextBox.Text))
            {
                errorProvider1.SetError(CodigoTextBox, "Ingrese el código");
                CodigoTextBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(DescripcionTextBox.Text))
            {
                errorProvider1.SetError(DescripcionTextBox, "Ingrese una descripción");
                DescripcionTextBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(PrecioTextBox.Text))
            {
                errorProvider1.SetError(PrecioTextBox, "Ingrese un precio");
                PrecioTextBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ExistenciaTextBox.Text))
            {
                errorProvider1.SetError(ExistenciaTextBox, "Ingrese una existencia");
                ExistenciaTextBox.Focus();
                return;
            }

            BaseDatos bd = new BaseDatos();
            if (operacion == "Nuevo")
            {
                bd.InsertarProducto(CodigoTextBox.Text, DescripcionTextBox.Text, Convert.ToInt32(CategoriaComboBox.SelectedValue), Convert.ToDecimal(PrecioTextBox.Text), Convert.ToInt32(ExistenciaTextBox.Text));
                ListarProductos();
                LimpiarControles();
                DesabilitarControles();
            }
            else if (operacion == "Modificar")
            {
                bool modifico = bd.EditarProducto(CodigoTextBox.Text, DescripcionTextBox.Text, Convert.ToInt32(CategoriaComboBox.SelectedValue), Convert.ToDecimal(PrecioTextBox.Text), Convert.ToInt32(ExistenciaTextBox.Text));
                ListarProductos();
                LimpiarControles();
                DesabilitarControles();
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
            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;

            ModificarButton.Enabled = false;
            NuevoButton.Enabled = false;

        }

        private void DesabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            DescripcionTextBox.Enabled = false;
            CategoriaComboBox.Enabled = false;
            PrecioTextBox.Enabled = false;
            ExistenciaTextBox.Enabled = false;
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;

            ModificarButton.Enabled = true;
            NuevoButton.Enabled = true;
        }

        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            DescripcionTextBox.Clear();
            PrecioTextBox.Clear();
            ExistenciaTextBox.Clear();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DesabilitarControles();
            LimpiarControles();
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            operacion = "Modificar";
            if (ProductosDataGridView.SelectedRows.Count > 0)
            {
                CodigoTextBox.Text = ProductosDataGridView.CurrentRow.Cells["CODIGO"].Value.ToString();
                DescripcionTextBox.Text = ProductosDataGridView.CurrentRow.Cells["DESCRIPCION"].Value.ToString();
                CategoriaComboBox.Text = ProductosDataGridView.CurrentRow.Cells["CATEGORIA"].Value.ToString();
                PrecioTextBox.Text = ProductosDataGridView.CurrentRow.Cells["PRECIO"].Value.ToString();
                ExistenciaTextBox.Text = ProductosDataGridView.CurrentRow.Cells["EXISTENCIA"].Value.ToString();
                HabilitarControles();
                
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila del producto");
            }

            
        }
    }
}

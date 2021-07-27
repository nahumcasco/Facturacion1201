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

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ImagenPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);


            if (operacion == "Nuevo")
            {
                bd.InsertarProducto(CodigoTextBox.Text, DescripcionTextBox.Text, Convert.ToInt32(CategoriaComboBox.SelectedValue), Convert.ToDecimal(PrecioTextBox.Text), Convert.ToInt32(ExistenciaTextBox.Text), ms.GetBuffer());
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
            ImagenPictureBox.Image = null;
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DesabilitarControles();
            LimpiarControles();
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            operacion = "Modificar";

            BaseDatos bd = new BaseDatos();

            if (ProductosDataGridView.SelectedRows.Count > 0)
            {
                CodigoTextBox.Text = ProductosDataGridView.CurrentRow.Cells["CODIGO"].Value.ToString();
                DescripcionTextBox.Text = ProductosDataGridView.CurrentRow.Cells["DESCRIPCION"].Value.ToString();
                CategoriaComboBox.Text = ProductosDataGridView.CurrentRow.Cells["CATEGORIA"].Value.ToString();
                PrecioTextBox.Text = ProductosDataGridView.CurrentRow.Cells["PRECIO"].Value.ToString();
                ExistenciaTextBox.Text = ProductosDataGridView.CurrentRow.Cells["EXISTENCIA"].Value.ToString();

                var temporal = bd.SeleccionarImagenproducto(ProductosDataGridView.CurrentRow.Cells["CODIGO"].Value.ToString());

                if (temporal.Length > 0)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(temporal);
                    ImagenPictureBox.Image = System.Drawing.Bitmap.FromStream(ms);
                }
                else
                {
                    ImagenPictureBox.Image = null;
                }


                HabilitarControles();
                
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila del producto");
            }

            
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            if (ProductosDataGridView.SelectedRows.Count > 0)
            {
                BaseDatos bd = new BaseDatos();
                bool elimino = bd.EliminarProducto(ProductosDataGridView.CurrentRow.Cells[0].Value.ToString());
                ListarProductos();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila del producto");
            }
        }

        private void ImagenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                ImagenPictureBox.Image = Image.FromFile(dialog.FileName);
            }


        }

        private void PrecioTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}

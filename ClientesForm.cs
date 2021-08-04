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
        string operacion = string.Empty;

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
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                ImagenPictureBox.Image = Image.FromFile(dialog.FileName);
            }
        }

       

        private void HabilitarControles()
        {
            IdentidadtextBox.Enabled = true;
            NombretextBox.Enabled = true;
            TelefonotextBox.Enabled = true;
            DirecciontextBox.Enabled = true;

            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;
            ModificarButton.Enabled = false;
            NuevoButton.Enabled = false;
        }

        private void DesabilitarControles()
        {
            IdentidadtextBox.Enabled = false;
            NombretextBox.Enabled = false;
            TelefonotextBox.Enabled = false;
            DirecciontextBox.Enabled = false;

            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            ModificarButton.Enabled = true;
            NuevoButton.Enabled = true;
        }

        private void LimpiarControles()
        {
            IdentidadtextBox.Clear();
            NombretextBox.Clear();
            TelefonotextBox.Clear();
            DirecciontextBox.Clear();
            ImagenPictureBox.Image = null;
        }
        private void GuardarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdentidadtextBox.Text))
            {
                errorProvider1.SetError(IdentidadtextBox, "Debe ingresar una identidad");
                IdentidadtextBox.Focus();
                return;
            }
            if (NombretextBox.Text == "")
            {
                errorProvider1.SetError(NombretextBox, "Debe ingresar un nombre");
                NombretextBox.Focus();
                return;
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ImagenPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            if (operacion == "Nuevo")
            {
                try
                {
                    bool inserto = bd.InsertarCliente(IdentidadtextBox.Text, NombretextBox.Text, Convert.ToInt32(TelefonotextBox.Text), DirecciontextBox.Text, ms.GetBuffer());
                    if (inserto)
                    {
                        ListarClientes();
                        DesabilitarControles();
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar el nuevo cliente");
                    }
                }
                catch (Exception)
                {
                }
            }
            else if(operacion == "Modificar")
            {

            }


        }

        private void NuevoButton_Click_1(object sender, EventArgs e)
        {
            operacion = "Nuevo";
            HabilitarControles();
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            if (ClientesDataGridView.SelectedRows.Count > 0)
            {
                operacion = "Modificar";
                HabilitarControles();

                IdTextBox.Text = ClientesDataGridView.CurrentRow.Cells["ID"].Value.ToString();
                IdentidadtextBox.Text = ClientesDataGridView.CurrentRow.Cells["IDENTIDAD"].Value.ToString();
                NombretextBox.Text = ClientesDataGridView.CurrentRow.Cells["NOMBRE"].Value.ToString();
                TelefonotextBox.Text = ClientesDataGridView.CurrentRow.Cells["TELEFONO"].Value.ToString();
                DirecciontextBox.Text = ClientesDataGridView.CurrentRow.Cells["DIRECCION"].Value.ToString();

                var temporal = bd.SeleccionarImagenCliente(Convert.ToInt32(ClientesDataGridView.CurrentRow.Cells["ID"].Value.ToString()));

                if (temporal.Length > 0)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(temporal);
                    ImagenPictureBox.Image = System.Drawing.Bitmap.FromStream(ms);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un cliente");
            }
            

        }
    }
}

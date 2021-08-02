using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Facturacion1201
{
    public partial class Usuarios : Syncfusion.Windows.Forms.Office2010Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private string operacion = string.Empty;

        BaseDatos bd = new BaseDatos();


        private void NuevoButton_Click(object sender, EventArgs e)
        {
            operacion = "Nuevo";
            HabilitarControles();

        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            ListarUsuarios();
        }

        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            NombretextBox.Enabled = true;
            ContrasenatextBox.Enabled = true;
            EstadocheckBox.Enabled = true;
            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;

            ModificarButton.Enabled = false;
            NuevoButton.Enabled = false;
        }


        private void DesabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            NombretextBox.Enabled = false;
            ContrasenatextBox.Enabled = false;
            EstadocheckBox.Enabled = false;

            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;

            ModificarButton.Enabled = true;
            NuevoButton.Enabled = true;
        }

        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            NombretextBox.Clear();
            ContrasenatextBox.Clear();
            EstadocheckBox.Checked = false;
        }

        private void ListarUsuarios()
        {
            UsuariosdataGridView.DataSource = bd.SeleccionarUsuarios();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            if (operacion == "Nuevo")
            {
                bool inserto = bd.InsertarUsuario(CodigoTextBox.Text, NombretextBox.Text, ContrasenatextBox.Text);
                ListarUsuarios();
                LimpiarControles();
            }
            else if(operacion == "Modificar")
            {
                bool edito = bd.EditarUsuario(CodigoTextBox.Text, NombretextBox.Text, ContrasenatextBox.Text, EstadocheckBox.Checked);
                ListarUsuarios();
                LimpiarControles();
            }
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            operacion = "Modificar";

            if (UsuariosdataGridView.SelectedRows.Count > 0)
            {
                CodigoTextBox.Text = UsuariosdataGridView.CurrentRow.Cells["CODIGO"].Value.ToString();
                NombretextBox.Text = UsuariosdataGridView.CurrentRow.Cells["NOMBRE"].Value.ToString();
                ContrasenatextBox.Text = UsuariosdataGridView.CurrentRow.Cells["CLAVE"].Value.ToString();
                EstadocheckBox.Checked = Convert.ToBoolean(UsuariosdataGridView.CurrentRow.Cells["ESTAACTIVO"].Value);
                ListarUsuarios();
                HabilitarControles();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario");
            }

        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            if (UsuariosdataGridView.SelectedRows.Count > 0)
            {
                bool elimino = bd.EliminarUsuario(UsuariosdataGridView.CurrentRow.Cells["CODIGO"].Value.ToString());
                ListarUsuarios();

            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario");
            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DesabilitarControles();
            LimpiarControles();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Facturacion1201
{
    public partial class LoginForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (UsuarioTextBox.Text == "")
            {
                errorProvider1.SetError(UsuarioTextBox, "Ingrese el usuario");
                UsuarioTextBox.Focus();
                return;
            }
            errorProvider1.SetError(UsuarioTextBox, "");

            if (ContrasenaTextBox.Text == "")
            {
                errorProvider1.SetError(ContrasenaTextBox, "Ingrese una contraseña");
                ContrasenaTextBox.Focus();
                return;
            }
            errorProvider1.SetError(ContrasenaTextBox, "");

            //Canectar a la base de datos

            string cadena = "Data Source=192.168.1.28; Initial Catalog=FACTURACION1201; User ID=sa; Password=Estado2012";

            //SqlConnection _conexion = new SqlConnection(cadena);
            //_conexion.Open();

            bool EsUsuarioValido = false;

            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "SELECT 1 FROM USUARIOS WHERE CODIGO = '" + UsuarioTextBox.Text + "' AND CLAVE = '" + ContrasenaTextBox.Text + "';";

                conexion.Open();

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    EsUsuarioValido = Convert.ToBoolean(comando.ExecuteScalar());
                }
            }

            if (EsUsuarioValido)
            {
                PrincipalForm formulario = new PrincipalForm();
                this.Hide();
                formulario.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña invalida");
            }



            


        }
    }
}

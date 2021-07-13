using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            PrincipalForm formulario = new PrincipalForm();
            this.Hide();
            formulario.Show();


        }
    }
}

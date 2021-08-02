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
    public partial class PrincipalForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public PrincipalForm()
        {
            InitializeComponent();
        }

        Usuarios frmUsuarios;
        ProductosForm frmProductos;
        ClientesForm frmClientes;
        private void UsuariostoolStripButton_Click(object sender, EventArgs e)
        {
            if (frmUsuarios == null)
            {
                frmUsuarios = new Usuarios();
                frmUsuarios.MdiParent = this;
                frmUsuarios.FormClosed += FrmUsuarios_FormClosed;
                frmUsuarios.Show();
            }
            else
            {
                frmUsuarios.Activate();
            }
            
            
        }

        private void FrmUsuarios_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmUsuarios = null;
        }

        
        private void ProductostoolStripButton_Click(object sender, EventArgs e)
        {
            if (frmProductos == null)
            {
                frmProductos = new ProductosForm();
                frmProductos.MdiParent = this;
                frmProductos.FormClosed += FrmProductos_FormClosed;
                frmProductos.Show();
            }
            else
            {
                frmProductos.Activate();
            }
        }

        private void FrmProductos_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmProductos = null;
        }

        private void PrincipalForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Desea salir del sistema?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            Application.Exit();
        }

        private void ClientestoolStripButton_Click(object sender, EventArgs e)
        {
            if (frmClientes == null)
            {
                frmClientes = new ClientesForm();
                frmClientes.MdiParent = this;
                frmClientes.FormClosed += FrmClientes_FormClosed;
                frmClientes.Show();
            }
            else
            {
                frmClientes.Activate();
            }
        }

        private void FrmClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmClientes = null;
        }
    }
}

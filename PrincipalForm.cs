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
    }
}

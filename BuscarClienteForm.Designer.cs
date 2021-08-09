
namespace Facturacion1201
{
    partial class BuscarClienteForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.NombreClienteTextBox = new System.Windows.Forms.TextBox();
            this.AceptarButton = new System.Windows.Forms.Button();
            this.Cancelarbutton = new System.Windows.Forms.Button();
            this.ClientesDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ClientesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // NombreClienteTextBox
            // 
            this.NombreClienteTextBox.Location = new System.Drawing.Point(143, 51);
            this.NombreClienteTextBox.Name = "NombreClienteTextBox";
            this.NombreClienteTextBox.Size = new System.Drawing.Size(378, 20);
            this.NombreClienteTextBox.TabIndex = 1;
            this.NombreClienteTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NombreClienteTextBox_KeyUp);
            // 
            // AceptarButton
            // 
            this.AceptarButton.Location = new System.Drawing.Point(356, 117);
            this.AceptarButton.Name = "AceptarButton";
            this.AceptarButton.Size = new System.Drawing.Size(75, 23);
            this.AceptarButton.TabIndex = 2;
            this.AceptarButton.Text = "Aceptar";
            this.AceptarButton.UseVisualStyleBackColor = true;
            this.AceptarButton.Click += new System.EventHandler(this.AceptarButton_Click);
            // 
            // Cancelarbutton
            // 
            this.Cancelarbutton.Location = new System.Drawing.Point(446, 117);
            this.Cancelarbutton.Name = "Cancelarbutton";
            this.Cancelarbutton.Size = new System.Drawing.Size(75, 23);
            this.Cancelarbutton.TabIndex = 3;
            this.Cancelarbutton.Text = "Cancelar";
            this.Cancelarbutton.UseVisualStyleBackColor = true;
            this.Cancelarbutton.Click += new System.EventHandler(this.Cancelarbutton_Click);
            // 
            // ClientesDataGridView
            // 
            this.ClientesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClientesDataGridView.Location = new System.Drawing.Point(0, 188);
            this.ClientesDataGridView.Name = "ClientesDataGridView";
            this.ClientesDataGridView.Size = new System.Drawing.Size(545, 175);
            this.ClientesDataGridView.TabIndex = 4;
            // 
            // BuscarClienteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 362);
            this.Controls.Add(this.ClientesDataGridView);
            this.Controls.Add(this.Cancelarbutton);
            this.Controls.Add(this.AceptarButton);
            this.Controls.Add(this.NombreClienteTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BuscarClienteForm";
            this.Text = "Buscar Cliente";
            this.Load += new System.EventHandler(this.BuscarClienteForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ClientesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NombreClienteTextBox;
        private System.Windows.Forms.Button AceptarButton;
        private System.Windows.Forms.Button Cancelarbutton;
        private System.Windows.Forms.DataGridView ClientesDataGridView;
    }
}
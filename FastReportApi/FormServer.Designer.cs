
namespace FastReportApi
{
    partial class FormServer
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormServer));
            this.btImprimir = new System.Windows.Forms.Button();
            this.btDisenear = new System.Windows.Forms.Button();
            this.btDesignOneTable = new System.Windows.Forms.Button();
            this.btPrintOneTable = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.btPreviewOneTable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btImprimir
            // 
            this.btImprimir.Location = new System.Drawing.Point(28, 18);
            this.btImprimir.Name = "btImprimir";
            this.btImprimir.Size = new System.Drawing.Size(75, 23);
            this.btImprimir.TabIndex = 0;
            this.btImprimir.Text = "Imprimir";
            this.btImprimir.UseVisualStyleBackColor = true;
            this.btImprimir.Click += new System.EventHandler(this.button1_Click);
            // 
            // btDisenear
            // 
            this.btDisenear.Location = new System.Drawing.Point(109, 18);
            this.btDisenear.Name = "btDisenear";
            this.btDisenear.Size = new System.Drawing.Size(75, 23);
            this.btDisenear.TabIndex = 1;
            this.btDisenear.Text = "diseñar";
            this.btDisenear.UseVisualStyleBackColor = true;
            this.btDisenear.Click += new System.EventHandler(this.btDisenear_Click);
            // 
            // btDesignOneTable
            // 
            this.btDesignOneTable.Location = new System.Drawing.Point(153, 58);
            this.btDesignOneTable.Name = "btDesignOneTable";
            this.btDesignOneTable.Size = new System.Drawing.Size(113, 23);
            this.btDesignOneTable.TabIndex = 3;
            this.btDesignOneTable.Text = "Design One Table";
            this.btDesignOneTable.UseVisualStyleBackColor = true;
            this.btDesignOneTable.Click += new System.EventHandler(this.btDesignOneTable_Click);
            // 
            // btPrintOneTable
            // 
            this.btPrintOneTable.Location = new System.Drawing.Point(30, 58);
            this.btPrintOneTable.Name = "btPrintOneTable";
            this.btPrintOneTable.Size = new System.Drawing.Size(117, 23);
            this.btPrintOneTable.TabIndex = 0;
            this.btPrintOneTable.Text = "Print One Table";
            this.btPrintOneTable.UseVisualStyleBackColor = true;
            this.btPrintOneTable.Click += new System.EventHandler(this.btPrintOneTable_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            // 
            // btPreviewOneTable
            // 
            this.btPreviewOneTable.Location = new System.Drawing.Point(272, 58);
            this.btPreviewOneTable.Name = "btPreviewOneTable";
            this.btPreviewOneTable.Size = new System.Drawing.Size(113, 23);
            this.btPreviewOneTable.TabIndex = 3;
            this.btPreviewOneTable.Text = "Preview One Table";
            this.btPreviewOneTable.UseVisualStyleBackColor = true;
            this.btPreviewOneTable.Click += new System.EventHandler(this.btPreviewOneTable_Click);
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 104);
            this.Controls.Add(this.btDisenear);
            this.Controls.Add(this.btImprimir);
            this.Controls.Add(this.btPrintOneTable);
            this.Controls.Add(this.btPreviewOneTable);
            this.Controls.Add(this.btDesignOneTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormServer";
            this.ShowInTaskbar = false;
            this.Text = "Servidor";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btImprimir;
        private System.Windows.Forms.Button btDisenear;
        private System.Windows.Forms.Button btDesignOneTable;
        private System.Windows.Forms.Button btPrintOneTable;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button btPreviewOneTable;
    }
}


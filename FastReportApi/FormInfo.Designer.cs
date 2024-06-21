
namespace FastReportApi
{
    partial class FormInfo
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
            this.laMessage = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // laMessage
            // 
            this.laMessage.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.laMessage.Location = new System.Drawing.Point(24, 17);
            this.laMessage.Name = "laMessage";
            this.laMessage.Size = new System.Drawing.Size(268, 84);
            this.laMessage.TabIndex = 0;
            this.laMessage.Text = "label1";
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(120, 113);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 1;
            this.btOk.Text = "Aceptar";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // FormInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 149);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.laMessage);
            this.MaximumSize = new System.Drawing.Size(333, 188);
            this.MinimumSize = new System.Drawing.Size(333, 188);
            this.Name = "FormInfo";
            this.Text = "Info";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laMessage;
        private System.Windows.Forms.Button btOk;
    }
}
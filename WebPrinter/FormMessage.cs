using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebPrinter
{
    public partial class FormMessage : Form
    {
        public FormMessage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
        }

        private void FormMessage_Load(object sender, EventArgs e)
        {
            // Obtener el tamaño de la pantalla de trabajo (excluyendo la barra de tareas)
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

            // Establecer la nueva posición del formulario
            int newX = (workingArea.Width - this.Width) / 2; // Centrado horizontalmente
            int newY = workingArea.Bottom - this.Height; // Parte inferior de la pantalla

            // Establecer la ubicación del formulario
            this.Location = new Point(newX, newY);
        }
    }
}

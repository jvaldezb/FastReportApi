using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintConfiguration
{
    public partial class FormConfiguracion : Form
    {
        private Configuracion _configuracion;

        public FormConfiguracion()
        {
            InitializeComponent();
        }

        public string Puerto
        {
            get { return tbNumPort.Text; }
            set { tbNumPort.Text = value; }
        }

        public bool Imprimir
        {
            get { return rbPrint.Checked; }
            set { rbPrint.Checked = value; }
        }

        public bool Disenar
        {
            get { return rbDesign.Checked; }
            set { rbDesign.Checked = value; }
        }

        #region Configuración
        // Función para cargar la configuración desde un archivo JSON        
        private void CargarConfiguracion(string rutaArchivo)
        {
            
            // Verifica si el archivo existe
            if (File.Exists(rutaArchivo))
            {
                try
                {
                    // Lee todo el contenido del archivo JSON
                    string contenidoJson = File.ReadAllText(rutaArchivo);

                    // Deserializa el contenido JSON en un objeto de la clase Configuracion
                    _configuracion = JsonConvert.DeserializeObject<Configuracion>(contenidoJson);

                    // Muestra el puerto en el TextBox
                    Puerto = _configuracion.Puerto;

                    // Establecer la selección de los RadioButtons
                    Imprimir = _configuracion.Imprimir;
                    Disenar = _configuracion.Disenar;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al leer el archivo de configuración: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("El archivo de configuración no existe.");
            }
        }

        // Función para guardar la configuración en el archivo JSON
        private void GuardarConfiguracion(string rutaArchivo)
        {
            try
            {
                // Serializa la configuración en formato JSON
                string jsonConfiguracion = JsonConvert.SerializeObject(_configuracion);

                // Escribe el JSON en el archivo
                File.WriteAllText(rutaArchivo, jsonConfiguracion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el archivo de configuración: " + ex.Message);
            }
        }

        #endregion

        // Evento para modificar el puerto
        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Actualiza el puerto en la configuración
                _configuracion.Puerto = Puerto;

                // Guarda la selección de los RadioButtons en la configuración
                _configuracion.Imprimir = rbPrint.Checked;
                _configuracion.Disenar = rbDesign.Checked;

                // Guarda la configuración en el archivo JSON
                GuardarConfiguracion("configuracion.json");

                MessageBox.Show("Configuración guardada correctamente.");
            }
            catch (ArgumentException ex)
            {
                this.DialogResult = DialogResult.None;
                MessageBox.Show("Error al modificar el puerto: " + ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormConfiguracion_Load(object sender, EventArgs e)
        {
            // Cargar configuración al inicio del formulario
            CargarConfiguracion("configuracion.json");
        }
    }
}

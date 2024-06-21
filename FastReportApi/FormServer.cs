using FastReport;
using FastReport.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Owin.Hosting;
using PrintConfiguration;
using FastReport.Preview;

namespace FastReportApi
{
    public enum ReportCommand
    {
        Print,
        Preview,
        Design
    }

    public partial class FormServer : Form, IReportBase
    {
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem toolStripMenuItemStart;
        private ToolStripMenuItem toolStripMenuItemStop;
        private ToolStripMenuItem toolStripMenuItemConfig;
        private ToolStripMenuItem toolStripMenuItemExit;

        private IDisposable _server;
        Report report = new Report();
        string _formato;
        string _reportName;
        string _jsonData;
        string _impresora;
        string _lista_tablas;
        NumTable _numTable = NumTable.Unica;
        string _puerto = "55677";
        string _operacion;
        ReportCommand _reportCommand = ReportCommand.Print;
        public static Configuracion configuracion;

        public string JsonData { get => _jsonData; }
        public string Impresora { get => _impresora; }
        public string ReportName { get => _reportName; }
        public string Formato { get => _formato; }
        public NumTable NumTables { get => _numTable; }

        public FormServer()
        {
            InitializeComponent();

            notifyIcon.Icon = Properties.Resources.WebPrinter; // Establece el ícono de la bandeja del sistema
            notifyIcon.Text = "WebPrinter";
            notifyIcon.Visible = true;

            // Crea un menú contextual para el NotifyIcon
            contextMenuStrip = new ContextMenuStrip();

            toolStripMenuItemStart = new ToolStripMenuItem();
            toolStripMenuItemStart.Text = "Iniciar";
            toolStripMenuItemStart.Click += ToolStripMenuItemIniciar_Click;
            contextMenuStrip.Items.Add(toolStripMenuItemStart);

            toolStripMenuItemStop = new ToolStripMenuItem();
            toolStripMenuItemStop.Text = "Detener";
            toolStripMenuItemStop.Click += ToolStripMenuItemDetener_Click;
            contextMenuStrip.Items.Add(toolStripMenuItemStop);

            toolStripMenuItemConfig = new ToolStripMenuItem();
            toolStripMenuItemConfig.Text = "Configurar";
            toolStripMenuItemConfig.Click += ToolStripMenuItemConfig_Click;
            contextMenuStrip.Items.Add(toolStripMenuItemConfig);

            toolStripMenuItemExit = new ToolStripMenuItem();
            toolStripMenuItemExit.Text = "Salir";
            toolStripMenuItemExit.Click += ToolStripMenuItemExit_Click;
            contextMenuStrip.Items.Add(toolStripMenuItemExit);

            notifyIcon.ContextMenuStrip = contextMenuStrip;

            toolStripMenuItemStart.Enabled = false;
            toolStripMenuItemStop.Enabled = true;

            // Establece el formulario para que no aparezca en ALT+TAB
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            // Cargar configuración al inicio del formulario
            CargarConfiguracion("configuracion.json");
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
                    configuracion = JsonConvert.DeserializeObject<Configuracion>(contenidoJson);

                    // Muestra el puerto en el TextBox
                    _puerto = configuracion.Puerto;

                    // actualizar flag de impresion | diseno
                    if (configuracion.Imprimir)
                        _reportCommand = ReportCommand.Print;
                    else if (configuracion.Disenar)
                        _reportCommand = ReportCommand.Design;
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

        #endregion


        #region Menú servidor
        private void ToolStripMenuItemIniciar_Click(object sender, EventArgs e)
        {
            Start();
            toolStripMenuItemStart.Enabled = false;
            toolStripMenuItemStop.Enabled = true;
        }

        private void ToolStripMenuItemDetener_Click(object sender, EventArgs e)
        {
            Stop();
            toolStripMenuItemStart.Enabled = true;
            toolStripMenuItemStop.Enabled = false;
        }

        private void ToolStripMenuItemConfig_Click(object sender, EventArgs e)
        {
            PrintConfiguration.FormConfiguracion formConfiguracion = new PrintConfiguration.FormConfiguracion();
            if (formConfiguracion.ShowDialog() == DialogResult.OK)
            {
                // Cargar configuración al inicio del formulario
                CargarConfiguracion("configuracion.json");

                // mensaje de servidor reiniciando
                toolStripMenuItemStop.PerformClick();
                toolStripMenuItemStart.PerformClick();
                
                MessageBox.Show("El servidor se reinició correctamente");
            }
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            // Cierra la aplicación cuando se hace clic en "Salir" en el menú contextual
            Application.Exit();
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Asegura que el icono de la bandeja del sistema se elimine correctamente cuando se cierre la aplicación
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            report.Load(_reportName);

            RegisterData();
            
            report.PrintSettings.ShowDialog = false;
            report.PrintSettings.Printer = _impresora;
            report.Print();
        }

        public string GetReportsFolder()
        {
            string thisFolder = Config.ApplicationFolder;

            for (int i = 0; i < 6; i++)
            {
                if (System.IO.Directory.Exists(thisFolder + @"Reports"))
                {
                    return Path.GetFullPath(thisFolder + @"Reports\");
                }
                thisFolder += @"..\";
            }

            throw new Exception("Could not locate the Reports folder.");
        }

        void RegisterData()
        {            
            var listaTablas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TablasReporte>>(_jsonData);

            foreach (var item in listaTablas)
            {
                DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(Convert.ToString(item.ListaTabla), typeof(DataTable));
                report.RegisterData(dataTable, item.NombreTabla);
            }
        }        

        void RegisterDataObject()
        {            
            if (_lista_tablas == null)
            {
                DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(_jsonData, typeof(DataTable));
                report.RegisterData(dataTable, _formato);               
            }
            else
            {
                var listaTablas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TablasReporte>>(_lista_tablas);

                foreach (var item in listaTablas)
                {
                    DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(Convert.ToString(item.ListaTabla), typeof(DataTable));
                    report.RegisterData(dataTable, item.NombreTabla);
                }
            }
        }

        private void btDisenear_Click(object sender, EventArgs e)
        {            
            report.Load(_reportName);

            RegisterData();            

            report.Design();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EventoComun.OnPrintRemoteFrxData += (RemoteFrxData remoteFrxData) => 
            {
                report = new Report();

                _reportName = GetReportsFolder();
                _reportName += remoteFrxData.FileName;
                File.WriteAllBytes(_reportName, Convert.FromBase64String(remoteFrxData.File64String));

                _jsonData = remoteFrxData.JsonData;

                if (btImprimir.InvokeRequired)
                    btImprimir.Invoke(new Action(() => btImprimir.PerformClick()));
                else
                    btImprimir.PerformClick();
            };

            EventoComun.OnPrintLocalFrxData += (LocalFrxData localFrxData) =>
            {
                report = new Report();

                _reportName = GetReportsFolder();

                _jsonData = Util.NormalizeJsonTable(localFrxData.JsonData);

                JObject requestData = JObject.Parse(localFrxData.JsonData);
                _impresora = requestData["impresora"]?.ToString();
                _formato = requestData["formato"]?.ToString();
                _operacion = requestData["operacion"]?.ToString();
                _lista_tablas = requestData["lista_tablas"]?.ToString();
                _reportName += _formato + ".frx";

                #region operación

                if (_operacion != null)
                {
                    if (_operacion == "imprimir")
                        _reportCommand = ReportCommand.Print;
                    else if (_operacion == "disenar")
                        _reportCommand = ReportCommand.Design;
                    else if (_operacion == "previsualizar")
                        _reportCommand = ReportCommand.Preview;
                }

                CrearReportFormat(_reportName);

                if (_reportCommand == ReportCommand.Print)
                {
                    if (btPrintOneTable.InvokeRequired)
                        btPrintOneTable.Invoke(new Action(() => btPrintOneTable.PerformClick()));
                    else
                        btPrintOneTable.PerformClick();
                }
                else if (_reportCommand == ReportCommand.Design)
                {
                    if (btDesignOneTable.InvokeRequired)
                        btDesignOneTable.Invoke(new Action(() => btDesignOneTable.PerformClick()));
                    else
                        btDesignOneTable.PerformClick();
                }
                else if (_reportCommand == ReportCommand.Preview)
                {
                    if (btPreviewOneTable.InvokeRequired)
                        btPreviewOneTable.Invoke(new Action(() => btPreviewOneTable.PerformClick()));
                    else
                        btPreviewOneTable.PerformClick();
                }

                #endregion

                #region lista_tablas

                if (_lista_tablas != null)
                {

                }

                #endregion
            };

            EventoComun.OnReportCommand += (int command) =>
            {
                if (command == 0)
                    _reportCommand = ReportCommand.Print;
                else if (command == 1)
                    _reportCommand = ReportCommand.Design;
            };

            Config.PreviewSettings.ShowInTaskbar = true;
            Config.PreviewSettings.TopMost = true;

            Config.DesignerSettings.ShowInTaskbar = true;            

            Start();
        }

        private void Start()
        {
            string baseAddress = $"http://localhost:{_puerto}/";
            // Iniciar el servidor y almacenar una referencia al objeto IDisposable
            _server = WebApp.Start<Startup>(url: baseAddress);
        }

        private void Stop()
        {
            // Verificar si el servidor está en ejecución
            if (_server != null)
            {
                // Detener el servidor llamando a Dispose() en el objeto IDisposable
                _server.Dispose();
                // Establecer la referencia a null para liberar la memoria
                _server = null;
            }
        }

        private void CrearReportFormat(string reportName)
        {
            if (!File.Exists(reportName))
            {
                // Crear una copia de blanco.frx 
                var directory = GetReportsFolder();
                var blanco = Path.Combine(directory, "blanco.frx");

                File.Copy(blanco, reportName);
            }
        }

        private void btDesignOneTable_Click(object sender, EventArgs e)
        {
            report.Dictionary.Clear();
            report.Load(_reportName);

            RegisterDataObject();

            report.Design();

            // Importante: Reiniciar el diseñador para asegurar que se reflejen los cambios
            //using (FastReport.Design.Designer designer = new FastReport.Design.Designer())
            //{
            //    designer.Report = report;
            //    designer.Refresh();
            //    designer.Show();
            //}            
        }

        private void btPrintOneTable_Click(object sender, EventArgs e)
        {
            try
            {
                report.Dictionary.Clear();
                report.Load(_reportName);

                RegisterDataObject();

                report.PrintSettings.ShowDialog = false;
                report.PrintSettings.Printer = _impresora;
                report.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verificar el formato de reporte en modo edición: "+ ex.Message);
            }
        }

        private void btPreviewOneTable_Click(object sender, EventArgs e)
        {
            report.Dictionary.Clear();
            report.Load(_reportName);

            RegisterDataObject();

            report.Show(false);

            // Importante: Reiniciar el diseñador para asegurar que se reflejen los cambios
            //using (FastReport.Design.Designer designer = new FastReport.Design.Designer())
            //{                
            //    designer.Report = report;
            //    designer.Refresh();
            //    designer.Show();
            //}


            FormMessage formMessage = new FormMessage();
            formMessage.TopMost = true;            
            if (formMessage.ShowDialog() == DialogResult.Yes)
            {
                report.PrintSettings.ShowDialog = false;
                report.PrintSettings.Printer = _impresora;
                report.Print();
            }

            ClosePreview(report);
        }

        public void ConfigurePreviewForm(Report report, int width, int height)
        {
            // Mostrar el reporte en la vista previa
            report.Prepare();
            report.ShowPrepared();

            // Verificar que la ventana de vista previa está disponible
            if (report.Preview != null)
            {
                // Obtener el formulario de vista previa desde el control de vista previa
                Form previewForm = report.Preview.FindForm();

                // Configurar el tamaño del formulario de vista previa
                if (previewForm != null)
                {
                    previewForm.Width = width;
                    previewForm.Height = height;
                }
            }
        }

        public void ClosePreview(Report report)
        {
            // Asegúrate de que el reporte tiene una ventana de vista previa asociada
            if (report.Preview != null)
            {
                PreviewControl previewControl = report.Preview as PreviewControl;
                if (previewControl != null && previewControl.FindForm() != null)
                {
                    // Cierra el formulario de vista previa
                    previewControl.FindForm().Close();
                }
            }
        }
    }

    #region clases para prueba
    class Maestro 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }        

        public Maestro(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;            
        }
    }

    // Clase para representar el detalle
    class Detalle 
    {
        public int Id { get; set; }
        public int IdDetalle { get; set; }
        public string Descripcion { get; set; }

        public Detalle(int id, int iddetalle, string descripcion)
        {
            Id = id;
            IdDetalle = iddetalle;
            Descripcion = descripcion;
        }
    }

    #endregion

    public class DocumentoImpresion
    {
        public string DocumentoCadena { get; set; }
        public string DatosJson { get; set; }
    }

    public class TablasReporte
    {
        public string NombreTabla { get; set; }
        public object ListaTabla { get; set; }
    }

    
}

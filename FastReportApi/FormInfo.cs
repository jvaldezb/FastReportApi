using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastReportApi
{
    public partial class FormInfo : Form
    {
        public string Message { get => laMessage.Text; set => laMessage.Text = value; }
        public FormInfo()
        {
            InitializeComponent();
        }
    }
}

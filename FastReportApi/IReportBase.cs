using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastReportApi
{
    public enum NumTable
    {
        Unica,
        Multiple
    }

    public interface IReportBase
    {
        string JsonData { get; }
        string Impresora { get; }
        string ReportName { get; }
        string Formato { get; }
        NumTable NumTables { get; }
    }
}

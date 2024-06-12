using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastReportApi
{
    public static class EventoComun
    {        
        public static event Action<RemoteFrxData> OnPrintRemoteFrxData;
        public static event Action<LocalFrxData> OnPrintLocalFrxData;
        public static event Action<int> OnReportCommand;

        public static void RaisePrintRemoteFrxData(RemoteFrxData remoteFrxData)
        {
            OnPrintRemoteFrxData?.Invoke(remoteFrxData);
        }

        public static void RaisePrintLocalFrxData(LocalFrxData localFrxData)
        {
            OnPrintLocalFrxData?.Invoke(localFrxData);
        }

        public static void RaiseReportCommand(int command)
        {
            OnReportCommand?.Invoke(command);
        }
    }

    public class RemoteFrxData
    {
        public string FileName { get; set; }
        public string File64String { get; set; }        
        public string JsonData { get; set; }
    }

    public class LocalFrxData
    {             
        public string JsonData { get; set; }
    }
}

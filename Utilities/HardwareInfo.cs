using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class HardwareInfo
    {
        public static string GetSHA1(String texto)
        {            
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            Byte[] textOriginal = ASCIIEncoding.Default.GetBytes(texto);
            Byte[] hash = sha1.ComputeHash(textOriginal);
            StringBuilder cadena = new StringBuilder();
            foreach (byte i in hash)
            {
                cadena.AppendFormat("{0:x2}", i);
            }
            return cadena.ToString();
        }

        public static string GetSerialNumberMainboard()
        {
            string result = "";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
            foreach (ManagementObject getserial in MOS.Get())
            {
                result += getserial["SerialNumber"].ToString();
            }            

            return result;
        }

        public static string GetSerialNumberHardDisk()
        {
            string result = "";
            ArrayList hdCollection = new ArrayList();

            ManagementObjectSearcher searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                HardDrive hd = new HardDrive();
                hd.Model = wmi_HD["Model"].ToString();
                hd.Type = wmi_HD["InterfaceType"].ToString();

                hdCollection.Add(hd);
            }

            searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

            int i = 0;
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                // get the hard drive from collection
                // using index
                if (hdCollection.Count > i)
                {
                    HardDrive hd = (HardDrive)hdCollection[i];

                    // get the hardware serial no.
                    if (wmi_HD["SerialNumber"] == null)
                        hd.SerialNo = "None";
                    else
                        hd.SerialNo = wmi_HD["SerialNumber"].ToString();
                }

                ++i;
            }

            foreach (HardDrive hd in hdCollection)
            {
                result = hd.SerialNo.Trim();
            }
            return result;
        }

        public static string GetUniqueKeyLicense()
        {
            var nroSeriePlaca = GetSerialNumberMainboard();
            var nroSerieDisco = GetSerialNumberHardDisk();
            var clave1 = GetSHA1(nroSeriePlaca + nroSerieDisco);
            return clave1;
        }
    }
}

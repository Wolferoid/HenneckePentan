using HenneckePentan.Models;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenneckePentan.Services
{
    class ErrorDescription
    {
        public string GetText(ErrorCode errorCode)
        {
            string errorText = null;
            switch (errorCode)
            {
                case ErrorCode.NoError: errorText = "STATUS: No error."; break;
                case ErrorCode.WrongCPU_Type: errorText = "ERROR: Wrong CPU type."; break;
                case ErrorCode.ConnectionError: errorText = "ERROR: Connection error. Wrong Rack or Slot number."; break;
                case ErrorCode.IPAddressNotAvailable: errorText = "ERROR: IP Adress "+ ConnectionSettings.IpAddress + " not available."; break;
                case ErrorCode.WrongVarFormat: errorText = "ERROR: Wrong var format."; break;
                case ErrorCode.WrongNumberReceivedBytes: errorText = "ERROR: Wrong number recived bytes."; break;
                case ErrorCode.SendData: errorText = "ERROR: Send data."; break;
                case ErrorCode.ReadData: errorText = "ERROR: Read data."; break;
                case ErrorCode.WriteData: errorText = "ERROR: Write data."; break;
                default: break;
            }

            return errorText;
        }        
    }
}

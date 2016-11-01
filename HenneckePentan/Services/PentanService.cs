using HenneckePentan.Models;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenneckePentan.Services
{
    class PentanService
    {
        #region Fields
        Plc plc;
        ErrorCode connectionResult;
        string strStatus;
        bool isConnected;
        #endregion

        #region Properties
        public string Status
        {
            get { return strStatus; }
        }
        #endregion


        #region Constructor
        
        #endregion

        public string Connect()
        {
            ErrorDescription error = new ErrorDescription();
            string status = null;
            // Применить проверку типа try-catch ибо using не годиться
            // Connection to device
            plc = new Plc(CpuType.S7400,
                          ConnectionSettings.IpAddress,
                          Convert.ToInt16(ConnectionSettings.Rack),
                          Convert.ToInt16(ConnectionSettings.Slot));
            if (!plc.IsConnected)
            {
                // Ensure IP is responding
                if (plc.IsAvailable)
                {
                   status = error.GetText(plc.Open());
                }
                else
                {
                    //status = "ERROR: Device is not available. Check IP Address setting.";
                    status = error.GetText(plc.Open());
                }

                if (plc.IsConnected)
                {
                    isConnected = true;
                }
            }
            return status;
        }

        public void Disconnect()
        {
            if (plc != null && plc.IsConnected)
            {
                plc.Close();

                isConnected = false;

                strStatus = "STATUS: Connection is closed.";
            }
            else
            {
                strStatus = "STATUS: Disconnect button clicked, but something wrong.";
            }
        }

        #region Template
        public void WriteRecipe()
        { }

        public void ReadRecipe()
        { }

        public void WriteSettings()
        { }

        public void ReadSettings()
        { }

        public void PidEnable()
        { }
        public void PidDisable()
        { }
        #endregion
    }
}

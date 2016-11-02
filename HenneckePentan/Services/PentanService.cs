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

        public string Disconnect()
        {
            string status = null;
            if (plc != null && plc.IsConnected)
            {
                plc.Close();

                isConnected = false;

                status = "СТАТУС: Соединение разорвано.";
            }
            else
            {
                status = "СТАТУС: Ошибка разрыва соединения.";
            }
            return status;
        }

        #region Template
        public void WriteRecipe()
        { }

        public string ReadCurrent()
        {
            float value = 0.0f;
            if (plc != null && plc.IsConnected)
            {
                ErrorDescription error = new ErrorDescription();
                var obj = plc.Read("MD270");
                value = Convert.ToSingle(obj);
                
            }
            else
            {
                //status = "ОШИБКА: Соединение не установлено.";
            }
            return value.ToString();
        }

        public void WriteSettings()
        { }

        public void ReadSettings()
        { }

        public string PidEnable()
        {
            string status = null;
            if (plc != null && plc.IsConnected)
            {
                ErrorDescription error = new ErrorDescription();
                status  = error.GetText(plc.Write("M239.0", 1));
               
            }
            else
            {
                status = "ОШИБКА: Соединение не установлено.";
            }
            return status;
        }
        public string PidDisable()
        {
            string status = null;
            if (plc != null && plc.IsConnected)
            {
                ErrorDescription error = new ErrorDescription();
                status = error.GetText(plc.Write("M239.0", 0));
            }
            else
            {
                status = "ОШИБКА: Соединение не установлено.";
            }
            return status;
        }
        #endregion
    }
}

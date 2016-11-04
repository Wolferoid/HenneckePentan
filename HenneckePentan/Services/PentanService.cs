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
        #endregion

        #region Methods
        /// <summary>
        /// Connect to PLC
        /// </summary>
        /// <returns></returns>
        public string Connect()
        {
            string status = null;
            // Применить проверку типа try-catch ибо using не годиться
            // Connection to device
            plc = new Plc(ConnectionSettings.CpuType,
                          ConnectionSettings.IpAddress,
                          Convert.ToInt16(ConnectionSettings.Rack),
                          Convert.ToInt16(ConnectionSettings.Slot));
            if (!plc.IsConnected)
            {
                // Ensure IP is responding
                if (plc.IsAvailable)
                {
                    status = Status.Text(plc.Open());
                }
                else
                {
                    status = Status.Text(plc.Open());
                }
            }
            return status;
        }

        /// <summary>
        /// Disconnect from PLC
        /// </summary>
        /// <returns></returns>
        public string Disconnect()
        {
            string status = null;
            if (plc != null && plc.IsConnected)
            {
                plc.Close();
                status = Status.Text("СТАТУС: Соединение разорвано.");
            }
            else
            {
                status = Status.Text("СТАТУС: Ошибка разрыва соединения.");
            }
            return status;
        }

        /// <summary>
        /// Check conection status
        /// </summary>
        /// <returns></returns>
        public bool ConnectionStatus()
        {
            bool status = false;

            if (plc != null && plc.IsConnected)
                status = true;

            return status;
        }

        /// <summary>
        /// Enable PID function
        /// </summary>
        /// <returns></returns>
        public string PidEnable()
        {
            string status = null;
            if (plc != null && plc.IsConnected)
            {
                status = Status.Text(plc.Write("DB1000.DBX0.0", 1));

            }
            else
            {
                status = Status.Text("ОШИБКА: Соединение не установлено.");
            }
            return status;
        }

        /// <summary>
        /// Disable PID function
        /// </summary>
        /// <returns></returns>
        public string PidDisable()
        {
            string status = null;
            if (plc != null && plc.IsConnected)
            {
                status = Status.Text(plc.Write("DB1000.DBX0.0", 0));
            }
            else
            {
                status = Status.Text("ОШИБКА: Соединение не установлено.");
            }
            return status;
        }

        /// <summary>
        /// Gets Pid enable/disable status
        /// </summary>
        /// <returns></returns>
        public bool PidStatus()
        {
            bool status = false;

            if (plc != null && plc.IsConnected)
            {
                var obj = plc.Read("DB1000.DBX0.0");
                status = Convert.ToBoolean(obj);
            }
            return status;
        }

        /// <summary>
        /// Read current pentan rate
        /// </summary>
        /// <returns></returns>
        public string ReadCurrent()
        {
            double? result = null;
            if (plc != null && plc.IsConnected)
            {
                result = Math.Round(Convert.ToDouble(plc.Read(DataType.DataBlock, 1000, 6, VarType.Real, 1) as double?), 2);
            }
            else
            {
                //status = "ОШИБКА: Соединение не установлено.";
            }
            return string.Format("{0:0.00}", result);
        }

        /// <summary>
        /// Write recipe data
        /// </summary>
        /// <param name="sValue">Double in a string. Range from 0.0 to 15.0</param>
        /// <returns></returns>
        public string WriteRecipe(string sValue)
        {
            string status = null;
            double dValue;
            bool isParsed;

            try
            {
                //dValue = Convert.ToDouble(sValue);
                dValue = double.Parse(sValue);
                isParsed = true;
            }
            catch (FormatException)
            {
                status = Status.Text("ОШИБКА: Неверно значение рецепта.");
                isParsed = false;
                dValue = 0;
            }
            catch (ArgumentNullException)
            {
                status = Status.Text("ОШИБКА: Значение рецепта не задано.");
                isParsed = false;
                dValue = 0;
            }
            catch (Exception e)
            {
                status = Status.Text(string.Format("ОШИБКА: {0}", e.Message));
                isParsed = false;
                dValue = 0;
            }

            if (isParsed)
            {

                if (plc != null && plc.IsConnected)
                {
                    if (0.0 <= dValue && dValue <= 15.0)
                    {
                        status = Status.Text(plc.Write(DataType.DataBlock, 1000, 2, Math.Round(dValue, 2)));
                    }
                    else
                    {
                        status = Status.Text("ОШИБКА: Допустимый диапазон значений от 0.0 до 15.0 ");
                    }
                }
                else
                {
                    status = Status.Text("ОШИБКА: Соединение не установлено.");
                }

            }
            return status;
        }

        public string ReadPumpRate()
        {
            double? result = null;
            if (plc != null && plc.IsConnected)
            {
                result = Math.Round((Convert.ToDouble(plc.Read(DataType.DataBlock, 1000, 10, VarType.Real, 1) as double?) / 100), 2);
            }
            else
            {
                //status = "ОШИБКА: Соединение не установлено.";
            }
            return string.Format("{0:0.00}", result);
        }
        #endregion


        #region Template
        public void WriteSettings()
        { }

        public void ReadSettings()
        { }



        #endregion
    }
}

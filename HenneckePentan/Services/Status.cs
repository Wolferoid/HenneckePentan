using HenneckePentan.Models;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenneckePentan.Services
{
    public static class Status
    {
        public static string Text(ErrorCode errorCode)
        {
            string status = null;
            switch (errorCode)
            {
                case ErrorCode.NoError: status = "СТАТУС: Операция выполнена успешно."; break;
                case ErrorCode.WrongCPU_Type: status = "ОШИБКА: Неправильный тип ЦПУ."; break;
                case ErrorCode.ConnectionError: status = "ОШИБКА: Ошибка соединения. Задан неправильный номер Rack или Slot."; break;
                case ErrorCode.IPAddressNotAvailable: status = "ОШИБКА: IP адрес " + ConnectionSettings.IpAddress + " не доступен."; break;
                case ErrorCode.WrongVarFormat: status = "ОШИБКА: Неправильный формат данных."; break;
                case ErrorCode.WrongNumberReceivedBytes: status = "ОШИБКА: Неверное количество принятых байт."; break;
                case ErrorCode.SendData: status = "ОШИБКА: Отправки данных."; break;
                case ErrorCode.ReadData: status = "ОШИБКА: Чтения данных."; break;
                case ErrorCode.WriteData: status = "ОШИБКА: Записи данных."; break;
                default: break;
            }

            return String.Format("[{0}] {1}", DateTime.Now.ToLongTimeString(), status);
        }
        public static string Text(string str)
        {
            return String.Format("[{0}] {1}", DateTime.Now.ToLongTimeString(), str);
        }
    }
}

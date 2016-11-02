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
                case ErrorCode.NoError: errorText = "СТАТУС: Операция выполнена успешно."; break;
                case ErrorCode.WrongCPU_Type: errorText = "ОШИБКА: Неправильный тип ЦПУ."; break;
                case ErrorCode.ConnectionError: errorText = "ОШИБКА: Ошибка соединения. Задан неправильный номер Rack или Slot."; break;
                case ErrorCode.IPAddressNotAvailable: errorText = "ОШИБКА: IP адрес "+ ConnectionSettings.IpAddress + " не доступен."; break;
                case ErrorCode.WrongVarFormat: errorText = "ОШИБКА: Неправильный формат данных."; break;
                case ErrorCode.WrongNumberReceivedBytes: errorText = "ОШИБКА: Неверное количество принятых байт."; break;
                case ErrorCode.SendData: errorText = "ОШИБКА: Отправки данных."; break;
                case ErrorCode.ReadData: errorText = "ОШИБКА: Чтения данных."; break;
                case ErrorCode.WriteData: errorText = "ОШИБКА: Записи данных."; break;
                default: break;
            }

            return errorText;
        }        
    }
}

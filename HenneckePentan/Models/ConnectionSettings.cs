using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenneckePentan.Models
{
    static class ConnectionSettings
    {
#if DEBUG
        public static string IpAddress = "192.168.0.100";
        public static string Rack = "0";
        public static string Slot = "3";
        public static CpuType CpuType = CpuType.S7400;
#else
        public static string IpAddress = "172.17.16.10";
        public static string Rack = "0";
        public static string Slot = "2";
        public static CpuType CpuType = CpuType.S7300;
#endif
    }
}

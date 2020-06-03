using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictCapacityUsingMathNet
{
    class SoLieuDuDoan
    {
        public bool success { get; set; }
        public List<DataDuDoan> data { get; set; }
        public string message { get; set; }
    }

    public class DataDuDoan
    {
        public string date { get; set; }
        public double ghi { get; set; }
        public double ghi_90 { get; set; }
        public double ghi_10 { get; set; }
        public double ebh { get; set; }
        public double dni_10 { get; set; }
        public double dni { get; set; }
        public double dni_90 { get; set; }
        public double dhi { get; set; }
        public double zenith { get; set; }
        public double azimuth { get; set; }
        public double cloud_opacity { get; set; }
        public double air_temperature { get; set; }

        public string ToString(double capacity)
        {
            return "Time : " + date + " , ghi : " + ghi + " , envtemp : " + air_temperature + " , Du Doan :" + capacity;
        }
    }
}

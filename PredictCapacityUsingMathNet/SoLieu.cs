using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictCapacityUsingMathNet
{
    public class SoLieu
    {
        public bool success { get; set; }
        public List<Data> data { get; set; }
        public string message { get; set; }

    }

    public class Data
    {
        public string time { get; set; }
        public double capacity { get; set; }
        public double ghi { get; set; }
        public double paneltemp { get; set; }
        public double envtemp { get; set; }
        public Data() { }
        public Data(string ti, double cap, double gh, double temp)
        {
            time = ti;
            capacity = cap;
            ghi = gh;
            envtemp = temp;
        }
        public override string ToString()
        {
            return "time : " + time + ", capacity : " + capacity + " , ghi : " + ghi + " , envtemp : " + envtemp;
        }

        /*public double CapacityDuDoan(MaTran rs)
        {
            double dudoan = MaTran.TichVoHuong2Vector(new double[3] { 1, ghi, envtemp }, rs.GetCol(0), 3);
            Console.WriteLine("Du doan ( Time : " + time + " ,     ghi = " + ghi + " , temp = " + envtemp + " ): " + dudoan + "\nThuc Te : " + capacity);
            return dudoan;
        }*/
    }
}

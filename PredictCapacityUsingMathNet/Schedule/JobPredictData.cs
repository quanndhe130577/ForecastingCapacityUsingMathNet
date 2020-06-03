using MathNet.Numerics.LinearAlgebra.Double;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static PredictCapacityUsingMathNet.SoLieuDAO;

namespace PredictCapacityUsingMathNet.Schedule
{
    class JobPredictData : IJob
    {
        List<Data> list = new List<Data>();
        double capacity_MAX;
        public Task Execute(IJobExecutionContext context)
        {
            PredictData();
            throw new NotImplementedException();
        }

        public bool GetData()
        {
            try
            {
                capacity_MAX = 0;
                DataTable dt = SoLieuDAO.GetSoLieu(DateTime.Now.AddDays(-1), DateTime.Now);
                if (dt.Rows.Count == 0)
                {
                    Console.WriteLine("No data in DB !!!");
                    return false;
                }
                foreach (DataRow rd in dt.Rows)
                {
                    double cap = double.Parse(rd["Capacity"].ToString());
                    list.Add(new Data(rd["Time"].ToString(), cap,
                        double.Parse(rd["GHI"].ToString()), double.Parse(rd["Temperature"].ToString())
                        ));
                    if (capacity_MAX < cap)
                    {
                        capacity_MAX = cap;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Get Data From DB : " + e.Message);
                return false;
            }
            return true;
        }

        public void PredictData()
        {
            if (!GetData())
            {
                return;
            }
            int number = list.Count;
            int numberTraining = number / 10 * 8;
            int numberTest = number - numberTraining;
            // convert History Data to Matrix
            double[,] X_Data = new double[numberTraining, 3];
            for (int i = 0; i < numberTraining; i++)
            {
                X_Data[i, 0] = 1;
                X_Data[i, 1] = list[i].ghi;
                X_Data[i, 2] = list[i].envtemp;
            }
            double[,] X_Test = new double[numberTest, 3];
            for (int i = 0; i < numberTest; i++)
            {
                X_Test[i, 0] = 1;
                X_Test[i, 1] = list[i + numberTraining].ghi;
                X_Test[i, 2] = list[i + numberTraining].envtemp;
            }


            // setup Matran Y
            double[,] Y_Data = new double[numberTraining, 1];
            for (int i = 0; i < numberTraining; i++)
            {
                Y_Data[i, 0] = list[i].capacity;
            }

            var X = DenseMatrix.OfArray(X_Data);
            var Y = DenseMatrix.OfArray(Y_Data);
            // solve
            var qrTheta = X.QR().Solve(Y).AsColumnMajorArray();

            // calculate Forecasting Error
            double saisoMAPE = 0;

            //Console.WriteLine("Test Sai So : ");
            for (int i = 0; i < X_Test.GetLength(0); i++)
            {
                double duDoan = YPredict(GetRowFrom2DArray(X_Test, i), qrTheta);
                double thucte = list[i + numberTraining].capacity;
                //  Console.WriteLine("GHI : " + X_Test[i, 1] + " , envTemp = " + X_Test[i, 2] + "\nDu Doan : " + duDoan + "\nThuc Te :" + thucte);
                saisoMAPE += Math.Abs((duDoan - thucte) / capacity_MAX);
            }
            Console.WriteLine("Sai So : " + saisoMAPE * 100 / number + " % ");
            Console.WriteLine("Cong Suat Thiet Ke : " + capacity_MAX);

            Console.WriteLine("-----------------------");
            Console.WriteLine("Du Doan Capacity : " + " " + DateTime.Now);


            DateTime start = DateTime.Now;
            // get Predict data
            try
            {
                HttpWebRequest WebReq_DuDoan = (HttpWebRequest)WebRequest.Create
            (string.Format("https://www.nldc.evn.vn/Renewable/Forecast/GetThoiTietNhaMay?start=" + start.AddDays(-1).ToString("yyyyMMddHHmmss") + "&end=" + start.AddHours(3).ToString("yyyyMMddHHmmss") + "&idNhaMay=362"));

                WebReq_DuDoan.Method = "GET";
                HttpWebResponse WebResp_DuDoan = (HttpWebResponse)WebReq_DuDoan.GetResponse();

                Stream json_DuDoan = WebResp_DuDoan.GetResponseStream();
                StreamReader json_str_db = new StreamReader(json_DuDoan);
                string str_dudoan = json_str_db.ReadToEnd();

                JavaScriptSerializer jss_dudoan = new JavaScriptSerializer();
                SoLieuDuDoan obj_dudoan = jss_dudoan.Deserialize<SoLieuDuDoan>(str_dudoan);

                // convert Predict Data to Matrix
                double[,] X_DuDoan = new double[obj_dudoan.data.Count, 3];
                for (int i = 0; i < obj_dudoan.data.Count; i++)
                {
                    X_DuDoan[i, 0] = 1;
                    X_DuDoan[i, 1] = obj_dudoan.data[i].ghi;
                    X_DuDoan[i, 2] = obj_dudoan.data[i].air_temperature;
                }
                //insert DuLieuDuBao

                for (int i = 0; i < X_DuDoan.GetLength(0); i++)
                {
                    double dudoan = YPredict(GetRowFrom2DArray(X_DuDoan, i), qrTheta);
                    DataTable dt = SoLieuDuDoanDAO.GetDuLieuDuDoanByTime(DateTime.Parse(obj_dudoan.data[i].date));
                    if (dt.Rows.Count == 0)
                    {
                        SoLieuDuDoanDAO.InsertHistorialData(obj_dudoan.data[i], dudoan);
                        Console.WriteLine("Insert Du Doan: " + obj_dudoan.data[i].ToString(dudoan));
                    }
                    else
                    {
                        SoLieuDuDoanDAO.UpdateHistorialData(obj_dudoan.data[i], dudoan);
                        Console.WriteLine("Update Du Doan: " + obj_dudoan.data[i].ToString(dudoan));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Predict Data : " + e.Message);
            }
        }

        private double YPredict(double[] xPlot, double[] theta)
        {
            double rs = 0;
            for (int i = 0; i < xPlot.Length; i++)
            {
                rs += xPlot[i] * theta[i];
            }
            return rs;
        }

        private double[] GetRowFrom2DArray(double[,] array, int numberRow)
        {
            double[] rs = new double[array.GetLength(1)];
            for (int i = 0; i < array.GetLength(1); i++)
            {
                rs[i] = array[numberRow, i];
            }
            return rs;
        }
    }
}

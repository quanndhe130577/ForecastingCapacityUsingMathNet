
using Quartz;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PredictCapacityUsingMathNet.Schedule
{
    class JobGetData : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            GetData();
            throw new NotImplementedException();
        }
        public void GetData()
        {
            try
            {
                DateTime start = DateTime.Now;
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create
            (string.Format("https://www.nldc.evn.vn/Renewable/Scada/GetScadaNhaMay?start=" + start.AddDays(-1).ToString("yyyyMMddHHmmss") + "&end=" + start.ToString("yyyyMMddHHmmss") + "&idNhaMay=362"));

                WebReq.Method = "GET";
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                Stream json = WebResp.GetResponseStream();
                StreamReader json_str = new StreamReader(json);
                string str = json_str.ReadToEnd();

                JavaScriptSerializer jss = new JavaScriptSerializer();
                SoLieu obj = jss.Deserialize<SoLieu>(str);
                Console.WriteLine("sucess : " + obj.success + " " + DateTime.Now);

                int number = obj.data.Count;

                //set capacity_Max
                double capacity_MAX = obj.data[0].capacity / 0.9;

                DateTime TimeMax = SoLieuDAO.GetTimeMax();
                for (int i = 0; i < obj.data.Count; i++)
                {

                    if (capacity_MAX < obj.data[i].capacity / 0.9)
                    {
                        capacity_MAX = obj.data[i].capacity / 0.9;
                    }
                    // Console.WriteLine(obj.data[i]);
                    DataTable dt = SoLieuDAO.GetDuLieuByTime(DateTime.Parse(obj.data[i].time));
                    if (dt.Rows.Count == 0)
                    {
                        SoLieuDAO.InsertHistorialData(obj.data[i]);
                         Console.WriteLine("Insert Du Lieu : " + obj.data[i].ToString());
                    }
                }

                Console.WriteLine("message : " + obj.message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Get Data : " + e.Message);
            }
        }
    }
}

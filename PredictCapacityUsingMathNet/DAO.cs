using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictCapacityUsingMathNet
{
    class DAO
    {
        public static SqlConnection GetConnection()
        {
            //string ConnectionString = @"server=localhost; database=Northwind; Integrated Security=SSPI;";
            string ConnectionString = ConfigurationManager.ConnectionStrings["ForecastingCapacity"].ConnectionString;
            return new SqlConnection(ConnectionString);
        }

        //user SqlDataReader
        public static DataTable GetDataBySQL(string sql)
        {
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            DataTable dt = new DataTable();
            Command.Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            dt.Load(reader);
            Command.Connection.Close();
            return dt;
        }

        public static DataTable GetDataBySQLWithParameters(string sql, params SqlParameter[] parameters)
        {
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            Command.Parameters.AddRange(parameters);
            DataTable dt = new DataTable();
            Command.Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            dt.Load(reader);
            Command.Connection.Close();
            return dt;
        }

        public static int UpdateDataBySQL(string sql)
        {
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            DataTable dt = new DataTable();
            Command.Connection.Open();
            int number = Command.ExecuteNonQuery();
            Command.Connection.Close();
            return number;
        }

        public static int UpdateDataBySQLWithParametes(string sql, params SqlParameter[] parameters)
        {
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            Command.Parameters.AddRange(parameters);
            DataTable dt = new DataTable();
            Command.Connection.Open();
            int number = Command.ExecuteNonQuery();
            Command.Connection.Close();
            return number;
        }
    }

    public class SoLieuDAO
    {
        public static DateTime GetTimeMax()
        {
            string sql = "SELECT MAX(time) as Time FROM DuLieuLichSu";
            DataTable dt = DAO.GetDataBySQL(sql);
            foreach (DataRow dr in dt.Rows)
            {
                return DateTime.Parse((dr["Time"].ToString()).Equals("") ? "01/01/0001" : (dr["Time"].ToString()));
            }
            return DateTime.Parse("01/01/0001");
        }
        public static DataTable GetDuLieuByTime(DateTime dt)
        {
            string sql = "SELECT * FROM DuLieuLichSu WHERE time = @dt";
            SqlParameter param1 = new SqlParameter("@dt", SqlDbType.DateTime);
            param1.Value = dt;
            return DAO.GetDataBySQLWithParameters(sql, param1);
        }
        public static int InsertHistorialData(Data sl)
        {
            string sql = "INSERT INTO DuLieuLichSu (time , capacity, ghi, envtemp) " +
                "VALUES ( @time, @cap, @ghi, @evn)";
            SqlParameter param1 = new SqlParameter("@time", SqlDbType.DateTime);
            param1.Value = sl.time;
            SqlParameter param2 = new SqlParameter("@cap", SqlDbType.Float);
            param2.Value = sl.capacity;
            SqlParameter param3 = new SqlParameter("@ghi", SqlDbType.Float);
            param3.Value = sl.ghi;
            SqlParameter param4 = new SqlParameter("@evn", SqlDbType.Float);
            param4.Value = sl.envtemp;

            return DAO.UpdateDataBySQLWithParametes(sql, param1, param2, param3, param4);
        }

        public static DataTable GetSoLieu(DateTime dtFrom, DateTime dtTo)
        {
            string sql = "SELECT time as Time, ghi as GHI, envtemp as Temperature, capacity as Capacity FROM DuLieuLichSu ";
            sql += " WHERE Time between @tf and @tt ORDER BY Time asc";
            SqlParameter param1 = new SqlParameter("@tf", SqlDbType.DateTime);
            param1.Value = dtFrom;
            SqlParameter param2 = new SqlParameter("@tt", SqlDbType.DateTime);
            param2.Value = dtTo;
            return DAO.GetDataBySQLWithParameters(sql, param1, param2);
        }
        public class SoLieuDuDoanDAO
        {
            public static DateTime GetTimeMax()
            {
                string sql = "SELECT MAX( time_db ) as Time FROM DuLieuDuDoan";
                DataTable dt = DAO.GetDataBySQL(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    return DateTime.Parse((dr["Time"].ToString()).Equals("") ? "01/01/0001" : (dr["Time"].ToString()));
                }
                return DateTime.Parse("01/01/0001");
            }
            public static int InsertHistorialData(DataDuDoan sl, double capacity)
            {
                string sql = "INSERT INTO DuLieuDuDoan ( time_db , capacity_db, ghi_db , envtemp_db ) " +
                    "VALUES ( @time, @cap, @ghi, @evn )";
                SqlParameter param1 = new SqlParameter("@time", SqlDbType.DateTime);
                param1.Value = sl.date;
                SqlParameter param2 = new SqlParameter("@cap", SqlDbType.Float);
                param2.Value = capacity;
                SqlParameter param3 = new SqlParameter("@ghi", SqlDbType.Float);
                param3.Value = sl.ghi;
                SqlParameter param4 = new SqlParameter("@evn", SqlDbType.Float);
                param4.Value = sl.air_temperature;

                return DAO.UpdateDataBySQLWithParametes(sql, param1, param2, param3, param4);
            }

            public static int UpdateHistorialData(DataDuDoan sl, double capacity)
            {
                string sql = "UPDATE DuLieuDuDoan SET capacity_db = @cap, ghi_db = @ghi , envtemp_db = @evn " +
                    " WHERE time_db = @time ";
                SqlParameter param1 = new SqlParameter("@time", SqlDbType.DateTime);
                param1.Value = sl.date;
                SqlParameter param2 = new SqlParameter("@cap", SqlDbType.Float);
                param2.Value = capacity;
                SqlParameter param3 = new SqlParameter("@ghi", SqlDbType.Float);
                param3.Value = sl.ghi;
                SqlParameter param4 = new SqlParameter("@evn", SqlDbType.Float);
                param4.Value = sl.air_temperature;

                return DAO.UpdateDataBySQLWithParametes(sql, param1, param2, param3, param4);
            }

            public static DataTable GetDuLieuDuDoanByTime(DateTime dt)
            {
                string sql = "SELECT * FROM DuLieuDuDoan WHERE time_db = @dt";
                SqlParameter param1 = new SqlParameter("@dt", SqlDbType.DateTime);
                param1.Value = dt;
                Console.WriteLine(dt);
                return DAO.GetDataBySQLWithParameters(sql, param1);
            }
        }
    }
}

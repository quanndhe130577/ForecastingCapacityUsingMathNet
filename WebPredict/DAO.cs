using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPredict
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
}

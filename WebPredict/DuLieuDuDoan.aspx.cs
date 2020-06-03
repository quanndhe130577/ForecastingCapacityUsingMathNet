using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPredict
{
    public partial class DuLieuDuDoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDuBao(DateTime.Now, DateTime.Now.AddHours(3));
            }
        }
        public void LoadDuBao(DateTime dtFrom, DateTime dtTo)
        {
            string sql = "SELECT time_db as Time, ghi_db as GHI, envtemp_db as Temperature, capacity_db as Capacity FROM DuLieuDuDoan ";
            if (dtFrom != null && dtTo != null)
            {
                sql += " WHERE time_db between @tf and @tt ORDER BY Time desc";
                SqlParameter param1 = new SqlParameter("@tf", SqlDbType.DateTime);
                param1.Value = dtFrom;
                SqlParameter param2 = new SqlParameter("@tt", SqlDbType.DateTime);
                param2.Value = dtTo;
                DataTable dt = DAO.GetDataBySQLWithParameters(sql, param1, param2);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                sql += " ORDER BY Time desc";
                DataTable dt = DAO.GetDataBySQL(sql);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }
    }
}
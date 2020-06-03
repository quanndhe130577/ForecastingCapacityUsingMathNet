using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPredict
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // GridView1.AllowPaging = true;
               // GridView2.AllowPaging = true;
                LoadLichSu(DateTime.Now.AddDays(-1), DateTime.Now);
               
            }
        }

        public void LoadLichSu(DateTime dtFrom, DateTime dtTo)
        {
            string sql = "SELECT time as Time, ghi as GHI, envtemp as Temperature, capacity as Capacity FROM DuLieuLichSu ";
            if(dtFrom != null && dtTo != null)
            {
                sql += " WHERE Time between @tf and @tt ORDER BY Time desc";
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
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVC_EntityFramework_Code_First.Common;
using Entity.Models;
using Service;

namespace MVC_EntityFramework_Code_First.Controllers
{
    //customize AuthorizeAttribute
    [CheckRoleAttribute(Role = "Admin")]
    public class DuLieuDuDoanController : Controller
    {
        private DuLieuDbContext db = new DuLieuDbContext();
        // GET: DuLieuDuDoan
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            // du lieu du bao
            DateTime dt = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second);
            var tempdb1 = DuLieuDuDoanDAO.GetDataFollowTime(dt);
            return Json(tempdb1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDataSoSanh()
        {
            // Du lieu so sanh 
            try
            {
                DateTime dtls = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second);
                return Json(DuLieuDuDoanDAO.GetDataSoSanhFollowTime(dtls), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null);
            }

        }
    }
}

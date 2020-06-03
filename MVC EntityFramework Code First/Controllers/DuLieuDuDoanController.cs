using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVC_EntityFramework_Code_First.Common;
using Entity.Models;

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
            DateTime dt = DateTime.Now;
            var temp = (from DuLieuDuDoan in db.DuDoanList 
                        where DuLieuDuDoan.time_db > dt
                        orderby DuLieuDuDoan.time_db descending 
                        select DuLieuDuDoan).ToList();
            return View(temp);
        }
        public JsonResult GetData()
        {  
            // du lieu du bao
            DateTime dt = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second);
            var tempdb = (from DuLieuDuDoan in db.DuDoanList
                          where DuLieuDuDoan.time_db >= dt
                          orderby DuLieuDuDoan.time_db ascending
                          select new { Date = DuLieuDuDoan.time_db, Capacity = DuLieuDuDoan.capacity_db }).ToList();
            var tempdb1 = tempdb.Select( s => new { Date = s.Date.ToString("yyyy-MM-ddTHH:mm:ss"), Capacity = s.Capacity }).ToList();
            return Json(tempdb1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDataSoSanh()
        {
            // Du lieu so sanh 
            try
            {
                DateTime dtls = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second);
                 var dataTb1 = db.LichSuList.AsNoTracking().ToList();
                 var dataTb2 = db.DuDoanList.AsNoTracking().ToList();
                var tempss = (from DuLieuDuDoan in dataTb2
                              join DuLieuLichSu in dataTb1 on DuLieuDuDoan.time_db equals DuLieuLichSu.time into sosanh
                              from DuLieu in sosanh.DefaultIfEmpty()
                              where DuLieuDuDoan.time_db >= dtls 
                              orderby DuLieuDuDoan.time_db ascending
                              select new { Date = DuLieuDuDoan.time_db, CapacityTT = ( DuLieu == null ? 191100 : DuLieu.capacity), CapacityDB = DuLieuDuDoan.capacity_db ,
                              GhiTT = (DuLieu == null ? 191100 : DuLieu.ghi) , GhiDB = DuLieuDuDoan.ghi_db
                              }).ToList();
                //var temp = tempss.Where(s => s.Date >= dtls);
                var tempdb1 = tempss.Select(s => new { Date = s.Date.ToString("yyyy-MM-ddTHH:mm:ss"), CapacityTT = s.CapacityTT, CapacityDB = (s.CapacityDB < 0 ? 0 : s.CapacityDB),
                    GhiTT = s.GhiTT, GhiDB = s.GhiDB
                }).ToList();
                return Json(tempdb1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null);
            }
            
        }
        // GET: DuLieuDuDoan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuLieuDuDoan duLieuDuDoan = db.DuDoanList.Find(id);
            if (duLieuDuDoan == null)
            {
                return HttpNotFound();
            }
            return View(duLieuDuDoan);
        }

        // GET: DuLieuDuDoan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DuLieuDuDoan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,capacity_db,ghi_db,envtemp_db,time_db")] DuLieuDuDoan duLieuDuDoan)
        {
            if (ModelState.IsValid)
            {
                db.DuDoanList.Add(duLieuDuDoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(duLieuDuDoan);
        }

        // GET: DuLieuDuDoan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuLieuDuDoan duLieuDuDoan = db.DuDoanList.Find(id);
            if (duLieuDuDoan == null)
            {
                return HttpNotFound();
            }
            return View(duLieuDuDoan);
        }

        // POST: DuLieuDuDoan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,capacity_db,ghi_db,envtemp_db,time_db")] DuLieuDuDoan duLieuDuDoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(duLieuDuDoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(duLieuDuDoan);
        }

        // GET: DuLieuDuDoan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuLieuDuDoan duLieuDuDoan = db.DuDoanList.Find(id);
            if (duLieuDuDoan == null)
            {
                return HttpNotFound();
            }
            return View(duLieuDuDoan);
        }

        // POST: DuLieuDuDoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DuLieuDuDoan duLieuDuDoan = db.DuDoanList.Find(id);
            db.DuDoanList.Remove(duLieuDuDoan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

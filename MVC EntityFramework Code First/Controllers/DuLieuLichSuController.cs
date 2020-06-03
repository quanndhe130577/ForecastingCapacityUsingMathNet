using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Entity.Models;

namespace MVC_EntityFramework_Code_First.Controllers
{
    public class DuLieuLichSuController : Controller
    {
        private DuLieuDbContext db = new DuLieuDbContext();

        // GET: DuLieuLichSu
        public ActionResult Index()
        {
            /*DateTime dt = DateTime.Now.AddDays(-1);
            var temp = (from DuLieuLichSu in db.LichSuList 
                        where DuLieuLichSu.time > dt
                        orderby DuLieuLichSu.time descending 
                        select DuLieuLichSu).ToList();*/
            return View();
        }
        public JsonResult GetYesterdayCapacity()
        {
            DateTime dtFrom = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second).AddDays(-1);
            DateTime dtTo = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second);
            var temp = (from DuLieuLichSu in db.LichSuList 
                        where DuLieuLichSu.time >= dtFrom && DuLieuLichSu.time < dtTo
                        orderby DuLieuLichSu.time ascending 
                        select new { Date = DuLieuLichSu.time , Capacity = DuLieuLichSu.capacity}).ToList();
            var tempdb1 = temp.Select(s => new { Date = s.Date.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss"), Capacity = s.Capacity }).ToList();
            return Json(tempdb1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData()
        {
            DateTime dtFrom = DateTime.Now.AddDays(-1);
            var temp = (from DuLieuLichSu in db.LichSuList
                        where DuLieuLichSu.time > dtFrom
                        orderby DuLieuLichSu.time ascending
                        select new { DuLieuLichSu.time, DuLieuLichSu.capacity }).ToList();
            //var tempdb1 = temp.Select(s => new { Date = ((s.time.Minute % 5 == 0) ? s.time.ToString("yyyy-MM-ddTHH:mm:ss") : null), Capacity = s.capacity }).ToList();
            var tempdb1 = temp.Select(s => new { Date = s.time.ToString("yyyy-MM-ddTHH:mm:ss"), Capacity = s.capacity }).ToList();
            return Json(tempdb1, JsonRequestBehavior.AllowGet);
        }
        private class ListData
        {
            public List<String> listTime;
            public List<Double> listCap;
        }
        // GET: DuLieuLichSu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuLieuLichSu duLieuLichSu = db.LichSuList.Find(id);
            if (duLieuLichSu == null)
            {
                return HttpNotFound();
            }
            return View(duLieuLichSu);
        }

        // GET: DuLieuLichSu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DuLieuLichSu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,capacity,ghi,envtemp,time")] DuLieuLichSu duLieuLichSu)
        {
            if (ModelState.IsValid)
            {
                db.LichSuList.Add(duLieuLichSu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(duLieuLichSu);
        }

        // GET: DuLieuLichSu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuLieuLichSu duLieuLichSu = db.LichSuList.Find(id);
            if (duLieuLichSu == null)
            {
                return HttpNotFound();
            }
            return View(duLieuLichSu);
        }

        // POST: DuLieuLichSu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,capacity,ghi,envtemp,time")] DuLieuLichSu duLieuLichSu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(duLieuLichSu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(duLieuLichSu);
        }

        // GET: DuLieuLichSu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuLieuLichSu duLieuLichSu = db.LichSuList.Find(id);
            if (duLieuLichSu == null)
            {
                return HttpNotFound();
            }
            return View(duLieuLichSu);
        }

        // POST: DuLieuLichSu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DuLieuLichSu duLieuLichSu = db.LichSuList.Find(id);
            db.LichSuList.Remove(duLieuLichSu);
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

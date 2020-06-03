using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Entity.Models;
using Service;

namespace MVC_EntityFramework_Code_First.Controllers
{
    [AuthorizeAttribute]
    public class AccountController : Controller
    {
        private DuLieuDbContext db = new DuLieuDbContext();

        [AllowAnonymous]
        // GET: Login
        public ActionResult Index()
        {
            return View(db.AccountList.ToList());
        }

        [AllowAnonymous]
        public JsonResult CheckLogin(string username, string password)
        {
            try
            {
                var rs = AccountDAO.CheckLogin(username, password);

                if (rs != null)
                {
                    Session["account"] = rs;
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return Json("Fail", JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult AddUser([Bind(Include = "Fullname, Lastname, Username, Password, Phone, Address, IdentifyCode,Role")] Account login)
        {
            AccountDAO.AddAccount(login);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public JsonResult CheckUsername(string username)
        {

            if (AccountDAO.CheckUsername(username))
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session.Remove("account");
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult ToSignUp()
        {
            return View(db.AccountList.ToList());
        }
        // GET: Login/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account login = db.AccountList.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password,Fullname,Lastname,Phone,Address,IdentifyCode")] Account login)
        {
            if (ModelState.IsValid)
            {
                db.AccountList.Add(login);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(login);
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account login = db.AccountList.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Fullname,Lastname,Phone,Address,IdentifyCode")] Account login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login);
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account login = db.AccountList.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account login = db.AccountList.Find(id);
            db.AccountList.Remove(login);
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

*/
    }
}

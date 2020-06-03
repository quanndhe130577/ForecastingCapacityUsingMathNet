using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_EntityFramework_Code_First.Common
{
    public class CheckRoleAttribute : AuthorizeAttribute
    {
        public string Role { get; set; }

        // call hàm này để check xem có được phép truy cập
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var account = (Account)HttpContext.Current.Session["account"];
            if (account == null) return false;
            if(account.Role != Role)
            {
                return false;
            }
            return true;
        }

        //nếu không được phép truy cập sẽ call hàm này
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/RoleDenied.cshtml"
            };
        }
    }
}
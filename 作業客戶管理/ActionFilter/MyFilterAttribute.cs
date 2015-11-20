using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 作業客戶管理.ActionFilter
{
    public class MyFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.TempData["st"] = DateTime.Now.Ticks;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var spendTime = DateTime.Now.Ticks - (long)filterContext.Controller.TempData["st"];
            Debug.WriteLine("Action執行花費時間：" +spendTime);
            base.OnActionExecuted(filterContext);
        }
    }
}
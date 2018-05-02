using Entity.ViewModel;
using Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Controllers
{
    public class BaseController : Controller
    {
        public CacheRuntime Cache { 
             get { 
                 CacheRuntime cache=new CacheRuntime(); 
                 cache.ExpiredTimespan=30;//设置30分钟过期
                 return cache;
             } 
        }

        public string UKey {
            get {
                string ukey = "";
                if (!string.IsNullOrEmpty(Request["ukey"] ?? ""))
                {
                    ukey = Request["ukey"];//如果表单内有值就从表单内获取
                }
                else
                {
                    ukey = Request.Cookies["ukey"]!=null?Request.Cookies["ukey"].Value:"";//表单内没有就从cookie中获取
                }

                return ukey;
            }
        }

        protected UserEntity CurrentUser {
            get {
                UserEntity user = Cache.Get<UserEntity>(UKey);
                if (user == null)
                {
                    //Response.Redirect("/", true);
                }

                return user;
            }
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.UKey = UKey;
            if (CurrentUser != null)
            {
                ViewBag.CommonMenus = CurrentUser.Menus;
            }
            //获取菜单信息
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
 
        }


    }
}

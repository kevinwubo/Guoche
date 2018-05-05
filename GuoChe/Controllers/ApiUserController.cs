using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity.ViewModel;
using Common;

namespace GuoChe.Controllers
{
    public class ApiUserController : Controller
    {
        //
        // GET: /ApiUser/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult Login(string username, string password)
        {
            ApiUserEntity viewE = new ApiUserEntity();
            viewE.result = true;
            viewE.message = "登录成功！";
            viewE.token = Guid.NewGuid().ToString();
            return Json(JsonHelper.ToJson(viewE));
        }


        /// <summary>
        /// 手机注册返回验证码
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        public JsonResult RegisterVCode(string telephone)
        {
            ApiUserEntity viewE = new ApiUserEntity();
            viewE.result = true;
            viewE.message = "验证码返回成功！";
            viewE.vcode = "898929";
            return Json(JsonHelper.ToJson(viewE));
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="vcode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult Register(string telephone, string vcode, string password)
        {
            return Json("");
        }

        /// <summary>
        /// 忘记密码  直接更新数据库中密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="telephone"></param>
        /// <param name="vcode"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        public JsonResult Forget(string userid, string telephone, string vcode, string newpassword)
        {
            return Json("");
        }
    }
}

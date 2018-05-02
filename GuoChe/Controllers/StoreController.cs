using Common;
using Infrastructure.Helper;
using Entity.ViewModel;
using Infrastructure.Cache;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Controllers
{
    public class StoreController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string sid)
        {
            ViewBag.Province = BaseDataService.GetAllProvince();
            return View();
        }

        public JsonResult GetCity(int pid)
        {
            List<City> listCity=BaseDataService.GetAllCity();
            if (!listCity.IsEmpty())
            {
                listCity = listCity.Where(t => t.ProvinceID == pid).ToList();
            }

            return Json(listCity);
        }


    }
}

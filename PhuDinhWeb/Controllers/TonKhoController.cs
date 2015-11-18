using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PhuDinhData;
using PhuDinhData.Filter;

namespace PhuDinhWeb.Controllers
{
    [Authorize]
    public class TonKhoController : Controller
    {
        public ActionResult Index()
        {
            TonKhoManager.UpdateTonKho();

            var now = DateTime.Now.Date;

            var filter = new Filter_tTonKho();

            filter.SetFilter(Filter_tTonKho.Ngay, now);

            return View(TonKhoManager.GetTonKho(filter));
        }
    }
}

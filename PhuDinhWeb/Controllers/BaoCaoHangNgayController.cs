using System;
using System.Web.Mvc;

namespace PhuDinhWeb.Controllers
{
    public class BaoCaoHangNgayController : Controller
    {
        public ActionResult Index()
        {
            return View(PhuDinhData.ReportData.ReportDaily.FilterByDate(DateTime.Now.Date));
        }
    }
}

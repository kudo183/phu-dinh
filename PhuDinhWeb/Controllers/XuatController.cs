using System;
using System.Web.Mvc;
using PhuDinhData.ReportData;

namespace PhuDinhWeb.Controllers
{
    [Authorize]
    public class XuatController : Controller
    {
        public ActionResult Index(DateTime? ngay)
        {
            if (ngay==null)
            {
                ngay = DateTime.Now;
            }
            var result = new ReportDaily.ReportDailyData() { DateFilter = ngay.Value, RowData = ReportDaily.FilterByDate(ngay.Value) };
            return View(result);
        }
    }
}

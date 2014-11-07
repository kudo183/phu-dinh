using System;
using System.Web.Mvc;
using PhuDinhData.ReportData;

namespace PhuDinhWeb.Controllers
{
    [Authorize]
    public class XuatController : Controller
    {
        public ActionResult Index()
        {
            return Index(DateTime.Now.Date);
        }

        [HttpPost]
        public ActionResult Index(DateTime date)
        {
            var result = new ReportDaily.ReportDailyData() { DateFilter = date, RowData = ReportDaily.FilterByDate(date) };
            return View(result);
        }
    }
}

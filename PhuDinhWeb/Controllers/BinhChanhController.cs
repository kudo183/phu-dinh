using System;
using System.Linq;
using System.Web.Mvc;
using PhuDinhData;

namespace PhuDinhWeb.Controllers
{
    public class BinhChanhController : Controller
    {
        public ActionResult Index()
        {
            TonKhoManager.UpdateTonKho();

            var context = ContextFactory.CreateContext();
            var now = DateTime.Now.Date;

            return View(context.tTonKhoes.Where(p => p.MaKhoHang == 2 && p.Ngay == now)
                .OrderBy(p => p.tMatHang.TenMatHangDayDu).ToList());
        }
    }
}

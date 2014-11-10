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
        private readonly Dictionary<string, int> _khos;
        private readonly Dictionary<string, bool> _loais;
        private readonly Dictionary<bool, string> _titlesLoai;
        private readonly Dictionary<int, string> _titles;

        public TonKhoController()
        {
            _khos = new Dictionary<string, int> { { "phudinh", 1 }, { "pd", 1 }, { "binhchanh", 2 }, { "bc", 2 }, { "duchoa", 3 }, { "dh", 3 } };
            _loais = new Dictionary<string, bool> { { "0", false }, { "n", false }, { "nha", false }, { "1", true }, { "tq", true }, { "chtq", true } };
            _titles = new Dictionary<int, string> { { 1, "Phu Dinh" }, { 2, "Binh Chanh" }, { 3, "Duc Hoa" } };
            _titlesLoai = new Dictionary<bool, string> { { false, "Nha" }, { true, "TQ" } };
        }

        public ActionResult Index(string tenKho, string tenLoai)
        {
            if (string.IsNullOrEmpty(tenKho))
            {
                tenKho = "pd";
            }

            if (string.IsNullOrEmpty(tenLoai))
            {
                tenLoai = "n";
            }

            TonKhoManager.UpdateTonKho();

            var now = DateTime.Now.Date;

            var kho = _khos[tenKho];

            ViewBag.TiTle = _titles[kho];

            var filter = new Filter_tTonKho();

            filter.SetFilter(Filter_tTonKho.MaKhoHang, kho);
            filter.SetFilter(Filter_tTonKho.Ngay, now);

            var loai = _loais[tenLoai.ToLower()];

            ViewBag.TiTle = string.Format("{0} - {1}", _titles[kho], _titlesLoai[loai]);

            filter.SetFilter(Filter_tTonKho.TenMatHang, loai == false ? "#ChTQ" : "ChTQ");
            
            return View(TonKhoManager.GetTonKho(filter));
        }
    }
}

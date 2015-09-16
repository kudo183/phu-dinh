using PhuDinhDataEntity;
using System;
using System.Linq;
using System.Collections.Generic;
using PhuDinhCommon;

namespace PhuDinhData
{
    public static class TonKhoManager
    {
        public static void UpdateTonKho(DateTime begin, DateTime end)
        {
            BusinessLogics.CapNhatTonKho.UpdateTonKhosTuNgayDDenNgayN(begin, end);
        }

        private static readonly object _lock = new object();

        public static void UpdateTonKho()
        {
            lock (_lock)
            {
                var now = DateTime.Now.Date;

                var thamSoNgays = ClientContext.Instance.GetData<ThamSoNgay>(p => p.Ten == Constant.ThamSo_NgayCapNhatTonKhoCuoiCung).ToList();
                ThamSoNgay thamSoNgayCapNhatTonKhoCuoiCung;
                if (thamSoNgays.Count == 1)
                {
                    thamSoNgayCapNhatTonKhoCuoiCung = thamSoNgays[0];
                }
                else
                {
                    thamSoNgayCapNhatTonKhoCuoiCung = new ThamSoNgay()
                    {
                        Ten = Constant.ThamSo_NgayCapNhatTonKhoCuoiCung,
                        GiaTri = now.Subtract(new TimeSpan(1, 0, 0, 0))
                    };
                }

                var minDate = thamSoNgayCapNhatTonKhoCuoiCung.GiaTri;

                if (minDate == now)
                {
                    return;
                }

                UpdateTonKho(minDate, now);

                thamSoNgayCapNhatTonKhoCuoiCung.GiaTri = now;

                ClientContext.Instance.AddOrUpdateEntity(thamSoNgayCapNhatTonKhoCuoiCung);
            }
        }

        public static IEnumerable<tTonKho> GetTonKho(Filter.Filter_tTonKho filter)
        {
            var kho = filter.GetFilterValue(Filter.Filter_tTonKho.MaKhoHang);

            var filterCanhBaoTonKho = new Filter.Filter_rCanhBaoTonKho();
            filterCanhBaoTonKho.SetFilter(Filter.Filter_rCanhBaoTonKho.MaKhoHang, kho);

            var canhBaoTonKhos = ClientContext.Instance
                .GetData(filterCanhBaoTonKho.Filter)
                .ToDictionary(p => p.MaMatHang);

            var result = ClientContext.Instance
                .GetDataWithRelated(filter.Filter, new List<string> { "tMatHang" })
                .ToList()
                .OrderBy(p => p.tMatHang.TenMatHangDayDu)
                .ToList();

            foreach (var tTonKho in result.ToList())
            {
                tTonKho.CanhBao = 0;
                if (canhBaoTonKhos.ContainsKey(tTonKho.MaMatHang) == false)
                {
                    continue;
                }

                if (tTonKho.SoLuong == 0 && canhBaoTonKhos[tTonKho.MaMatHang].TonThapNhat <= 0)
                {
                    result.Remove(tTonKho);
                    continue;
                }

                if (tTonKho.SoLuong < canhBaoTonKhos[tTonKho.MaMatHang].TonThapNhat)
                {
                    tTonKho.CanhBao = -1;
                }
                else if (tTonKho.SoLuong > canhBaoTonKhos[tTonKho.MaMatHang].TonCaoNhat)
                {
                    tTonKho.CanhBao = 1;
                }
            }
            
            return result;
        }
    }
}

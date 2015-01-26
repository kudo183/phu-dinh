using PhuDinhData.Repository;
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
            BusinessLogics.BusinessLogics.UpdateTonKhosTuNgayDDenNgayN(ContextFactory.CreateContext(), begin, end);
        }

        private static readonly object _lock = new object();

        public static void UpdateTonKho()
        {
            lock (_lock)
            {
                var context = ContextFactory.CreateContext();

                var now = DateTime.Now.Date;

                var thamSoNgayCapNhatTonKhoCuoiCung = context.ThamSoNgays.FirstOrDefault(p => p.Ten == Constant.ThamSo_NgayCapNhatTonKhoCuoiCung);

                if (thamSoNgayCapNhatTonKhoCuoiCung == null)
                {
                    thamSoNgayCapNhatTonKhoCuoiCung = context.ThamSoNgays.Add(
                        new ThamSoNgay() { Ten = Constant.ThamSo_NgayCapNhatTonKhoCuoiCung, GiaTri = now.Subtract(new TimeSpan(1, 0, 0, 0)) });
                }

                var minDate = thamSoNgayCapNhatTonKhoCuoiCung.GiaTri;

                if (minDate == now)
                {
                    return;
                }

                UpdateTonKho(minDate, now);

                thamSoNgayCapNhatTonKhoCuoiCung.GiaTri = now;

                context.SaveChanges();

                context.Dispose();
            }
        }

        public static IEnumerable<tTonKho> GetTonKho(Filter.Filter_tTonKho filter)
        {
            var context = ContextFactory.CreateContext();

            var kho = filter.GetFilterValue(Filter.Filter_tTonKho.MaKhoHang);

            var filterCanhBaoTonKho = new Filter.Filter_rCanhBaoTonKho();
            filterCanhBaoTonKho.SetFilter(Filter.Filter_rCanhBaoTonKho.MaKhoHang, kho);

            var canhBaoTonKhos = Repository<rCanhBaoTonKho>.GetDataQuery(context, filterCanhBaoTonKho.Filter).ToDictionary(p => p.MaMatHang);

            var result = Repository<tTonKho>.GetDataQuery(context, filter.Filter).OrderBy(p => p.tMatHang.TenMatHangDayDu).ToList();

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

            context.Dispose();

            return result;
        }
    }
}

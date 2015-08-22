using System;
using System.Linq;
using PhuDinhCommon;
using PhuDinhDataEntity;

namespace PhuDinhData
{
    public static class CongNoManager
    {
        public static void UpdateCongNo(DateTime begin, DateTime end)
        {
            BusinessLogics.CapNhatCongNoKhachHang.UpdateCongNoKhachHangTuNgayDDenNgayN(begin, end);
        }

        private static readonly object _lock = new object();

        public static void UpdateCongNo()
        {
            lock (_lock)
            {
                var now = DateTime.Now.Date;

                var thamSoNgays = ClientContext.Instance.GetData<ThamSoNgay>(p => p.Ten == Constant.ThamSo_NgayCapNhatCongNoCuoiCung).ToList();
                ThamSoNgay thamSoNgayCapNhatCongNoCuoiCung;
                if (thamSoNgays.Count == 1)
                {
                    thamSoNgayCapNhatCongNoCuoiCung = thamSoNgays[0];
                }
                else
                {
                    thamSoNgayCapNhatCongNoCuoiCung = new ThamSoNgay()
                    {
                        Ten = Constant.ThamSo_NgayCapNhatCongNoCuoiCung,
                        GiaTri = now.Subtract(new TimeSpan(1, 0, 0, 0))
                    };
                }

                var minDate = thamSoNgayCapNhatCongNoCuoiCung.GiaTri;

                if (minDate == now)
                {
                    return;
                }

                UpdateCongNo(minDate, now);

                thamSoNgayCapNhatCongNoCuoiCung.GiaTri = now;

                ClientContext.Instance.AddOrUpdateEntity(thamSoNgayCapNhatCongNoCuoiCung);
            }
        }
    }
}

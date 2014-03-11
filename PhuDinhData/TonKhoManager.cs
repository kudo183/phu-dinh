using PhuDinhData.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using PhuDinhCommon;

namespace PhuDinhData
{
    public static class TonKhoManager
    {
        public static void UpdateByChiTietDonHang(List<Repository<tChiTietDonHang>.ChangedItemData> changed)
        {
            var context = ContextFactory.CreateContext();
            var thamSoNgayCapNhatTonKhoCuoiCung = context.ThamSoNgays.First(p => p.Ten == Constant.ThamSo_NgayCapNhatTonKhoCuoiCung);
            var minDate = thamSoNgayCapNhatTonKhoCuoiCung.GiaTri;

            var checkedMaDonHang = new List<int>();

            foreach (var changedItemData in changed)
            {
                var chiTietDonHang = changedItemData.CurrentValues;
                if (chiTietDonHang != null)
                {
                    if (checkedMaDonHang.Contains(chiTietDonHang.MaDonHang) == false)
                    {
                        checkedMaDonHang.Add(chiTietDonHang.MaDonHang);
                        var donHang = context.tDonHangs.First(p => p.Ma == chiTietDonHang.MaDonHang);
                        if (donHang.Ngay < minDate)
                        {
                            minDate = donHang.Ngay;
                        }
                    }
                }

                chiTietDonHang = changedItemData.OriginalValues;
                if (chiTietDonHang != null)
                {
                    if (checkedMaDonHang.Contains(chiTietDonHang.MaDonHang) == false)
                    {
                        checkedMaDonHang.Add(chiTietDonHang.MaDonHang);
                        var donHang = context.tDonHangs.First(p => p.Ma == chiTietDonHang.MaDonHang);
                        if (donHang.Ngay < minDate)
                        {
                            minDate = donHang.Ngay;
                        }
                    }
                }
            }

            var now = DateTime.Now.Date;

            UpdateTonKho(minDate, now);

            thamSoNgayCapNhatTonKhoCuoiCung.GiaTri = now;

            context.SaveChanges();
        }

        public static void UpdateByChiTietNhapHang(List<Repository<tChiTietNhapHang>.ChangedItemData> changed)
        {
            var context = ContextFactory.CreateContext();
            var thamSoNgayCapNhatTonKhoCuoiCung = context.ThamSoNgays.First(p => p.Ten == Constant.ThamSo_NgayCapNhatTonKhoCuoiCung);
            var minDate = thamSoNgayCapNhatTonKhoCuoiCung.GiaTri;

            var checkedMaNhapHang = new List<int>();

            foreach (var changedItemData in changed)
            {
                var chiTietNhapHang = changedItemData.CurrentValues;
                if (chiTietNhapHang != null)
                {
                    if (checkedMaNhapHang.Contains(chiTietNhapHang.MaNhapHang) == false)
                    {
                        checkedMaNhapHang.Add(chiTietNhapHang.MaNhapHang);
                        var nhapHang = context.tNhapHangs.First(p => p.Ma == chiTietNhapHang.MaNhapHang);
                        if (nhapHang.Ngay < minDate)
                        {
                            minDate = chiTietNhapHang.tNhapHang.Ngay;
                        }
                    }
                }

                chiTietNhapHang = changedItemData.OriginalValues;
                if (chiTietNhapHang != null)
                {
                    if (checkedMaNhapHang.Contains(chiTietNhapHang.MaNhapHang) == false)
                    {
                        checkedMaNhapHang.Add(chiTietNhapHang.MaNhapHang);
                        var nhapHang = context.tNhapHangs.First(p => p.Ma == chiTietNhapHang.MaNhapHang);
                        if (nhapHang.Ngay < minDate)
                        {
                            minDate = chiTietNhapHang.tNhapHang.Ngay;
                        }
                    }
                }
            }

            var now = DateTime.Now.Date;

            UpdateTonKho(minDate, now);

            thamSoNgayCapNhatTonKhoCuoiCung.GiaTri = now;

            context.SaveChanges();
        }

        public static void UpdateByChiTietChuyenKho(List<Repository<tChiTietChuyenKho>.ChangedItemData> changed)
        {
            var context = ContextFactory.CreateContext();
            var thamSoNgayCapNhatTonKhoCuoiCung = context.ThamSoNgays.First(p => p.Ten == Constant.ThamSo_NgayCapNhatTonKhoCuoiCung);
            var minDate = thamSoNgayCapNhatTonKhoCuoiCung.GiaTri;

            var checkedMaChuyenKho = new List<int>();

            foreach (var changedItemData in changed)
            {
                var chiTietChuyenKho = changedItemData.CurrentValues;
                if (chiTietChuyenKho != null)
                {
                    if (checkedMaChuyenKho.Contains(chiTietChuyenKho.MaChuyenKho) == false)
                    {
                        checkedMaChuyenKho.Add(chiTietChuyenKho.MaChuyenKho);
                        var chuyenKho = context.tChuyenKhoes.First(p => p.Ma == chiTietChuyenKho.MaChuyenKho);
                        if (chuyenKho.Ngay < minDate)
                        {
                            minDate = chiTietChuyenKho.tChuyenKho.Ngay;
                        }
                    }
                }

                chiTietChuyenKho = changedItemData.OriginalValues;
                if (chiTietChuyenKho != null)
                {
                    if (checkedMaChuyenKho.Contains(chiTietChuyenKho.MaChuyenKho) == false)
                    {
                        checkedMaChuyenKho.Add(chiTietChuyenKho.MaChuyenKho);
                        var chuyenKho = context.tChuyenKhoes.First(p => p.Ma == chiTietChuyenKho.MaChuyenKho);
                        if (chuyenKho.Ngay < minDate)
                        {
                            minDate = chiTietChuyenKho.tChuyenKho.Ngay;
                        }
                    }
                }
            }

            var now = DateTime.Now.Date;

            UpdateTonKho(minDate, now);

            thamSoNgayCapNhatTonKhoCuoiCung.GiaTri = now;

            context.SaveChanges();
        }

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
            }
        }
    }
}

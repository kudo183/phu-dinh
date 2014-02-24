using PhuDinhData.Repository;
using System;
using System.Collections.Generic;

namespace PhuDinhData
{
    public static class TonKhoManager
    {
        public static void UpdateByChiTietDonHang(List<Repository<tChiTietDonHang>.ChangedItemData> changed)
        {
            DateTime minDate = DateTime.Now.Date;

            var checkedMaDonHang = new List<int>();

            foreach (var changedItemData in changed)
            {
                var chiTietDonHang = changedItemData.CurrentValues;
                if (chiTietDonHang != null)
                {
                    if (checkedMaDonHang.Contains(chiTietDonHang.MaDonHang) == false)
                    {
                        checkedMaDonHang.Add(chiTietDonHang.MaDonHang);
                        if (chiTietDonHang.tDonHang.Ngay < minDate)
                        {
                            minDate = chiTietDonHang.tDonHang.Ngay;
                        }
                    }
                }

                chiTietDonHang = changedItemData.OriginalValues;
                if (chiTietDonHang != null)
                {
                    if (checkedMaDonHang.Contains(chiTietDonHang.MaDonHang) == false)
                    {
                        checkedMaDonHang.Add(chiTietDonHang.MaDonHang);
                        if (chiTietDonHang.tDonHang.Ngay < minDate)
                        {
                            minDate = chiTietDonHang.tDonHang.Ngay;
                        }
                    }
                }
            }

            UpdateTonKho(minDate);
        }

        public static void UpdateByChiTietNhapHang(List<Repository<tChiTietNhapHang>.ChangedItemData> changed)
        {
            DateTime minDate = DateTime.Now.Date;

            var checkedMaNhapHang = new List<int>();

            foreach (var changedItemData in changed)
            {
                var chiTietDonHang = changedItemData.CurrentValues;
                if (chiTietDonHang != null)
                {
                    if (checkedMaNhapHang.Contains(chiTietDonHang.MaNhapHang) == false)
                    {
                        checkedMaNhapHang.Add(chiTietDonHang.MaNhapHang);
                        if (chiTietDonHang.tNhapHang.Ngay < minDate)
                        {
                            minDate = chiTietDonHang.tNhapHang.Ngay;
                        }
                    }
                }

                chiTietDonHang = changedItemData.OriginalValues;
                if (chiTietDonHang != null)
                {
                    if (checkedMaNhapHang.Contains(chiTietDonHang.MaNhapHang) == false)
                    {
                        checkedMaNhapHang.Add(chiTietDonHang.MaNhapHang);
                        if (chiTietDonHang.tNhapHang.Ngay < minDate)
                        {
                            minDate = chiTietDonHang.tNhapHang.Ngay;
                        }
                    }
                }
            }

            UpdateTonKho(minDate);
        }

        public static void UpdateByChiTietChuyenKho(List<Repository<tChiTietChuyenKho>.ChangedItemData> changed)
        {
            DateTime minDate = DateTime.Now.Date;

            var checkedMaChuyenKho = new List<int>();

            foreach (var changedItemData in changed)
            {
                var chiTietChuyenKho = changedItemData.CurrentValues;
                if (chiTietChuyenKho != null)
                {
                    if (checkedMaChuyenKho.Contains(chiTietChuyenKho.MaChuyenKho) == false)
                    {
                        checkedMaChuyenKho.Add(chiTietChuyenKho.MaChuyenKho);
                        if (chiTietChuyenKho.tChuyenKho.Ngay < minDate)
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
                        if (chiTietChuyenKho.tChuyenKho.Ngay < minDate)
                        {
                            minDate = chiTietChuyenKho.tChuyenKho.Ngay;
                        }
                    }
                }
            }

            UpdateTonKho(minDate);
        }

        public static void UpdateTonKho(DateTime date)
        {
            BusinessLogics.BusinessLogics.UpdateTonKhosTuNgayD(ContextFactory.CreateContext(), date);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using PhuDinhData.Repository;

namespace PhuDinhData.BusinessLogics
{
    public static class BusinessLogics
    {
        public static void UpdateXong(PhuDinhEntities context, List<int> maDonHangs)
        {
            foreach (var maDonHang in maDonHangs)
            {
                var ma = maDonHang;

                var donHang = Repository<tDonHang>.GetData(context, (p => p.Ma == ma)).First();

                donHang.Xong = true;

                foreach (var tChiTietDonHang in donHang.tChiTietDonHangs)
                {
                    if (tChiTietDonHang.tChiTietChuyenHangDonHangs.Sum(p => p.SoLuong) == tChiTietDonHang.SoLuong)
                    {
                        tChiTietDonHang.Xong = true;
                    }
                    else
                    {
                        tChiTietDonHang.Xong = false;
                        donHang.Xong = false;
                    }
                }
            }

            context.SaveChanges();
        }

        public static void UpdateTonKhosTuNgayD(PhuDinhEntities context,
            DateTime ngayD)
        {
            var ngayTruocNgayD = ngayD.Subtract(new TimeSpan(1, 0, 0, 0));

            var tons = TinhChiTietTonTuNgayD(context, ngayTruocNgayD);

            var xuats = TinhChiTietXuatTuNgayD(context, ngayD);

            var nhaps = TinhChiTietNhapTuNgayD(context, ngayD);

            var khos = RepositoryLocator<rKhoHang>.GetData(context, p => true);
            var matHangs = RepositoryLocator<tMatHang>.GetData(context, p => true);

            var now = DateTime.Now.Date;

            var soNgay = (now - ngayD).Days + 1;

            Dictionary<int, Dictionary<DateTime, tTonKho>> tonKho;
            Dictionary<int, Dictionary<DateTime, int>> xuatKho, nhapKho;

            Dictionary<DateTime, tTonKho> tonMatHang;
            Dictionary<DateTime, int> xuatMatHang, nhapMatHang;

            bool bXuat, bNhap;

            foreach (var khoHang in khos)
            {
                var maKhoHang = khoHang.Ma;

                bXuat = xuats.ContainsKey(maKhoHang);
                bNhap = nhaps.ContainsKey(maKhoHang);

                if (tons.ContainsKey(maKhoHang) == false)
                {
                    tons.Add(maKhoHang, new Dictionary<int, Dictionary<DateTime, tTonKho>>());
                }
                tonKho = tons[maKhoHang];

                if (bXuat == false)
                {
                    xuats.Add(maKhoHang, new Dictionary<int, Dictionary<DateTime, int>>());
                }
                xuatKho = xuats[maKhoHang];

                if (bNhap == false)
                {
                    nhaps.Add(maKhoHang, new Dictionary<int, Dictionary<DateTime, int>>());
                }
                nhapKho = nhaps[maKhoHang];

                foreach (var matHang in matHangs)
                {
                    var maMatHang = matHang.Ma;

                    bXuat = xuatKho.ContainsKey(maMatHang);
                    bNhap = nhapKho.ContainsKey(maMatHang);

                    if (tonKho.ContainsKey(maMatHang) == false)
                    {
                        tonKho.Add(maMatHang, new Dictionary<DateTime, tTonKho>());
                    }
                    tonMatHang = tonKho[maMatHang];

                    if (bXuat == false)
                    {
                        xuatKho.Add(maMatHang, new Dictionary<DateTime, int>());
                    }
                    xuatMatHang = xuatKho[maMatHang];

                    if (bNhap == false)
                    {
                        nhapKho.Add(maMatHang, new Dictionary<DateTime, int>());
                    }
                    nhapMatHang = nhapKho[maMatHang];

                    var ngay = ngayD;
                    var ngayTruoc = ngayTruocNgayD;

                    for (var i = 0; i < soNgay; i++)
                    {
                        if (tonMatHang.ContainsKey(ngayTruoc) == false)
                        {
                            ngayTruoc = ngay;
                            ngay = ngay.AddDays(1);
                            continue;
                        }

                        bXuat = xuatMatHang.ContainsKey(ngay);
                        bNhap = nhapMatHang.ContainsKey(ngay);

                        var xuatNgay = bXuat == false ? 0 : xuatMatHang[ngay];

                        var nhapNgay = bNhap == false ? 0 : nhapMatHang[ngay];

                        if (tonMatHang.ContainsKey(ngay) == false)
                        {
                            var t = new tTonKho
                            {
                                Ngay = ngay,
                                MaKhoHang = maKhoHang,
                                MaMatHang = maMatHang,
                                SoLuongCu = tonMatHang[ngayTruoc].SoLuong,
                                SoLuong = tonMatHang[ngayTruoc].SoLuong + nhapNgay - xuatNgay
                            };

                            t.SoLuong = t.SoLuongCu + nhapNgay - xuatNgay;

                            context.tTonKhoes.Add(t);
                            tonMatHang.Add(ngay, t);
                        }
                        else
                        {
                            var tonNgay = tonMatHang[ngay];
                            tonNgay.SoLuongCu = tonMatHang[ngayTruoc].SoLuong;
                            tonNgay.SoLuong = tonNgay.SoLuongCu + nhapNgay - xuatNgay;
                        }
                        
                        ngayTruoc = ngay;
                        ngay = ngay.AddDays(1);
                    }
                }
            }

            context.SaveChanges();
        }

        //<kho, maMatHang, ngay, soLuong>
        private static Dictionary<int, Dictionary<int, Dictionary<DateTime, tTonKho>>> TinhChiTietTonTuNgayD(
            PhuDinhEntities context,
            DateTime ngayD)
        {
            var ton = new Dictionary<int, Dictionary<int, Dictionary<DateTime, tTonKho>>>();

            var tonKhos = RepositoryLocator<tTonKho>.GetData(
                   context, p => p.Ngay >= ngayD);

            foreach (var tonKho in tonKhos)
            {
                var kho = tonKho.MaKhoHang;
                if (ton.ContainsKey(kho) == false)
                {
                    ton.Add(kho, new Dictionary<int, Dictionary<DateTime, tTonKho>>());
                }

                var maMatHang = tonKho.MaMatHang;
                var chiTietKho = ton[kho];
                if (chiTietKho.ContainsKey(maMatHang) == false)
                {
                    chiTietKho.Add(maMatHang, new Dictionary<DateTime, tTonKho>());
                }

                var ngay = tonKho.Ngay;
                var chiTietMatHang = chiTietKho[maMatHang];
                if (chiTietMatHang.ContainsKey(ngay) == false)
                {
                    chiTietMatHang.Add(ngay, tonKho);
                }
            }

            return ton;
        }

        //<kho, maMatHang, ngay, soLuong>
        private static Dictionary<int, Dictionary<int, Dictionary<DateTime, int>>> TinhChiTietXuatTuNgayD(
            PhuDinhEntities context,
            DateTime ngayD)
        {
            var xuat = new Dictionary<int, Dictionary<int, Dictionary<DateTime, int>>>();

            var donHangs = RepositoryLocator<tDonHang>.GetData(
                context, p => p.Ngay >= ngayD);

            foreach (var donHang in donHangs)
            {
                var kho = donHang.MaKhoHang;
                if (xuat.ContainsKey(kho) == false)
                {
                    xuat.Add(kho, new Dictionary<int, Dictionary<DateTime, int>>());
                }

                var chiTietKho = xuat[kho];
                foreach (var chiTietDonHang in donHang.tChiTietDonHangs)
                {
                    var maMatHang = chiTietDonHang.MaMatHang;
                    if (chiTietKho.ContainsKey(maMatHang) == false)
                    {
                        chiTietKho.Add(maMatHang, new Dictionary<DateTime, int>());
                    }

                    var ngay = donHang.Ngay;
                    var chiTietMatHang = chiTietKho[maMatHang];
                    if (chiTietMatHang.ContainsKey(ngay) == false)
                    {
                        chiTietMatHang.Add(ngay, 0);
                    }

                    chiTietMatHang[ngay] += chiTietDonHang.SoLuong;
                }
            }

            var chuyenKhos = RepositoryLocator<tChuyenKho>.GetData(
                context, p => p.Ngay >= ngayD);

            foreach (var chuyenKho in chuyenKhos)
            {
                var kho = chuyenKho.MaKhoHangXuat;
                if (xuat.ContainsKey(kho) == false)
                {
                    xuat.Add(kho, new Dictionary<int, Dictionary<DateTime, int>>());
                }

                var chiTietKho = xuat[kho];
                foreach (var chiTietChuyenKho in chuyenKho.tChiTietChuyenKhoes)
                {
                    var maMatHang = chiTietChuyenKho.MaMatHang;
                    if (chiTietKho.ContainsKey(maMatHang) == false)
                    {
                        chiTietKho.Add(maMatHang, new Dictionary<DateTime, int>());
                    }

                    var ngay = chuyenKho.Ngay;
                    var chiTietMatHang = chiTietKho[maMatHang];
                    if (chiTietMatHang.ContainsKey(ngay) == false)
                    {
                        chiTietMatHang.Add(ngay, 0);
                    }

                    chiTietMatHang[ngay] += chiTietChuyenKho.SoLuong;
                }
            }

            return xuat;
        }

        //<ngay, kho, maMatHang, soLuong>
        private static Dictionary<int, Dictionary<int, Dictionary<DateTime, int>>> TinhChiTietNhapTuNgayD(
            PhuDinhEntities context,
            DateTime ngayD)
        {
            var nhap = new Dictionary<int, Dictionary<int, Dictionary<DateTime, int>>>();

            var nhapHangs = RepositoryLocator<tNhapHang>.GetData(
                context, p => p.Ngay >= ngayD);

            foreach (var nhapHang in nhapHangs)
            {
                var kho = nhapHang.MaKhoHang;
                if (nhap.ContainsKey(kho) == false)
                {
                    nhap.Add(kho, new Dictionary<int, Dictionary<DateTime, int>>());
                }

                var chiTietKho = nhap[kho];
                foreach (var chiTietDonHang in nhapHang.tChiTietNhapHangs)
                {
                    var maMatHang = chiTietDonHang.MaMatHang;
                    if (chiTietKho.ContainsKey(maMatHang) == false)
                    {
                        chiTietKho.Add(maMatHang, new Dictionary<DateTime, int>());
                    }

                    var ngay = nhapHang.Ngay;
                    var chiTietMatHang = chiTietKho[maMatHang];
                    if (chiTietMatHang.ContainsKey(ngay) == false)
                    {
                        chiTietMatHang.Add(ngay, 0);
                    }

                    chiTietMatHang[ngay] += chiTietDonHang.SoLuong;
                }
            }

            var chuyenKhos = RepositoryLocator<tChuyenKho>.GetData(
                context, p => p.Ngay >= ngayD);

            foreach (var chuyenKho in chuyenKhos)
            {
                var kho = chuyenKho.MaKhoHangNhap;
                if (nhap.ContainsKey(kho) == false)
                {
                    nhap.Add(kho, new Dictionary<int, Dictionary<DateTime, int>>());
                }

                var chiTietKho = nhap[kho];
                foreach (var chiTietChuyenKho in chuyenKho.tChiTietChuyenKhoes)
                {
                    var maMatHang = chiTietChuyenKho.MaMatHang;
                    if (chiTietKho.ContainsKey(maMatHang) == false)
                    {
                        chiTietKho.Add(maMatHang, new Dictionary<DateTime, int>());
                    }

                    var ngay = chuyenKho.Ngay;
                    var chiTietMatHang = chiTietKho[maMatHang];
                    if (chiTietMatHang.ContainsKey(ngay) == false)
                    {
                        chiTietMatHang.Add(ngay, 0);
                    }

                    chiTietMatHang[ngay] += chiTietChuyenKho.SoLuong;
                }
            }

            return nhap;
        }
    }
}

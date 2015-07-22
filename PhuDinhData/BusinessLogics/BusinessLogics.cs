using PhuDinhDataEntity;
using System;
using System.Collections.Generic;

namespace PhuDinhData.BusinessLogics
{
    public static class BusinessLogics
    {
        public static void UpdateTonKhosTuNgayDDenNgayN(
            DateTime ngayD,
            DateTime ngayN)
        {
            var ngayTruocNgayD = ngayD.Subtract(new TimeSpan(1, 0, 0, 0));

            var tons = TinhChiTietTonTuNgayD(ngayTruocNgayD, ngayN);

            var xuats = TinhChiTietXuatTuNgayD(ngayD, ngayN);

            var nhaps = TinhChiTietNhapTuNgayD(ngayD, ngayN);

            var khos = ClientContext.Instance.GetData<rKhoHang>(null);
            var matHangs = ClientContext.Instance.GetData<tMatHang>(null);

            var soNgay = (ngayN - ngayD).Days + 1;

            Dictionary<int, Dictionary<DateTime, tTonKho>> tonKho;
            Dictionary<int, Dictionary<DateTime, int>> xuatKho, nhapKho;

            Dictionary<DateTime, tTonKho> tonMatHang;
            Dictionary<DateTime, int> xuatMatHang, nhapMatHang;

            List<tTonKho> lstTonKhoAdded = new List<tTonKho>();
            List<tTonKho> lstTonKhoUpdated = new List<tTonKho>();

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
                                SoLuong = tonMatHang[ngayTruoc].SoLuong + nhapNgay - xuatNgay
                            };

                            lstTonKhoAdded.Add(t);
                            tonMatHang.Add(ngay, t);
                        }
                        else
                        {
                            var tonNgay = tonMatHang[ngay];
                            var soLuong = tonMatHang[ngayTruoc].SoLuong + nhapNgay - xuatNgay;
                            if (tonNgay.SoLuong != soLuong)
                            {
                                tonNgay.SoLuong = soLuong;
                                lstTonKhoUpdated.Add(tonNgay);
                            }
                        }

                        ngayTruoc = ngay;
                        ngay = ngay.AddDays(1);
                    }
                }
            }

            var addedOrUpdated = new List<tTonKho>();
            addedOrUpdated.AddRange(lstTonKhoAdded);
            addedOrUpdated.AddRange(lstTonKhoUpdated);

            ClientContext.Instance.AddOrUpdateEntities(addedOrUpdated);
        }

        //<kho, maMatHang, ngay, soLuong>
        private static Dictionary<int, Dictionary<int, Dictionary<DateTime, tTonKho>>> TinhChiTietTonTuNgayD(
            DateTime ngayD,
            DateTime ngayN)
        {
            var ton = new Dictionary<int, Dictionary<int, Dictionary<DateTime, tTonKho>>>();

            var tonKhos = ClientContext.Instance
                .GetData<tTonKho>(p => p.Ngay >= ngayD && p.Ngay <= ngayN);

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
            DateTime ngayD,
            DateTime ngayN)
        {
            var xuat = new Dictionary<int, Dictionary<int, Dictionary<DateTime, int>>>();

            var donHangs = ClientContext.Instance
                .GetDataWithRelated<tDonHang>(p => p.Ngay >= ngayD && p.Ngay <= ngayN, new List<string> { "tChiTietDonHangs" });

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

            var chuyenKhos = ClientContext.Instance
                .GetDataWithRelated<tChuyenKho>(p => p.Ngay >= ngayD && p.Ngay <= ngayN, new List<string> { "tChiTietChuyenKhoes" });

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
            DateTime ngayD,
            DateTime ngayN)
        {
            var nhap = new Dictionary<int, Dictionary<int, Dictionary<DateTime, int>>>();

            var nhapHangs = ClientContext.Instance
                .GetDataWithRelated<tNhapHang>(p => p.Ngay >= ngayD && p.Ngay <= ngayN, new List<string> { "tChiTietNhapHangs" });

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

            var chuyenKhos = ClientContext.Instance
                .GetDataWithRelated<tChuyenKho>(p => p.Ngay >= ngayD && p.Ngay <= ngayN, new List<string> { "tChiTietChuyenKhoes" });

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

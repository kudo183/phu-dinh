using System;
using System.Collections.Generic;
using PhuDinhDataEntity;

namespace PhuDinhData.BusinessLogics
{
    public static class CapNhatCongNoKhachHang
    {
        public static void UpdateCongNoKhachHangTuNgayDDenNgayN(
            DateTime ngayD,
            DateTime ngayN)
        {
            var ngayTruocNgayD = ngayD.Subtract(new TimeSpan(1, 0, 0, 0));

            var congNos = TinhChiTietCongNoKhachHangTuNgayD(ngayTruocNgayD, ngayN);

            var nhanTiens = TinhChiTietNhanTienKhachHangNgayD(ngayD, ngayN);

            var giamTrus = TinhChiTietGiamTruKhachHangNgayD(ngayD, ngayN);

            var phuThus = TinhChiTietPhuThuKhachHangNgayD(ngayD, ngayN);

            var toaHangs = TinhChiTietToaHangTuNgayD(ngayD, ngayN);

            var khachHangs = ClientContext.Instance.GetData<rKhachHang>(null);

            var soNgay = (ngayN - ngayD).Days + 1;

            Dictionary<DateTime, tCongNoKhachHang> congNo;
            Dictionary<DateTime, int> nhanTien, giamTru, phuThu, toaHang;

            List<tCongNoKhachHang> lstCongNoAdded = new List<tCongNoKhachHang>();
            List<tCongNoKhachHang> lstCongNoUpdated = new List<tCongNoKhachHang>();

            bool bNhanTien, bGiamTru, bPhuThu, bToaHang;

            foreach (var khachHang in khachHangs)
            {
                var maKhachHang = khachHang.Ma;

                bNhanTien = nhanTiens.ContainsKey(maKhachHang);
                bGiamTru = giamTrus.ContainsKey(maKhachHang);
                bPhuThu = phuThus.ContainsKey(maKhachHang);
                bToaHang = toaHangs.ContainsKey(maKhachHang);

                if (congNos.ContainsKey(maKhachHang) == false)
                {
                    congNos.Add(maKhachHang, new Dictionary<DateTime, tCongNoKhachHang>());
                }
                congNo = congNos[maKhachHang];

                if (bNhanTien == false)
                {
                    nhanTiens.Add(maKhachHang, new Dictionary<DateTime, int>());
                }
                nhanTien = nhanTiens[maKhachHang];

                if (bGiamTru == false)
                {
                    giamTrus.Add(maKhachHang, new Dictionary<DateTime, int>());
                }
                giamTru = giamTrus[maKhachHang];

                if (bPhuThu == false)
                {
                    phuThus.Add(maKhachHang, new Dictionary<DateTime, int>());
                }
                phuThu = phuThus[maKhachHang];

                if (bToaHang == false)
                {
                    toaHangs.Add(maKhachHang, new Dictionary<DateTime, int>());
                }
                toaHang = toaHangs[maKhachHang];

                var ngay = ngayD;
                var ngayTruoc = ngayTruocNgayD;

                for (var i = 0; i < soNgay; i++)
                {
                    if (congNo.ContainsKey(ngayTruoc) == false)
                    {
                        ngayTruoc = ngay;
                        ngay = ngay.AddDays(1);
                        continue;
                    }

                    bNhanTien = nhanTien.ContainsKey(ngay);
                    bGiamTru = giamTru.ContainsKey(ngay);
                    bPhuThu = phuThu.ContainsKey(ngay);
                    bToaHang = toaHang.ContainsKey(ngay);

                    var nhanTienNgay = bNhanTien == false ? 0 : nhanTien[ngay];
                    var giamTruNgay = bGiamTru == false ? 0 : giamTru[ngay];
                    var phuThuNgay = bPhuThu == false ? 0 : phuThu[ngay];
                    var toaHangNgay = bToaHang == false ? 0 : toaHang[ngay];

                    if (congNo.ContainsKey(ngay) == false)
                    {
                        var t = new tCongNoKhachHang()
                        {
                            Ngay = ngay,
                            MaKhachHang = maKhachHang,
                            SoTien = congNo[ngayTruoc].SoTien + toaHangNgay - nhanTienNgay - giamTruNgay + phuThuNgay
                        };

                        lstCongNoAdded.Add(t);
                        congNo.Add(ngay, t);
                    }
                    else
                    {
                        var congNoNgay = congNo[ngay];
                        var soTien = congNo[ngayTruoc].SoTien + toaHangNgay - nhanTienNgay - giamTruNgay + phuThuNgay;
                        if (congNoNgay.SoTien != soTien)
                        {
                            congNoNgay.SoTien = soTien;
                            lstCongNoUpdated.Add(congNoNgay);
                        }
                    }

                    ngayTruoc = ngay;
                    ngay = ngay.AddDays(1);
                }
            }

            var addedOrUpdated = new List<tCongNoKhachHang>();
            addedOrUpdated.AddRange(lstCongNoAdded);
            addedOrUpdated.AddRange(lstCongNoUpdated);

            ClientContext.Instance.AddOrUpdateEntities(addedOrUpdated);
        }

        //<maKhachHang, ngay, congNo>
        private static Dictionary<int, Dictionary<DateTime, tCongNoKhachHang>> TinhChiTietCongNoKhachHangTuNgayD(
            DateTime ngayD,
            DateTime ngayN)
        {
            var result = new Dictionary<int, Dictionary<DateTime, tCongNoKhachHang>>();

            var congNos = ClientContext.Instance
                .GetData<tCongNoKhachHang>(p => p.Ngay >= ngayD && p.Ngay <= ngayN);

            foreach (var congNo in congNos)
            {
                var maKhacHang = congNo.MaKhachHang;
                if (result.ContainsKey(maKhacHang) == false)
                {
                    result.Add(maKhacHang, new Dictionary<DateTime, tCongNoKhachHang>());
                }

                var ngay = congNo.Ngay;
                var khachHang = result[maKhacHang];
                if (khachHang.ContainsKey(ngay) == false)
                {
                    khachHang.Add(ngay, congNo);
                }
            }

            return result;
        }

        //<maKhachHang, ngay, soTien>
        private static Dictionary<int, Dictionary<DateTime, int>> TinhChiTietToaHangTuNgayD(
            DateTime ngayD,
            DateTime ngayN)
        {
            var result = new Dictionary<int, Dictionary<DateTime, int>>();

            var toaHangs = ClientContext.Instance
                .GetDataWithRelated<tToaHang>(p => p.Ngay >= ngayD && p.Ngay <= ngayN, new List<string> { "tChiTietToaHangs.tChiTietDonHang" });

            foreach (var toaHang in toaHangs)
            {
                var maKhachHang = toaHang.MaKhachHang;
                if (result.ContainsKey(maKhachHang) == false)
                {
                    result.Add(maKhachHang, new Dictionary<DateTime, int>());
                }

                var ngay = toaHang.Ngay;
                var khachHang = result[maKhachHang];

                if (khachHang.ContainsKey(ngay) == false)
                {
                    khachHang.Add(ngay, 0);
                }

                khachHang[ngay] += toaHang.ThanhTien;
            }

            return result;
        }

        //<maKhachHang, ngay, soLuong>
        private static Dictionary<int, Dictionary<DateTime, int>> TinhChiTietNhanTienKhachHangNgayD(
            DateTime ngayD,
            DateTime ngayN)
        {
            var result = new Dictionary<int, Dictionary<DateTime, int>>();

            var nhanTiens = ClientContext.Instance
                .GetData<tNhanTienKhachHang>(p => p.Ngay >= ngayD && p.Ngay <= ngayN);

            foreach (var nhanTien in nhanTiens)
            {
                var maKhachHang = nhanTien.MaKhachHang;
                if (result.ContainsKey(maKhachHang) == false)
                {
                    result.Add(maKhachHang, new Dictionary<DateTime, int>());
                }

                var ngay = nhanTien.Ngay;
                var khachHang = result[maKhachHang];

                if (khachHang.ContainsKey(ngay) == false)
                {
                    khachHang.Add(ngay, 0);
                }

                khachHang[ngay] += nhanTien.SoTien;
            }

            return result;
        }

        //<maKhachHang, ngay, soLuong>
        private static Dictionary<int, Dictionary<DateTime, int>> TinhChiTietGiamTruKhachHangNgayD(
            DateTime ngayD,
            DateTime ngayN)
        {
            var result = new Dictionary<int, Dictionary<DateTime, int>>();

            var giamTrus = ClientContext.Instance
                .GetData<tGiamTruKhachHang>(p => p.Ngay >= ngayD && p.Ngay <= ngayN);

            foreach (var giamTru in giamTrus)
            {
                var maKhachHang = giamTru.MaKhachHang;
                if (result.ContainsKey(maKhachHang) == false)
                {
                    result.Add(maKhachHang, new Dictionary<DateTime, int>());
                }

                var ngay = giamTru.Ngay;
                var khachHang = result[maKhachHang];

                if (khachHang.ContainsKey(ngay) == false)
                {
                    khachHang.Add(ngay, 0);
                }

                khachHang[ngay] += giamTru.SoTien;
            }

            return result;
        }

        //<maKhachHang, ngay, soLuong>
        private static Dictionary<int, Dictionary<DateTime, int>> TinhChiTietPhuThuKhachHangNgayD(
            DateTime ngayD,
            DateTime ngayN)
        {
            var result = new Dictionary<int, Dictionary<DateTime, int>>();

            var phuThus = ClientContext.Instance
                .GetData<tPhuThuKhachHang>(p => p.Ngay >= ngayD && p.Ngay <= ngayN);

            foreach (var phuThu in phuThus)
            {
                var maKhachHang = phuThu.MaKhachHang;
                if (result.ContainsKey(maKhachHang) == false)
                {
                    result.Add(maKhachHang, new Dictionary<DateTime, int>());
                }

                var ngay = phuThu.Ngay;
                var khachHang = result[maKhachHang];

                if (khachHang.ContainsKey(ngay) == false)
                {
                    khachHang.Add(ngay, 0);
                }

                khachHang[ngay] += phuThu.SoTien;
            }

            return result;
        }
    }
}

using PhuDinhDataEntity;
using System;
using System.Collections.Generic;

namespace PhuDinhData.ReportData
{
    public static class ReportNhapHang
    {
        public const string ReportType_All = "Tat Ca";
        public const string ReportType_TuLam = "Tu Lam";
        public const string ReportType_LayNgoai = "Lay Ngoai";

        public class ReportNhapHangData
        {
            public string TenNhaCungCap { get; set; }
            public string TenNhanVien { get; set; }
            public string TenKho { get; set; }
            public string TenMatHang { get; set; }
            public int SoLuong { get; set; }
        }

        public static List<ReportNhapHangData> FilterByDate(DateTime ngay, string type)
        {
            switch (type)
            {
                case ReportType_LayNgoai:
                    return FilterLayNgoai(p => p.Ngay == ngay && p.MaNhaCungCap != 6);
                case ReportType_TuLam:
                    return FilterTuLam(p => p.Ngay == ngay && p.MaNhaCungCap == 6);
                case ReportType_All:
                    return Filter(p => p.Ngay == ngay);
                default:
                    return null;
            }
        }

        public static List<ReportNhapHangData> FilterByDate(DateTime tuNgay, DateTime denNgay, string type)
        {
            switch (type)
            {
                case ReportType_LayNgoai:
                    return FilterLayNgoai(p => p.Ngay >= tuNgay && p.Ngay <= denNgay && p.MaNhaCungCap != 6);
                case ReportType_TuLam:
                    return FilterTuLam(p => p.Ngay >= tuNgay && p.Ngay <= denNgay && p.MaNhaCungCap == 6);
                case ReportType_All:
                    return Filter(p => p.Ngay >= tuNgay && p.Ngay <= denNgay);
                default:
                    return null;
            }
        }

        private static List<ReportNhapHangData> Filter(System.Linq.Expressions.Expression<Func<tNhapHang, bool>> filter)
        {
            return new List<ReportNhapHangData>();
        }

        private static List<ReportNhapHangData> FilterTuLam(System.Linq.Expressions.Expression<Func<tNhapHang, bool>> filter)
        {
            var nhapHangs = ClientContext.Instance.GetDataWithRelated(filter, new List<string> { "rKhoHang", "rNhanVien" });

            var khoHangs = new Dictionary<int, string>();
            var NhanViens = new Dictionary<int, string>();
            var matHangs = new Dictionary<int, string>();

            var gKhoHangs = new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();
            foreach (var nhapHang in nhapHangs)
            {
                if (khoHangs.ContainsKey(nhapHang.MaKhoHang) == false)
                {
                    khoHangs.Add(nhapHang.MaKhoHang, nhapHang.rKhoHang.TenKho);
                }
                if (NhanViens.ContainsKey(nhapHang.MaNhanVien) == false)
                {
                    NhanViens.Add(nhapHang.MaNhanVien, nhapHang.rNhanVien.TenNhanVien);
                }

                if (gKhoHangs.ContainsKey(nhapHang.MaKhoHang) == false)
                {
                    gKhoHangs.Add(nhapHang.MaKhoHang, new Dictionary<int, Dictionary<int, int>>());
                }
                var gNhanViens = gKhoHangs[nhapHang.MaKhoHang];
                if (gNhanViens.ContainsKey(nhapHang.MaNhanVien) == false)
                {
                    gNhanViens.Add(nhapHang.MaNhanVien, new Dictionary<int, int>());
                }
                var gMatHangs = gNhanViens[nhapHang.MaNhanVien];

                var maNhapHang = nhapHang.Ma;
                var tChiTietNhapHangs = PhuDinhData.ClientContext.Instance
                    .GetDataWithRelated<tChiTietNhapHang>(p => p.MaNhapHang == maNhapHang, new List<string> { "tMatHang" });
                foreach (var chiTietDonHang in tChiTietNhapHangs)
                {
                    if (matHangs.ContainsKey(chiTietDonHang.MaMatHang) == false)
                    {
                        matHangs.Add(chiTietDonHang.MaMatHang, chiTietDonHang.tMatHang.TenMatHangDayDu);
                    }

                    if (gMatHangs.ContainsKey(chiTietDonHang.MaMatHang) == false)
                    {
                        gMatHangs.Add(chiTietDonHang.MaMatHang, 0);
                    }

                    gMatHangs[chiTietDonHang.MaMatHang] += chiTietDonHang.SoLuong;
                }
            }

            var result = new List<ReportNhapHangData>();

            foreach (var gKhoHang in gKhoHangs)
            {
                result.Add(new ReportNhapHangData()
                {
                    TenKho = khoHangs[gKhoHang.Key]
                });
                foreach (var gNhanVien in gKhoHang.Value)
                {
                    result.Add(new ReportNhapHangData()
                    {
                        TenNhanVien = NhanViens[gNhanVien.Key]
                    });
                    foreach (var gMatHang in gNhanVien.Value)
                    {
                        result.Add(new ReportNhapHangData()
                        {
                            TenMatHang = matHangs[gMatHang.Key],
                            SoLuong = gMatHang.Value
                        });
                    }
                }
            }

            return result;
        }

        private static List<ReportNhapHangData> FilterLayNgoai(System.Linq.Expressions.Expression<Func<tNhapHang, bool>> filter)
        {
            var nhapHangs = ClientContext.Instance.GetDataWithRelated(filter, new List<string> { "rKhoHang", "rNhaCungCap" });

            var khoHangs = new Dictionary<int, string>();
            var nhaCungCaps = new Dictionary<int, string>();
            var matHangs = new Dictionary<int, string>();

            var gKhoHangs = new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();
            foreach (var nhapHang in nhapHangs)
            {
                if (khoHangs.ContainsKey(nhapHang.MaKhoHang) == false)
                {
                    khoHangs.Add(nhapHang.MaKhoHang, nhapHang.rKhoHang.TenKho);
                }
                if (nhaCungCaps.ContainsKey(nhapHang.MaNhaCungCap) == false)
                {
                    nhaCungCaps.Add(nhapHang.MaNhaCungCap, nhapHang.rNhaCungCap.TenNhaCungCap);
                }

                if (gKhoHangs.ContainsKey(nhapHang.MaKhoHang) == false)
                {
                    gKhoHangs.Add(nhapHang.MaKhoHang, new Dictionary<int, Dictionary<int, int>>());
                }
                var gNhaCungCaps = gKhoHangs[nhapHang.MaKhoHang];
                if (gNhaCungCaps.ContainsKey(nhapHang.MaNhaCungCap) == false)
                {
                    gNhaCungCaps.Add(nhapHang.MaNhaCungCap, new Dictionary<int, int>());
                }
                var gMatHangs = gNhaCungCaps[nhapHang.MaNhaCungCap];

                var maNhapHang = nhapHang.Ma;
                var tChiTietNhapHangs = ClientContext.Instance
                    .GetDataWithRelated<tChiTietNhapHang>(p => p.MaNhapHang == maNhapHang, new List<string> { "tMatHang" });
                foreach (var chiTietDonHang in tChiTietNhapHangs)
                {
                    if (matHangs.ContainsKey(chiTietDonHang.MaMatHang) == false)
                    {
                        matHangs.Add(chiTietDonHang.MaMatHang, chiTietDonHang.tMatHang.TenMatHangDayDu);
                    }

                    if (gMatHangs.ContainsKey(chiTietDonHang.MaMatHang) == false)
                    {
                        gMatHangs.Add(chiTietDonHang.MaMatHang, 0);
                    }

                    gMatHangs[chiTietDonHang.MaMatHang] += chiTietDonHang.SoLuong;
                }
            }

            var result = new List<ReportNhapHangData>();

            foreach (var gKhoHang in gKhoHangs)
            {
                result.Add(new ReportNhapHangData()
                {
                    TenKho = khoHangs[gKhoHang.Key]
                });
                foreach (var gKhachHang in gKhoHang.Value)
                {
                    result.Add(new ReportNhapHangData()
                    {
                        TenNhaCungCap = nhaCungCaps[gKhachHang.Key]
                    });
                    foreach (var gMatHang in gKhachHang.Value)
                    {
                        result.Add(new ReportNhapHangData()
                        {
                            TenMatHang = matHangs[gMatHang.Key],
                            SoLuong = gMatHang.Value
                        });
                    }
                }
            }

            return result;
        }
    }
}

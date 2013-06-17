using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public class DDtMatHang
    {
        public static List<tMatHang> tMatHangs = new List<tMatHang>()
        {
            new tMatHang() {Ma = 1, MaLoai = 1, TenMatHang = "Mat hang 1", LoaiHang = new rLoaiHang(){Ma = 1, TenLoai = "Loai hang 1"},
                rLoaiHangList = DDrLoaiHang.rLoaiHangs
            },
            new tMatHang() {Ma = 2, MaLoai = 2, TenMatHang = "Mat hang 2", LoaiHang = new rLoaiHang(){Ma = 2, TenLoai = "Loai hang 2"},
                rLoaiHangList = DDrLoaiHang.rLoaiHangs
            },
            new tMatHang() {Ma = 3, MaLoai = 3, TenMatHang = "Mat hang 3", LoaiHang = new rLoaiHang(){Ma = 3, TenLoai = "Loai hang 3"},
                rLoaiHangList = DDrLoaiHang.rLoaiHangs
            },
            new tMatHang() {Ma = 4, MaLoai = 4, TenMatHang = "Mat hang 4", LoaiHang = new rLoaiHang(){Ma = 4, TenLoai = "Loai hang 4"},
                rLoaiHangList = DDrLoaiHang.rLoaiHangs
            },
            new tMatHang() {Ma = 5, MaLoai = 5, TenMatHang = "Mat hang 5", LoaiHang = new rLoaiHang(){Ma = 5, TenLoai = "Loai hang 5"},
                rLoaiHangList = DDrLoaiHang.rLoaiHangs
            },
            new tMatHang() {Ma = 6, MaLoai = 6, TenMatHang = "Mat hang 6", LoaiHang = new rLoaiHang(){Ma = 6, TenLoai = "Loai hang 6"},
                rLoaiHangList = DDrLoaiHang.rLoaiHangs
            },
            new tMatHang() {Ma = 7, MaLoai = 7, TenMatHang = "Mat hang 7", LoaiHang = new rLoaiHang(){Ma = 7, TenLoai = "Loai hang 7"},
                rLoaiHangList = DDrLoaiHang.rLoaiHangs
            },
            new tMatHang() {Ma = 8, MaLoai = 8, TenMatHang = "Mat hang 8", LoaiHang = new rLoaiHang(){Ma = 8, TenLoai = "Loai hang 8"},
                rLoaiHangList = DDrLoaiHang.rLoaiHangs
            },
            new tMatHang() {Ma = 9, MaLoai = 9, TenMatHang = "Mat hang 9", LoaiHang = new rLoaiHang(){Ma = 9, TenLoai = "Loai hang 9"},
                rLoaiHangList = DDrLoaiHang.rLoaiHangs
            },
            new tMatHang() {Ma = 10, MaLoai = 10, TenMatHang = "Mat hang 10", LoaiHang = new rLoaiHang(){Ma = 10, TenLoai = "Loai hang 10"},
                rLoaiHangList = DDrLoaiHang.rLoaiHangs
            }
        };
    }
}

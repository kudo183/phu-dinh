using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public class DDrChanh
    {
        public static List<rChanh> rChanhs = new List<rChanh>()
        {
            new rChanh() {Ma = 1, MaBaiXe = 1, TenChanh = "Chanh 1", BaiXe = new rBaiXe(){Ma = 1, DiaDiemBaiXe = "Bai xe 1"},
                rBaiXeList = DDrBaiXe.rBaiXes
            },
            new rChanh() {Ma = 2, MaBaiXe = 2, TenChanh = "Chanh 2", BaiXe = new rBaiXe(){Ma = 2, DiaDiemBaiXe = "Bai xe 2"},
                rBaiXeList = DDrBaiXe.rBaiXes
            },
            new rChanh() {Ma = 3, MaBaiXe = 3, TenChanh = "Chanh 3", BaiXe = new rBaiXe(){Ma = 3, DiaDiemBaiXe = "Bai xe 3"},
                rBaiXeList = DDrBaiXe.rBaiXes
            },
            new rChanh() {Ma = 4, MaBaiXe = 4, TenChanh = "Chanh 4", BaiXe = new rBaiXe(){Ma = 4, DiaDiemBaiXe = "Bai xe 4"},
                rBaiXeList = DDrBaiXe.rBaiXes
            },
            new rChanh() {Ma = 5, MaBaiXe = 5, TenChanh = "Chanh 5", BaiXe = new rBaiXe(){Ma = 5, DiaDiemBaiXe = "Bai xe 5"},
                rBaiXeList = DDrBaiXe.rBaiXes
            }
        };
    }
}

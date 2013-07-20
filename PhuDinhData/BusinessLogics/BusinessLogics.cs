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
    }
}

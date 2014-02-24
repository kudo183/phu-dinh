using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChuyenKho
    {
        private static ChuyenKhoViewModel _viewModel;
        public static ChuyenKhoViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChuyenKhoViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tChuyenKho Create(int i)
        {
            return new tChuyenKho()
            {
                Ma = i,
                MaNhanVien = i,
                MaKhoHangXuat = i,
                MaKhoHangNhap = i,
                Ngay = System.DateTime.Now,
                rNhanVien= DDrNhanVien.Create(i),
                rNhanVienList = DDrNhanVien.ViewModel.Entity.ToList(),
                rKhoHangXuat = DDrKhoHang.Create(i),
                rKhoHangNhap = DDrKhoHang.Create(i),
                rKhoHangList = DDrKhoHang.ViewModel.Entity.ToList()
            };
        }
    }
}

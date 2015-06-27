using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtNhapHang
    {
        private static NhapHangViewModel _viewModel;
        public static NhapHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new NhapHangViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tNhapHang Create(int i)
        {
            return new tNhapHang()
            {
                Ma = i,
                MaNhaCungCap = i,
                MaNhanVien = i,
                MaKhoHang = i,
                Ngay = System.DateTime.Now,
                rNhanVien= DDrNhanVien.Create(i),
                rNhanVienList = DDrNhanVien.ViewModel.Entity.ToList(),
                rNhaCungCap = DDrNhaCungCap.Create(i),
                rNhaCungCapList = DDrNhaCungCap.ViewModel.Entity.ToList(),
                rKhoHang = DDrKhoHang.Create(i),
                rKhoHangList = DDrKhoHang.ViewModel.Entity.ToList()
            };
        }
    }
}

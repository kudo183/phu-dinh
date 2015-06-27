using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChuyenHang
    {
        private static ChuyenHangViewModel _viewModel;
        public static ChuyenHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChuyenHangViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tChuyenHang Create(int i)
        {
            return new tChuyenHang()
            {
                Ma = i,
                MaNhanVienGiaoHang = i,
                Ngay = System.DateTime.Now,
                Gio = System.DateTime.Now.TimeOfDay,
                rNhanVien = DDrNhanVien.Create(i),
                rNhanVienList = DDrNhanVien.ViewModel.Entity.ToList()
            };
        }
    }
}

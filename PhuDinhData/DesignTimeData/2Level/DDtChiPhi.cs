using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChiPhi
    {
        private static ChiPhiViewModel _viewModel;
        public static ChiPhiViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChiPhiViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tChiPhi Create(int i)
        {
            return new tChiPhi()
            {
                Ma = i,
                MaLoaiChiPhi = i,
                MaNhanVienGiaoHang = i,
                SoTien = i * 1000,
                GhiChu = "Ghi chú " + i,
                rNhanVien = DDrNhanVien.Create(i),
                rLoaiChiPhi = DDrLoaiChiPhi.Create(i),
                rNhanVienList = DDrNhanVien.ViewModel.Entity.ToList(),
                rLoaiChiPhiList = DDrLoaiChiPhi.ViewModel.Entity.ToList()
            };
        }
    }
}

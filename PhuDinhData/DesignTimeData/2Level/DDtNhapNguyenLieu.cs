using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtNhapNguyenLieu
    {
        private static NhapNguyenLieuViewModel _viewModel;
        public static NhapNguyenLieuViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new NhapNguyenLieuViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tNhapNguyenLieu Create(int i)
        {
            return new tNhapNguyenLieu()
            {
                Ma = i,
                MaNhaCungCap = i,
                MaNguyenLieu = i,
                Ngay = System.DateTime.Now,
                rNguyenLieu= DDrNguyenLieu.Create(i),
                rNguyenLieuList = DDrNguyenLieu.ViewModel.Entity.ToList(),
                rNhaCungCap = DDrNhaCungCap.Create(i),
                rNhaCungCapList = DDrNhaCungCap.rNhaCungCaps
            };
        }
    }
}

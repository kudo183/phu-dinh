using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrNguyenLieu
    {
        private static NguyenLieuViewModel _viewModel;
        public static NguyenLieuViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new NguyenLieuViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rNguyenLieu Create(int i)
        {
            return new rNguyenLieu()
            {
                Ma = i,
                MaLoaiNguyenLieu = i,
                DuongKinh = i,
                rLoaiNguyenLieu = DDrLoaiNguyenLieu.Create(i),
                rLoaiNguyenLieuList = DDrLoaiNguyenLieu.rLoaiNguyenLieus
            };
        }
    }
}

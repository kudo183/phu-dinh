using PhuDinhDataEntity;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrLoaiNguyenLieu
    {
        private static LoaiNguyenLieuViewModel _viewModel;
        public static LoaiNguyenLieuViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new LoaiNguyenLieuViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rLoaiNguyenLieu Create(int i)
        {
            return new rLoaiNguyenLieu()
            {
                Ma = i,
                TenLoai = "Loại nguyên liệu " + i
            };
        }
    }
}

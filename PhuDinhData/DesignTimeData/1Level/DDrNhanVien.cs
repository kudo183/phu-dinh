using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrNhanVien
    {
        private static NhanVienModel _viewModel;
        public static NhanVienModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new NhanVienModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rNhanVien Create(int i)
        {
            return new rNhanVien()
            {
                Ma = i,
                MaPhuongTien = i,
                TenNhanVien = "Nhân viên " + i,
                rPhuongTien = DDrPhuongTien.Create(i),
                rPhuongTienList = DDrPhuongTien.rPhuongTiens
            };
        }
    }
}

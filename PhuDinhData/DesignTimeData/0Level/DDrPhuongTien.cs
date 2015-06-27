using PhuDinhDataEntity;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrPhuongTien
    {
        private static PhuongTienViewModel _viewModel;
        public static PhuongTienViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new PhuongTienViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rPhuongTien Create(int i)
        {
            return new rPhuongTien()
            {
                Ma = i,
                TenPhuongTien = "Phương tiện " + i
            };
        }
    }
}

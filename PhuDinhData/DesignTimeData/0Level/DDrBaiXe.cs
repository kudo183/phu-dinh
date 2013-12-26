using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrBaiXe
    {
        private static BaiXeViewModel _viewModel;
        public static BaiXeViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new BaiXeViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rBaiXe Create(int i)
        {
            return new rBaiXe()
            {
                Ma = i,
                DiaDiemBaiXe = "Bai xe " + i
            };
        }
    }
}

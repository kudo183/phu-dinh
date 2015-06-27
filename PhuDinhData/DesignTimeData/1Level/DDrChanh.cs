using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrChanh
    {
        private static ChanhViewModel _viewModel;
        public static ChanhViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChanhViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rChanh Create(int i)
        {
            return new rChanh()
            {
                Ma = i,
                MaBaiXe = i,
                TenChanh = "Chành " + i,
                rBaiXe = DDrBaiXe.Create(i),
                rBaiXeList = DDrBaiXe.ViewModel.Entity.ToList()
            };
        }
    }
}

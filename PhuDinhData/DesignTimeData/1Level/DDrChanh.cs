using System.Collections.ObjectModel;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrChanh
    {
        private static DGChanhViewModel _viewModel;
        public static DGChanhViewModel ViewModel
        {
            get
            {
                if(_viewModel!=null)
                {
                    return _viewModel;
                }
                
                _viewModel = new DGChanhViewModel();

                const int count = 10;
                var chanhs = new ObservableCollection<rChanh>();
                for (var i = 1; i <= count; i++)
                {
                    chanhs.Add(Create(i));
                }

                _viewModel.Entity = chanhs;

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
                rBaiXeList = DDrBaiXe.rBaiXes
            };
        }
    }
}

using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrDiaDiem
    {
        private static DiaDiemViewModel _viewModel;
        public static DiaDiemViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new DiaDiemViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rDiaDiem Create(int i)
        {
            return new rDiaDiem()
            {
                Ma = i,
                MaNuoc = i,
                Tinh = "Tỉnh " + i,
                rNuoc = DDrNuoc.Create(i),
                rNuocList = DDrNuoc.ViewModel.Entity.ToList()
            };
        }
    }
}

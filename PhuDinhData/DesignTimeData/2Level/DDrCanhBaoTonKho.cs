using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrCanhBaoTonKho
    {
        private static CanhBaoTonKhoViewModel _viewModel;
        public static CanhBaoTonKhoViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new CanhBaoTonKhoViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rCanhBaoTonKho Create(int i)
        {
            return new rCanhBaoTonKho()
            {
                Ma = i,
                MaMatHang = i,
                MaKhoHang = i,
                TonCaoNhat = i,
                TonThapNhat = i,
                tMatHang = DDtMatHang.Create(i),
                tMatHangList = DDtMatHang.ViewModel.Entity.ToList(),
                rKhoHang = DDrKhoHang.Create(i),
                rKhoHangList = DDrKhoHang.ViewModel.Entity.ToList()
            };
        }
    }
}

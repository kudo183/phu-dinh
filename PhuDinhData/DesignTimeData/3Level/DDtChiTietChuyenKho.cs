using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDtChiTietChuyenKho
    {
        private static ChiTietChuyenKhoViewModel _viewModel;
        public static ChiTietChuyenKhoViewModel ViewModel
        {
            get 
            {
                if(_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChiTietChuyenKhoViewModel();

                const int count = 10;
                
                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tChiTietChuyenKho Create(int i)
        {
            return new tChiTietChuyenKho()
            {
                Ma = i,
                MaChuyenKho = i,
                MaMatHang = i,
                SoLuong = i,
                tChuyenKho = DDtChuyenKho.Create(i),
                tChuyenKhoList = DDtChuyenKho.ViewModel.Entity.ToList(),
                tMatHang = DDtMatHang.Create(i),
                tMatHangList = DDtMatHang.ViewModel.Entity.ToList()
            };
        }
    }
}

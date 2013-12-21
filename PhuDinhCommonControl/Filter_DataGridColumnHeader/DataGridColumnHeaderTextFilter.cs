using System.ComponentModel;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    public class DataGridColumnHeaderTextFilter : INotifyPropertyChanged
    {
        //0Lvel
        //1level
        public static readonly DataGridColumnHeaderTextFilter Chanh_BaiXe = new DataGridColumnHeaderTextFilter("Bãi Xe");
        public static readonly DataGridColumnHeaderTextFilter DiaDiem_Nuoc = new DataGridColumnHeaderTextFilter("Nước");
        public static readonly DataGridColumnHeaderTextFilter NguyenLieu_LoaiNguyenLieu = new DataGridColumnHeaderTextFilter("Loại Nguyên Liệu");
        public static readonly DataGridColumnHeaderTextFilter NhanVien_PhuongTien = new DataGridColumnHeaderTextFilter("Phương Tiện");
        public static readonly DataGridColumnHeaderTextFilter MatHang_LoaiHang = new DataGridColumnHeaderTextFilter("Loại Hàng");
        //2Lvel
        //3Level
        public static readonly DataGridColumnHeaderTextFilter DonHang_KhachHang = new DataGridColumnHeaderTextFilter(Constant.ViewName_KhachHang);
        public static readonly DataGridColumnHeaderTextFilter DonHang_KhachHangChanh = new DataGridColumnHeaderTextFilter(Constant.ViewName_KhachHangChanh);
        public static readonly DataGridColumnHeaderTextFilter KhachHangChanh_KhachHang = new DataGridColumnHeaderTextFilter(Constant.ViewName_KhachHang);
        public static readonly DataGridColumnHeaderTextFilter KhachHangChanh_Chanh = new DataGridColumnHeaderTextFilter(Constant.ViewName_Chanh);

        private DataGridColumnHeaderTextFilter(string name)
        {
            _name = name;
        }

        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                {
                    return;
                }

                _text = value;
                OnPropertyChanged("Text");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

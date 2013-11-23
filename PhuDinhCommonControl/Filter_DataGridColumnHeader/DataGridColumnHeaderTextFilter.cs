using System.ComponentModel;

namespace PhuDinhCommonControl
{
    public class DataGridColumnHeaderTextFilter : INotifyPropertyChanged
    {
        public static readonly DataGridColumnHeaderTextFilter DonHang_KhachHang = new DataGridColumnHeaderTextFilter("Khách Hàng");
        public static readonly DataGridColumnHeaderTextFilter DonHang_Chanh = new DataGridColumnHeaderTextFilter("Chành");

        //1level
        public static readonly DataGridColumnHeaderTextFilter Chanh_BaiXe = new DataGridColumnHeaderTextFilter("Bãi Xe");
        public static readonly DataGridColumnHeaderTextFilter DiaDiem_Nuoc = new DataGridColumnHeaderTextFilter("Nước");
        public static readonly DataGridColumnHeaderTextFilter NguyenLieu_LoaiNguyenLieu = new DataGridColumnHeaderTextFilter("Loại Nguyên Liệu");
        public static readonly DataGridColumnHeaderTextFilter NhanVien_PhuongTien = new DataGridColumnHeaderTextFilter("Phương Tiện");
        public static readonly DataGridColumnHeaderTextFilter MatHang_LoaiHang = new DataGridColumnHeaderTextFilter("Loại Hàng");

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

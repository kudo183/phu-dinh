using System.ComponentModel;

namespace PhuDinhCommonControl
{
    public class DataGridColumnHeaderTextFilter : INotifyPropertyChanged
    {
        public static readonly DataGridColumnHeaderTextFilter DonHang_KhachHang = new DataGridColumnHeaderTextFilter("Khách Hàng");
        public static readonly DataGridColumnHeaderTextFilter DonHang_Chanh = new DataGridColumnHeaderTextFilter("Chành");
        public static readonly DataGridColumnHeaderTextFilter Chanh_BaiXe = new DataGridColumnHeaderTextFilter("Bãi Xe");

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

using System;
using System.ComponentModel;

namespace PhuDinhCommonControl
{
    public class DataGridColumnHeaderDateFilter : INotifyPropertyChanged
    {
        public static readonly DataGridColumnHeaderDateFilter DonHang = new DataGridColumnHeaderDateFilter("Ngày");
        public static readonly DataGridColumnHeaderDateFilter ChuyenHang = new DataGridColumnHeaderDateFilter("Ngày");
        public static readonly DataGridColumnHeaderDateFilter ChiPhi = new DataGridColumnHeaderDateFilter("Ngày");

        private DataGridColumnHeaderDateFilter(string name)
        {
            _name = name;
        }

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date == value)
                {
                    return;
                }

                _date = value;
                OnPropertyChanged("Date");
            }
        }

        private bool _isUsed = true;
        public bool IsUsed
        {
            get { return _isUsed; }
            set
            {
                if (_isUsed == value)
                {
                    return;
                }

                _isUsed = value;
                OnPropertyChanged("IsUsed");
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

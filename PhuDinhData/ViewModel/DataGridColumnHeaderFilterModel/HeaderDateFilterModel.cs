using System;

namespace PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel
{
    public class HeaderDateFilterModel : HeaderFilterBaseModel
    {
        public HeaderDateFilterModel(string name) : base(name) { }

        private DateTime _date = DateTime.Now.Date;
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
                RaisePropertyChanged("Date");
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
                RaisePropertyChanged("IsUsed");
            }
        }
    }
}

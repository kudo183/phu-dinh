using System;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    public class BaseView : UserControl
    {
        public event EventHandler AfterSave;
        public event EventHandler AfterCancel;

        public virtual void RefreshView()
        {
            
        }

        public virtual void Save()
        {
            if (AfterSave != null)
            {
                AfterSave(this, null);
            }
        }

        public virtual void Cancel()
        {
            if (AfterCancel != null)
            {
                AfterCancel(this, null);
            }
        }

        protected void bmMenu_Click(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            if (button.Name == "btnSave")
            {
                Save();
            }
            else if (button.Name == "btnCancel")
            {
                Cancel();
            }
        }
    }
}

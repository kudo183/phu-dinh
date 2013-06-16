using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    public class BaseView : UserControl
    {
        public virtual void RefreshView()
        {
            
        }

        public virtual void Save()
        {
            
        }

        public virtual void Cancel()
        {
            
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

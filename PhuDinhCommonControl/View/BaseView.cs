using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhuDinhCommonControl
{
    public class BaseView<T> : UserControl where T : class
    {
        public event EventHandler AfterSave;
        public event EventHandler AfterCancel;

        public BaseView()
        {
            PreviewKeyDown += BaseView_PreviewKeyDown;
        }

        void BaseView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.S:
                        Save();
                        break;
                    case Key.X:
                        Cancel();
                        break;
                }
            }
        }

        public virtual void CommitEdit()
        {

        }

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

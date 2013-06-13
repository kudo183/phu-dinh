using System.Windows.Controls;

namespace PhuDinhCommonControl.CustomControl
{
    public class TextBoxExt:TextBox
    {
        protected override void OnGotKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            SelectAll();
        }
    }
}

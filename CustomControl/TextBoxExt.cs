using System.Windows.Controls;
using System.Windows.Input;

namespace CustomControl
{
    public class TextBoxExt : TextBox
    {
        public bool IsUseEnterKeyPressBinding { get; set; }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            SelectAll();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (IsUseEnterKeyPressBinding == true && e.Key == Key.Return)
            {
                var exp = GetBindingExpression(TextBox.TextProperty);

                exp.UpdateSource();
            }
        }
    }
}

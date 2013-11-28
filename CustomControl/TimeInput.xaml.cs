using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomControl
{
    /// <summary>
    /// Interaction logic for TimeInput.xaml
    /// </summary>
    public partial class TimeInput : UserControl
    {
        public TimeInput()
        {
            InitializeComponent();
        }

        public TimeSpan Value
        {
            get { return (TimeSpan)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(TimeSpan), typeof(TimeInput),
            new FrameworkPropertyMetadata(new TimeSpan(0, 0, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var timeInput = obj as TimeInput;
            var time = (TimeSpan)e.NewValue;
            timeInput.txtHour.Text = time.Hours.ToString();
            timeInput.txtMinute.Text = time.Minutes.ToString();            
        }

        private void txtHour_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int value;
            var text = txtHour.Text.Remove(txtHour.SelectionStart, txtHour.SelectionLength);
            text = txtHour.SelectionStart == 0 ? e.Text + text : text + e.Text;

            if (int.TryParse(text, out value) == false)
            {
                e.Handled = true;
                return;
            }

            if (value < 0 || value > 23)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtMinute_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int value;
            var text = txtMinute.Text.Remove(txtMinute.SelectionStart, txtMinute.SelectionLength);
            text = txtMinute.SelectionStart == 0 ? e.Text + text : text + e.Text;

            if (int.TryParse(text, out value) == false)
            {
                e.Handled = true;
                return;
            }

            if (value < 0 || value > 59)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtHour_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            if (int.TryParse(txtHour.Text, out value) == false)
            {
                return;
            }
            Value = new TimeSpan(value, Value.Minutes, 0);
        }

        private void txtMinute_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            if (int.TryParse(txtMinute.Text, out value) == false)
            {
                return;
            }
            Value = new TimeSpan(Value.Hours, value, 0);
        }
    }
}

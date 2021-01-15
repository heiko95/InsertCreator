using System.Windows.Controls;

namespace HgSoftware.InsertCreator.Extensions
{
    internal class AutoEditingComboBox : ComboBox
    {
        private TextBox _textBox;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textBox = Template.FindName("PART_EditableTextBox", this) as TextBox;
            if (_textBox != null)
            {
                _textBox.GotKeyboardFocus += _textBox_GotFocus;
                this.Unloaded += MyComboBox_Unloaded;
            }
        }

        private void MyComboBox_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _textBox.GotKeyboardFocus -= _textBox_GotFocus;
            this.Unloaded -= MyComboBox_Unloaded;
        }

        private void _textBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            _textBox.Select(_textBox.Text.Length, 0); // set caret to end of text
        }
    }
}
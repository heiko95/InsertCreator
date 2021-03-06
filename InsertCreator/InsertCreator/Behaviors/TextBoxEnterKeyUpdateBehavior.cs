﻿using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows.Input;

namespace HgSoftware.InsertCreator.Behaviors
{
    public class TextBoxEnterKeyUpdateBehavior : Behavior<TextBox>
    {
        #region Protected Methods

        protected override void OnAttached()
        {
            if (this.AssociatedObject != null)
            {
                base.OnAttached();
                this.AssociatedObject.KeyDown += AssociatedObject_KeyDown;
            }
        }

        protected override void OnDetaching()
        {
            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.KeyDown -= AssociatedObject_KeyDown;
                base.OnDetaching();
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void AssociatedObject_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && e.Key == Key.Return && e.Key == Key.Enter)
            {
                // Kill logical focus
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(textBox), null);
                // Kill keyboard focus
                Keyboard.ClearFocus();
            }
        }

        #endregion Private Methods
    }
}
using HgSoftware.InsertCreator.View;
using HgSoftware.InsertCreator.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace HgSoftware.InsertCreator.Model
{
    internal class PreviewWindowController
    {
        #region Private Fields

        private readonly PreviewViewModel _previewViewModel;
        private readonly PreView _window = new PreView();

        #endregion Private Fields

        #region Public Constructors

        public PreviewWindowController(PreviewViewModel vm)
        {
            _previewViewModel = vm;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Show()
        {
            if (SetWindow())
            {
                _window.Show();
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal void Close()
        {
            _window.Close();
        }

        internal void Update(bool state)
        {
            if (state && SetWindow())
            {
                _window.Show();
                return;
            }

            _window.Hide();
        }

        #endregion Internal Methods

        #region Private Methods

        private int CheckMonitor(int number, List<System.Windows.Forms.Screen> screens)
        {
            if (screens.Count >= number)
            {
                return number;
            }
            return CheckMonitor(number - 1, screens);
        }

        private bool SetWindow()
        {
            var screens = System.Windows.Forms.Screen.AllScreens.ToList();

            if (screens.Count < 2)
                return false;

            _window.DataContext = _previewViewModel;
            _window.ShowInTaskbar = false;
            _window.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;

            //var secScreen = screens.First(x => !x.Primary);

            var secScreen = screens[CheckMonitor(Properties.Settings.Default.MonitorNumber, screens) - 1];

            System.Drawing.Rectangle r = secScreen.WorkingArea;

            _window.Top = r.Top;
            _window.Left = r.Left;
            return true;
        }

        #endregion Private Methods
    }
}
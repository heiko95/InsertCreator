using HgSoftware.InsertCreator.View;
using HgSoftware.InsertCreator.ViewModel;
using System;
using System.Drawing;
using System.Linq;

namespace HgSoftware.InsertCreator.Model
{
    internal class PreviewWindowController
    {
        #region Private Fields

        private readonly PreviewViewModel _previewViewModel;
        private readonly PreView _window = new PreView();
        private int _selectedMonitorIndex = 1;

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

        public void SetSelectedMonitor(int monitorIndex)
        {
            _selectedMonitorIndex = monitorIndex;
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

        private bool SetWindow()
        {
            var screens = System.Windows.Forms.Screen.AllScreens.ToList();

            if (screens.Count < 2)
                return false;

            _window.DataContext = _previewViewModel;
            _window.ShowInTaskbar = false;
            _window.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;

            var screen = _selectedMonitorIndex >= 0 && _selectedMonitorIndex < screens.Count
                ? screens[_selectedMonitorIndex]
                : screens.FirstOrDefault(x => !x.Primary) ?? screens[1];
            System.Drawing.Rectangle r = screen.WorkingArea;

            // Screen.WorkingArea is reported in physical pixels, while WPF window
            // coordinates (Top/Left/Width/Height) are device-independent units
            // (1/96"). The app is not per-monitor DPI aware, so a single system
            // DPI scale applies; convert the rectangle with it so placement and
            // sizing stay correct when display scaling is not 100%.
            double scaleX, scaleY;
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                scaleX = g.DpiX / 96.0;
                scaleY = g.DpiY / 96.0;
            }

            // Stay in Normal state and size the (borderless) window to exactly
            // cover the target monitor instead of using WindowState.Maximized.
            // Setting WindowState.Maximized before the window has ever been
            // shown is a well-known WPF issue: Windows computes the maximized
            // bounds from the monitor the HWND is created on (usually the
            // primary monitor), not from Left/Top, so the window would first
            // appear on the wrong monitor regardless of the position we set here.
            _window.WindowState = System.Windows.WindowState.Normal;
            _window.Top = r.Top / scaleY;
            _window.Left = r.Left / scaleX;
            _window.Width = r.Width / scaleX;
            _window.Height = r.Height / scaleY;
            return true;
        }

        #endregion Private Methods
    }
}

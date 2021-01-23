using HgSoftware.InsertCreator.View;
using HgSoftware.InsertCreator.ViewModel;
using System.Linq;

namespace HgSoftware.InsertCreator.Model
{
    internal class PreviewWindowController
    {
        private readonly PreView _window = new PreView();
        private readonly PreviewViewModel _previewViewModel;

        public PreviewWindowController(PreviewViewModel vm)
        {
            _previewViewModel = vm;
        }

        private bool SetWindow()
        {
            var screens = System.Windows.Forms.Screen.AllScreens.ToList();

            if (screens.Count < 2)
                return false;

            _window.DataContext = _previewViewModel;
            _window.ShowInTaskbar = false;
            _window.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;

            var secScreen = screens.First(x => x.Primary == false);
            //var secScreen = screens[0];
            System.Drawing.Rectangle r = secScreen.WorkingArea;

            _window.Top = r.Top;
            _window.Left = r.Left;
            return true;
        }

        public void Show()
        {
            if (SetWindow())
            {
                _window.Show();
            }
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

        internal void Close()
        {
            _window.Close();
        }
    }
}
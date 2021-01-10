using Liedeinblendung.Extensions;
using Liedeinblendung.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Liedeinblendung.ViewModel
{
    public class WindowViewModel : ObservableObject
    {


        public WindowViewModel()
        {
            _gbData = new MainViewModel(_hymnalJsonReader.LoadHymnalData(($"{Directory.GetCurrentDirectory()}/DataSource/GB_Data.json")), "Gesangbuch");
            _cbData = new MainViewModel(_hymnalJsonReader.LoadHymnalData(($"{Directory.GetCurrentDirectory()}/DataSource/CB_Data.json")), "Chorbuch");
            HymnalInputVisible = true;
            MinistryViewModel = new MinistryViewModel();

            CurrentHymnalViewModel = _gbData;
        }

        public ICommand OnShowConfig => new RelayCommand(OpenConfigDialog);
        public ICommand OnShowInfo => new RelayCommand(OpenInfoDialog);

        async private void OpenInfoDialog(object obj)
        {
            await MaterialDesignThemes.Wpf.DialogHost.Show(InfoViewModel, "MainWindow");
        }

        async private void OpenConfigDialog(object obj)
        {
            await MaterialDesignThemes.Wpf.DialogHost.Show(ConfigViewModel, "MainWindow");
        }

        public int Selected
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);

                if (value == 2)
                {
                    HymnalInputVisible = false;
                    return;
                }

                HymnalInputVisible = true;
                if (value == 1)
                {
                    CurrentHymnalViewModel = _cbData;
                    return;
                }
                CurrentHymnalViewModel = _gbData;
            }
        }

        public bool HymnalInputVisible
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        

        private readonly MainViewModel _gbData;
        private readonly MainViewModel _cbData;
        private readonly HymnalJsonReader _hymnalJsonReader = new HymnalJsonReader();


        public ConfigViewModel ConfigViewModel = new ConfigViewModel();

        public InfoViewModel InfoViewModel = new InfoViewModel();

        public MinistryViewModel MinistryViewModel
        {
            get { return GetValue<MinistryViewModel>(); }
            set { SetValue(value); }
        }


        public MainViewModel CurrentHymnalViewModel
        {
            get { return GetValue<MainViewModel>(); }
            set { SetValue(value); }
        }









    }
}

﻿using System.Windows;
using System.Windows.Controls;
using Liedeinblendung.ViewModel;

namespace Liedeinblendung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(WindowViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private object _selected;

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List.SelectedItem != null)
            {
                _selected = List.SelectedItem;
                return;
            }
            List.SelectedItem = _selected;
        }
    }
}
using Avalonia.Controls;
using Avalonia.Interactivity;
using LoopCommandSharp.ViewModels;
using System;

namespace LoopCommandSharp.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

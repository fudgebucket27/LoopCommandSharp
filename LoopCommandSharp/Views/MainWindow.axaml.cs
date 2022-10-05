using Avalonia.Controls;
using Avalonia.Interactivity;
using LoopCommandSharp.Models;
using LoopCommandSharp.ViewModels;
using Microsoft.Extensions.Configuration;

namespace LoopCommandSharp.Views
{
    public partial class MainWindow : Window
    {
        public Settings? settings = null;
        public MainWindow()
        {
            InitializeComponent();
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
            settings = config.GetRequiredSection("Settings").Get<Settings>();
        }

        void ShowMintNftWindow(object sender, RoutedEventArgs e)
        {
            var window = new MintNftWindow()
            {
                DataContext = new MintNftWindowViewModel(settings)
            };
            window.ShowDialog(this);
        }

        void ShowSettingsWindow(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow()
            {
                DataContext = new SettingsWindowViewModel(settings)
            };
            window.ShowDialog(this);
        }
    }
}

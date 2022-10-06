using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using LoopCommandSharp.Models;
using LoopCommandSharp.ViewModels;
using Microsoft.Extensions.Configuration;
using ReactiveUI;

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
            if(string.IsNullOrEmpty(settings.LoopringApiKey))
            {
                var button = this.FindControl<Button>("Mint").IsEnabled = false;
            }
            else
            {
                var button = this.FindControl<Button>("Mint").IsEnabled = true;
            }
            
        }

        void ShowCreateNftCollectionWindow(object sender, RoutedEventArgs e)
        {
            var window = new CreateNftCollectionWindow()
            {
                DataContext = new CreateNftCollectionWindowViewModel(settings)
            };
            window.ShowDialog(this);
        }

        void ShowMintNftWindow(object sender, RoutedEventArgs e)
        {
            var window = new MintNftWindow()
            {
                DataContext = new MintNftWindowViewModel(settings)
            };
            window.ShowDialog(this);

        }


        private async void ShowSettingsWindow(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow()
            {
                DataContext = new SettingsWindowViewModel(settings)
            };
            var result = await window.ShowDialog<Settings>(this);

            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
            settings = config.GetRequiredSection("Settings").Get<Settings>();
            if (string.IsNullOrEmpty(settings.LoopringApiKey) || string.IsNullOrEmpty(settings.LoopringPrivateKey) || string.IsNullOrEmpty(settings.LoopringAddress) || settings.LoopringAccountId == null)
            {
                var button = this.FindControl<Button>("Mint").IsEnabled = false;
            }
            else
            {
                var button = this.FindControl<Button>("Mint").IsEnabled = true;
            }
        }
    }
}

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LoopCommandSharp.Models;
using LoopCommandSharp.ViewModels;
using LoopCommandSharp.Views;
using Microsoft.Extensions.Configuration;

namespace LoopCommandSharp
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
            Settings? settings = config.GetRequiredSection("Settings").Get<Settings>();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if( string.IsNullOrEmpty(settings.LoopringApiKey) || string.IsNullOrEmpty(settings.LoopringPrivateKey) || string.IsNullOrEmpty(settings.LoopringAddress) || settings.LoopringAccountId == 0)
                {
                    desktop.MainWindow= new SettingsWindow()
                    {
                        DataContext = new SettingsWindowViewModel(settings),
                    };
                }
                else
                {
                    desktop.MainWindow = new MainWindow
                    {
                        DataContext = new MainWindowViewModel(),
                    };
                }
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}

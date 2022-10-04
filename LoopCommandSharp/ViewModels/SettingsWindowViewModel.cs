using LoopCommandSharp.Models;
using LoopCommandSharp.Views;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace LoopCommandSharp.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public Settings? Settings { get; set; }

        string status = "";

        public string Status
        {
            get => status;
            set
            {
                status = value;
                LogPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
            }
        }

        public event PropertyChangedEventHandler LogPropertyChanged;

        public ReactiveCommand<Unit, Unit> UpdateSettings { get; }

        public SettingsWindowViewModel(Settings settings)
        {
            this.Settings = settings;
            UpdateSettings = ReactiveCommand.Create(Update);
        }

        void Update()
        {
            AppSettings appSettings = new AppSettings();
            appSettings.Settings = Settings;
            File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(appSettings, Formatting.Indented));
            Status = "Updated settings!";
        }

    }
}

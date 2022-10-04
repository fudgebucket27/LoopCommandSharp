using Avalonia.Controls;
using LoopCommandSharp.Models;
using LoopMintSharp;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoopCommandSharp.ViewModels 
{
    public class MintNftWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public LoopringService LoopringService { get; set; }
        public Settings Settings { get; set; }

        public ObservableCollection<string> Collections { get; set; }

        public bool IsMinting { get; set; } = false;

        public bool IsEnabled { get; set; } = true;

        public string Cids { get; set; }

        public string log { get; set; }

        public string Log
        {
            get => log;
            set
            {
                log = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Log)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ReactiveCommand<Unit, Unit> MintNft { get; }

        public MintNftWindowViewModel(Settings settings)
        {
            Settings = settings;
            Collections = new ObservableCollection<string>();
            LoopringService = new LoopringService();
            RxApp.MainThreadScheduler.Schedule(LoadCollections);
            MintNft = ReactiveCommand.Create(Mint);

        }

        private async void LoadCollections()
        {
            var loopringCollections = await LoopringService.FindNftCollection(Settings.LoopringApiKey, 50, 0, Settings.LoopringAddress, "", false);
            foreach(var collection in loopringCollections.collections)
            {
                Collections.Add($"{collection.collection.name},{collection.collection.contractAddress}");
            }
        }

        void Mint()
        {
            Log = "";
            IsMinting = true;
            IsEnabled = false;
            if (!string.IsNullOrEmpty(Cids))
            {
                using (StringReader reader = new StringReader(Cids))
                {
                    string line = string.Empty;
                    int count = 1;
                    do
                    {
                        line = reader.ReadLine();
                        if (line != null)
                        {
                            Log += $"Minted {count}" + Environment.NewLine;
                            count++;
                        }


                    } while (line != null);
                }
            }
            else
            {
                Log = "CIDS empty.";
            }
        }
    }
}

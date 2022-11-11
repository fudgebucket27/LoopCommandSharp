using LoopCommandSharp.Models;
using LoopMintSharp;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;

namespace LoopCommandSharp.ViewModels
{
    public class AirdropNftWindowViewModel :  ViewModelBase, INotifyPropertyChanged
    {
        public LoopringServices LoopringServices { get; set; }
        public Settings Settings { get; set; }

        public string loadingText { get; set; } = "Loading NFTs...";

        public string LoadingText
        {
            get => loadingText;
            set
            {
                loadingText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadingText)));
            }
        }

        public bool isEnabled { get; set; } = true;

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public int TransferAmount { get; set; }

        public string TransferMemo { get; set; }

        public ObservableCollection<Datum> NFTs { get; set; }

        public Datum selectedNFT { get; set; }

        public Datum SelectedNFT
        {
            get => selectedNFT;
            set
            {
                selectedNFT = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedNFT)));
            }
        }

        public int caretIndex { get; set; } = 0;
        public int CaretIndex
        {
            get => caretIndex;
            set
            {
                caretIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CaretIndex)));
            }
        }

        public string addresses { get; set; }


        public string Addresses
        {
            get => addresses;
            set
            {
                addresses = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Addresses)));
            }
        }

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

        public AirdropNftWindowViewModel(Settings settings)
        {
            Settings = settings;
            NFTs = new ObservableCollection<Datum>();
            LoopringServices = new LoopringServices();
            RxApp.MainThreadScheduler.Schedule(LoadNFTs);
        }

        private async void LoadNFTs()
        {
            var loopringNFTs = await LoopringServices.GetNFTs(Settings.LoopringApiKey, Settings.LoopringAccountId);
            if (loopringNFTs != null)
            {
                foreach (var nft in loopringNFTs)
                {
                    NFTs.Add(nft);
                }
            }
            LoadingText = "Choose an NFT...";
        }
    }
}

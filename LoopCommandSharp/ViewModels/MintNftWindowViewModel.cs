using Avalonia.Controls;
using Avalonia.Threading;
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
        public LoopringServices LoopringServices { get; set; }
        public Settings Settings { get; set; }

        public ObservableCollection<Collection> Collections { get; set; }

        public bool IsMinting { get; set; } = false;

        public bool IsEnabled { get; set; } = true;

        public string Cids { get; set; }

        public string log { get; set; }

        public int RoyaltyPercentage { get; set; } = 0;

        public int EditionsPerMint { get; set; } = 1;

        public string Log
        {
            get => log;
            set
            {
                log = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Log)));
            }
        }

        public Collection selectedCollection { get; set; }

        public Collection SelectedCollection
        {
            get => selectedCollection;
            set
            {
                selectedCollection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCollection)));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public ReactiveCommand<Unit, Unit> MintNft { get; }

        public MintNftWindowViewModel(Settings settings)
        {
            Settings = settings;
            Collections = new ObservableCollection<Collection>();
            LoopringServices = new LoopringServices();
            RxApp.MainThreadScheduler.Schedule(LoadCollections);
            MintNft = ReactiveCommand.Create(Mint);

        }

        private async void LoadCollections()
        {
            var loopringCollections = await LoopringServices.FindNftCollection(Settings.LoopringApiKey, 50, 0, Settings.LoopringAddress, "", false);
            foreach(var collection in loopringCollections.collections)
            {
                Collections.Add(collection.collection); 
            }
        }

        private async void Mint()
        {
            Log = "";
            IsMinting = true;
            IsEnabled = false;
            int lineCount = 0;
            if (!string.IsNullOrEmpty(Cids) && SelectedCollection != null)
            {
                //Calculate lines
                using (StringReader reader = new StringReader(Cids))
                {
                    string line = string.Empty;
                    do
                    {
                        line = reader.ReadLine();
                        if (line != null)
                        {
                            lineCount++;
                        }
                    } while (line != null);
                }

                //Loop through cids and mint!
                using (StringReader reader = new StringReader(Cids))
                {
                    string line = string.Empty;
                    int count = 1;
                    do
                    {
                        line = reader.ReadLine();
                        if (line != null)
                        {
                            string cid = line.Trim();
                            var mintResponse = await LoopringServices.MintCollection(Settings.LoopringApiKey, Settings.LoopringPrivateKey, Settings.LoopringAddress, Settings.LoopringAccountId, Settings.LoopringAccountId, RoyaltyPercentage, EditionsPerMint, Settings.ValidUntil, Settings.MaxFeeTokenId, Settings.NftFactoryCollection, Settings.Exchange, cid, false, SelectedCollection.baseUri, SelectedCollection.contractAddress);
                            if (!string.IsNullOrEmpty(mintResponse.errorMessage))
                            {
                                Log += $"Mint {count} out of {lineCount} NFTs was UNSUCCESSFUL. ERROR MESSAGE: {mintResponse.errorMessage}";
                            }
                            else
                            {
                                Log += $"Mint {count} out of {lineCount} NFTs was SUCCESSFUL";
                            }
                            count++;
                        }


                    } while (line != null);
                }
            }
            else if (string.IsNullOrEmpty(Cids))
            {
                Log = "CIDS empty!";
            }
            else if (SelectedCollection == null)
            {
                Log = "Choose a collection!";
            }
        }
    }
}

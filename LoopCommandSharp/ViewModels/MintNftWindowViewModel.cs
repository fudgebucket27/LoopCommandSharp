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
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoopCommandSharp.ViewModels 
{
    public class MintNftWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public Interaction<MintFeeWindowViewModel, MintFeeResultWindowViewModel> ShowDialog { get; }
        public LoopringServices LoopringServices { get; set; }
        public Settings Settings { get; set; }

        public ObservableCollection<Collection> Collections { get; set; }

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

        public string cids { get; set; }


        public string Cids
        {
            get => cids;
            set
            {
                cids = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cids)));
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

        public int RoyaltyPercentage { get; set; } 

        public int EditionsPerMint { get; set; } = 1;

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

        public string mintFee { get; set; } = "";

        public string MintFee

        {
            get => mintFee;
            set
            {
                mintFee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MintFee)));
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
            ShowDialog = new Interaction<MintFeeWindowViewModel, MintFeeResultWindowViewModel>();
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
            IsEnabled = false;
            int lineCount = 0;
            if (!string.IsNullOrEmpty(Cids) && SelectedCollection != null)
            {
                var mintFeeStatus = "";
                var offChainFee = await LoopringServices.GetMintFee(Settings.LoopringApiKey, Settings.LoopringAccountId, Settings.LoopringAddress, Settings.NftFactoryCollection, false, SelectedCollection.baseUri);
                var fee = offChainFee.fees[Settings.MaxFeeTokenId].fee;
                double feeAmount = lineCount * Double.Parse(fee);
                if (Settings.MaxFeeTokenId == 0)
                {
                    mintFeeStatus = $"It will cost around {TokenAmountConverter.ToString(feeAmount, 18)} ETH to mint {lineCount} NFTs";
                }
                else if (Settings.MaxFeeTokenId == 1)
                {
                    mintFeeStatus  = $"It will cost around {TokenAmountConverter.ToString(feeAmount, 18)} LRC to mint {lineCount} NFTs";
                }
                var dialog = new MintFeeWindowViewModel();
                dialog.MintFeeStatus = mintFeeStatus;
                var result = await ShowDialog.Handle(dialog);
                if(result == null || result.ConfirmMintFee == false)
                {
                    Log = "Minting cancelled. Mint fee was not confirmed!";
                    IsEnabled = true;
                    return;
                }
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
                            CaretIndex = Log.Length;
                            string cid = line.Trim();
                            if(!cids.StartsWith("Qm") || cid.Length != 46)
                            {
                                Log += $"Mint {count} out of {lineCount}NFTs was UNSUCCESSFUL. ERROR MESSAGE: {cid} is not a valid CID!" + Environment.NewLine;
                                count++;
                                continue;
                            }
                            var mintResponse = await LoopringServices.MintCollection(Settings.LoopringApiKey, Settings.LoopringPrivateKey, Settings.LoopringAddress, Settings.LoopringAccountId, 2,RoyaltyPercentage, EditionsPerMint, Settings.ValidUntil, Settings.MaxFeeTokenId, Settings.NftFactoryCollection, Settings.Exchange, cid, false, SelectedCollection.baseUri, SelectedCollection.contractAddress);
                            if (!string.IsNullOrEmpty(mintResponse.errorMessage))
                            {
                                Log += $"Mint {count} out of {lineCount} NFTs was UNSUCCESSFUL. ERROR MESSAGE: {mintResponse.errorMessage}" + Environment.NewLine;
                            }
                            else
                            {
                                Log += $"Mint {count} out of {lineCount} NFTs was SUCCESSFUL" + Environment.NewLine;
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
            IsEnabled = true;
        }
    }
}

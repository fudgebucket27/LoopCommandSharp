using LoopCommandSharp.Models;
using LoopMintSharp;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace LoopCommandSharp.ViewModels
{
    public class CreateNftCollectionWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public Settings? Settings { get; set; }

        public LoopringServices LoopringServices { get; set; }

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

        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Avatar { get; set; } = "";

        public string Banner { get; set; } = "";

        public string TileUri { get; set; } = "";

        public string status { get; set; }

        public string Status
        {
            get => status;
            set
            {
                status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public ReactiveCommand<Unit, Unit> CreateNftCollection { get; }

        public CreateNftCollectionWindowViewModel(Settings settings)
        {
            Settings = settings;
            LoopringServices = new LoopringServices();
            CreateNftCollection = ReactiveCommand.Create(CreateCollection);
        }

        private async void CreateCollection()
        {
            Status = "";
            IsEnabled = false;

            var avatarLink = "";
            var bannerLink = "";
            var tileUriLink = "";

            if(string.IsNullOrEmpty(Name.Trim()))
            {
                Status = "Collection name mandatory!";
                IsEnabled = true;
                return;
            }

            if(string.IsNullOrEmpty(Description.Trim()))
            {
                Status = "Collection description mandatory!";
                IsEnabled = true;
                return;
            }

            if (string.IsNullOrEmpty(Avatar.Trim()))
            {
                Status = "Collection avatar image link mandatory!";
                IsEnabled = true;
                return;
            }

            if (string.IsNullOrEmpty(Banner.Trim()))
            {
                Status = "Collection banner image link mandatory!";
                IsEnabled = true;
                return;
            }

            if (string.IsNullOrEmpty(TileUri.Trim()))
            {
                Status = "Collection tileUri image link mandatory!";
                IsEnabled = true;
                return;
            }

            if(Avatar.Trim().StartsWith("Qm"))
            {
                avatarLink = $"ipfs://{Avatar.Trim()}";
            }
            else if(Avatar.Trim().StartsWith("https://"))
            {
                avatarLink = Avatar.Trim();
            }
            else if (Avatar.Trim().StartsWith("http://"))
            {
                avatarLink = Avatar.Trim();
            }
            else
            {
                Status = "Collection avatar image link needs to be a valid ipfs or https link";
                IsEnabled = true;
                return;
            }

            if (Banner.Trim().StartsWith("Qm"))
            {
                bannerLink = $"ipfs://{Banner.Trim()}";
            }
            else if (Banner.Trim().StartsWith("https://"))
            {
                bannerLink = Banner.Trim();
            }
            else if (Banner.Trim().StartsWith("http://"))
            {
                bannerLink = Banner.Trim();
            }
            else
            {
                Status = "Collection banner image link needs to be a valid ipfs or https link";
                IsEnabled = true;
                return;
            }

            if (TileUri.Trim().StartsWith("Qm"))
            {
                tileUriLink = $"ipfs://{TileUri.Trim()}";
            }
            else if (TileUri.Trim().StartsWith("https://"))
            {
                tileUriLink = TileUri.Trim();
            }
            else if (TileUri.Trim().StartsWith("http://"))
            {
                tileUriLink = TileUri.Trim();
            }
            else
            {
                Status = "Collection tileUri image link needs to be a valid ipfs or https link";
                IsEnabled = true;
                return;
            }

            var collectionResult = await LoopringServices.CreateNftCollection(
                Settings.LoopringApiKey,
                avatarLink,
                bannerLink,
                Description,
                Name,
                Settings.NftFactoryCollection,
                Settings.LoopringAddress,
                tileUriLink,
                Settings.LoopringPrivateKey,
                false
                );
            if(collectionResult != null && !string.IsNullOrEmpty(collectionResult.contractAddress))
            {
                if(collectionResult.contractAddress.StartsWith("0x"))
                {
                    Status = $"Collection created with contract address: {collectionResult.contractAddress}";
                }
                else
                {
                    Status = $"Collection creation failed with ERROR MESSAGE: {collectionResult.contractAddress}";
                }
            }
            IsEnabled = true;
        }
    }
}

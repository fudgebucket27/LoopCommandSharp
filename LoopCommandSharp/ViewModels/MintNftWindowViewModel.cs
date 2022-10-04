using LoopCommandSharp.Models;
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
    public class MintNftWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public Settings Settings { get; set; }

        public List<string> Collections { get; set; }

        public bool IsMinting { get; set; } = false;

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
            Collections = new List<string>();
            Collections.Add("BLAH");
            Collections.Add("Bro");
            MintNft = ReactiveCommand.Create(Mint);
        }

        void Mint()
        {
            Log = "";
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

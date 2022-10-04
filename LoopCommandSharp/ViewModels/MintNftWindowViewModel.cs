using LoopCommandSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopCommandSharp.ViewModels 
{
    public class MintNftWindowViewModel : ViewModelBase
    {
        public Settings Settings { get; set; }

        public MintNftWindowViewModel(Settings settings)
        {
            Settings = settings;
        }
    }
}

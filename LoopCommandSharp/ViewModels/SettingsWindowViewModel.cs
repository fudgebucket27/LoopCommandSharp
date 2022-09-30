using LoopCommandSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopCommandSharp.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        public Settings? Settings { get;set;} 

        public SettingsWindowViewModel(Settings settings)
        {
            this.Settings = settings;
        }
    }
}

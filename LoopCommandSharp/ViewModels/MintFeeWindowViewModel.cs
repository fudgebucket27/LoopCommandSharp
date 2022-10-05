using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace LoopCommandSharp.ViewModels
{
    public class MintFeeWindowViewModel : ViewModelBase
    {
        public string MintFeeStatus { get; set; }


        public ReactiveCommand<Unit, MintFeeResultWindowViewModel?> ConfirmCommand { get; }

        public ReactiveCommand<Unit, MintFeeResultWindowViewModel?> CancelCommand { get; }

        public MintFeeWindowViewModel()
        {
            ConfirmCommand = ReactiveCommand.Create(() =>
            {
                return new MintFeeResultWindowViewModel() { ConfirmMintFee = true};
            });

            CancelCommand = ReactiveCommand.Create(() =>
            {
                return new MintFeeResultWindowViewModel() { ConfirmMintFee = false };
            });
        }

    }
}

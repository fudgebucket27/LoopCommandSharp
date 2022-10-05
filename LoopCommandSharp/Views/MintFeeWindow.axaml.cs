using Avalonia.Controls;
using Avalonia.ReactiveUI;
using LoopCommandSharp.ViewModels;
using ReactiveUI;
using System;

namespace LoopCommandSharp.Views
{
    public partial class MintFeeWindow : ReactiveWindow<MintFeeWindowViewModel>
    {
        public MintFeeWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel!.ConfirmCommand.Subscribe(Close)));
            this.WhenActivated(d => d(ViewModel!.CancelCommand.Subscribe(Close)));
        }
    }
}

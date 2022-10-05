using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using LoopCommandSharp.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace LoopCommandSharp.Views
{
    public partial class MintNftWindow : ReactiveWindow<MintNftWindowViewModel>
    {
        public MintNftWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }

        private async Task DoShowDialogAsync(InteractionContext<MintFeeWindowViewModel, MintFeeResultWindowViewModel?> interaction)
        {
            var dialog = new MintFeeWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<MintFeeResultWindowViewModel?>(this);
            interaction.SetOutput(result);
        }
    }

}

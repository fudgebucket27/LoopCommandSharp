using Avalonia.Controls;
using Avalonia.Interactivity;
using LoopCommandSharp.ViewModels;

namespace LoopCommandSharp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void ShowMintNftWindow(object sender, RoutedEventArgs e)
        {
            Hide();
            var window = new MintNftWindow()
            {
                DataContext = new MintNftWindowViewModel()
            };
            window.Show();
        }
    }
}

using MediaPlayerFinalizerHang.ViewModels;
using System;
using System.Windows;

namespace MediaPlayerFinalizerHang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
        }

        protected override void OnClosed(EventArgs e)
        {
            MessageBox.Show("MainWindow.OnClosed called... You should see one more message box! Otherwise, the application is still running.");
            _viewModel.Dispose();

            GC.Collect();

            // Application will hang here after playing the "bad" video
            GC.WaitForPendingFinalizers();

            MessageBox.Show("MainWindow.OnClosed called... And we have successfully waited for pending finalizers!");
            base.OnClosed(e);
        }

        private void GoodButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.PlayGoodVideo();
        }

        private void BadButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.PlayBadVideo();
        }

        MainWindowViewModel _viewModel;
    }
}

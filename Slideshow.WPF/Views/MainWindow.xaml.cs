using System.Windows;

namespace SlideShow.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //// 初期からハンバーガーメニューを開いた状態
            MenuToggleButton.IsChecked = true;
        }
    }
}

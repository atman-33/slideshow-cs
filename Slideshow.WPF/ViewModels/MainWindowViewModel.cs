using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Slideshow.WPF.Views;

namespace SlideShow.WPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        //// 画面遷移
        private IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            //// 画面遷移用（ナビゲーション）
            _regionManager = regionManager;

            //// 初期画面
            //_regionManager.RegisterViewWithRegion("ContentRegion", nameof(SlideshowView));

            //// DelegateCommandメソッドを登録
            SlideshowViewButton = new DelegateCommand(SlideshowViewButtonExecute);
            PageSettingViewButton = new DelegateCommand(PageSettingViewButtonExecute);
            OtherSettingsViewButton = new DelegateCommand(OtherSettingsViewButtonExecute);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 1. Property Data Binding
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        private string _title = "スライドショー アプリ";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _viewOutline;
        public string ViewOutline
        {
            get { return _viewOutline; }
            set { SetProperty(ref _viewOutline, value); }
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 2. Event Binding (DelegateCommand)
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public DelegateCommand SlideshowViewButton { get; }

        private void SlideshowViewButtonExecute()
        {
            //// パラメータ渡し
            var p = new NavigationParameters();
            p.Add("MainWindow", this);

            _regionManager.RequestNavigate("ContentRegion", nameof(SlideshowView), p);
        }

        public DelegateCommand PageSettingViewButton { get; }

        private void PageSettingViewButtonExecute()
        {
            //// パラメータ渡し
            var p = new NavigationParameters();
            p.Add("MainWindow", this);

            _regionManager.RequestNavigate("ContentRegion", nameof(PageSettingView), p);
        }

        public DelegateCommand OtherSettingsViewButton { get; }

        private void OtherSettingsViewButtonExecute()
        {
            //// パラメータ渡し
            var p = new NavigationParameters();
            p.Add("MainWindow", this);

            _regionManager.RequestNavigate("ContentRegion", nameof(OtherSettingsView), p);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 3. Others
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
    }
}

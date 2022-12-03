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
            ContentTitle = "Slideshow";
            _regionManager.RegisterViewWithRegion("ContentRegion", nameof(SlideshowView));


            //// DelegateCommandメソッドを登録
            SlideshowViewButton = new DelegateCommand(SlideshowViewButtonExecute);
            PageSettingViewButton = new DelegateCommand(PageSettingViewButtonExecute);
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

        private string _contentTitle;
        public string ContentTitle
        {
            get { return _contentTitle; }
            set { SetProperty(ref _contentTitle, value); }
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 2. Event Binding (DelegateCommand)
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        public DelegateCommand SlideshowViewButton { get; }

        private void SlideshowViewButtonExecute()
        {
            ContentTitle = "Slideshow";
            _regionManager.RequestNavigate("ContentRegion", nameof(SlideshowView));
        }

        public DelegateCommand PageSettingViewButton { get; }

        private void PageSettingViewButtonExecute()
        {
            //// パラメータ渡しのソース例
            //var p = new NavigationParameters();
            //p.Add("MainWindowViewModelRegionManager", _regionManager);

            ContentTitle = "Page Setting";
            _regionManager.RequestNavigate("ContentRegion", nameof(PageSettingView));
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 3. Others
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SlideShow.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Slideshow.WPF.ViewModels
{
    public class SlideshowViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// MainWindow
        /// </summary>
        private MainWindowViewModel _mainWindowViewModel;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SlideshowViewModel()
        {

        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 1. Property Data Binding
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 


        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 2. Event Binding (DelegateCommand)
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 


        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 3. Others
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //// 遷移前の画面からパラメータ受け取り
            _mainWindowViewModel = navigationContext.Parameters.GetValue<MainWindowViewModel>("MainWindow");
            _mainWindowViewModel.ViewOutline = "> Slideshow";
        }
    }
}

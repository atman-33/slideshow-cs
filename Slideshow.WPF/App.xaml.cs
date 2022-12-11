using Prism.Ioc;
using Prism.Regions;
using Slideshow.Domain.Exceptions;
using Slideshow.WPF.ViewModels;
using Slideshow.WPF.Views;
using SlideShow.WPF.Views;
using System.Windows;
using System.Windows.Threading;

namespace SlideShow.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            //// 例外処理をキャッチする処理
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //// 下記処理を追加しても例外発生でアプリが中断する場合、例外停止箇所の例外の編集から中断しないよう設定を変更する

            //_logger.Error(ex.Message, ex);              //// ログ出力

            MessageBoxImage icon = MessageBoxImage.Error;
            string caption = "エラー";
            var exceptionBase = e.Exception as ExceptionBase;    //// 型が異なる場合はnullが返る
            if (exceptionBase != null)
            {
                if (exceptionBase.Kind == ExceptionBase.ExceptionKind.Info)
                {
                    icon = MessageBoxImage.Information;
                    caption = "情報";
                }
                else if (exceptionBase.Kind == ExceptionBase.ExceptionKind.Warning)
                {
                    icon = MessageBoxImage.Warning;
                    caption = "警告";
                }
            }
            MessageBox.Show(e.Exception.Message, caption, MessageBoxButton.OK, icon);

            e.Handled = true;   //// true:アプリケーションが落ちない
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //// ナビゲーション画面 ※パラメータを引き渡す場合は、ViewModel に INavigationAware 実装が必要
            containerRegistry.RegisterForNavigation<SlideshowView>();
            containerRegistry.RegisterForNavigation<PageSettingView>();
            containerRegistry.RegisterForNavigation<PageEditingView>();
            containerRegistry.RegisterForNavigation<SlidePatternSettingView>();
            containerRegistry.RegisterForNavigation<SlidePatternEditingView>();
            containerRegistry.RegisterForNavigation<OtherSettingsView>();

            //// ダイアログ画面（別画面に表示） ※ViewModel に IDialogAware 実装が必要
            //// ex. containerRegistry.RegisterDialog<XXXView, XXXViewModel>();
        }
    }
}

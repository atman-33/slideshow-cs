using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Slideshow.Domain;
using Slideshow.Domain.Entities;
using Slideshow.Domain.Modules.Helpers;
using Slideshow.Domain.Repositories;
using Slideshow.WPF.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using SlideShow.WPF.ViewModels;
using Slideshow.Infrastructure;

namespace Slideshow.WPF.ViewModels
{
    public class PageEditingViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// MainWindow
        /// </summary>
        private MainWindowViewModel _mainWindowViewModel;

        //// メッセージボックス
        private IMessageService _messageService;

        //// 外部接触Repository
        private IPageMstRepository _pageMstRepository;

        //public IMediaService MediaService { get; private set; }

        private MediaElement _movieMediaElement;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PageEditingViewModel()
        {
            //// メッセージボックス
            _messageService = new MessageService();

            //// DelegateCommandメソッドを登録
            MoviePlayButton = new DelegateCommand(MoviePlayButtonExecute);
            MovieStopButton = new DelegateCommand(MovieStopButtonExecute);

            SaveButton = new DelegateCommand(SaveButtonExecute);
            PreviewButton = new DelegateCommand(PreviewButtonExecute);
            OpenMovieFileButton = new DelegateCommand(OpenMovieFileButtonExecute);
            OpenImageFileButton = new DelegateCommand(OpenImageFileButtonExecute);
            ImagePageNoDownButton = new DelegateCommand(ImagePageNoDownButtonExecute);
            ImagePageNoUpButton = new DelegateCommand(ImagePageNoUpButtonExecute);

            //// Factories経由で作成したRepositoryを、プライベート変数に格納
            _pageMstRepository = Factories.CreatePageMst();

            //// 新規の説明入力レコードを生成
            for (int i = 1; i <= 3; i++)
            {
                NoteEntities.Add(new NoteEntity(String.Empty));
            }

            //// 動画エレメントを設定
            _movieMediaElement = new MediaElement();
            MovieItemsControl.Add(_movieMediaElement);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 1. Property Data Binding
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        private int _pageIdText;
        public int PageIdText
        {
            get { return _pageIdText; }
            set { SetProperty(ref _pageIdText, value); }
        }

        private string _pageNameText;
        public string PageNameText
        {
            get { return _pageNameText; }
            set { SetProperty(ref _pageNameText, value); }
        }

        private string _movieLinkText;
        public string MovieLinkText
        {
            get { return _movieLinkText; }
            set { SetProperty(ref _movieLinkText, value); }
        }
 
        private string _imageLinkText;
        public string ImageLinkText
        {
            get { return _imageLinkText; }
            set { SetProperty(ref _imageLinkText, value); }
        }

        private int? _imagePageNoText = null;
        public int? ImagePageNoText
        {
            get { return _imagePageNoText; }
            set { SetProperty(ref _imagePageNoText, value); }
        }

        private float? _slideWaitingTimeText = null;
        public float? SlideWaitingTimeText
        {
            get { return _slideWaitingTimeText; }
            set { SetProperty(ref _slideWaitingTimeText, value); }
        }

        private ObservableCollection<NoteEntity> _noteEntities = new ObservableCollection<NoteEntity>();
        public ObservableCollection<NoteEntity> NoteEntities
        {
            get { return _noteEntities; }
            set { SetProperty(ref _noteEntities, value); }
        }

        private ObservableCollection<UIElement> _movieItemsControl = new ObservableCollection<UIElement>();
        public ObservableCollection<UIElement> MovieItemsControl
        {
            get { return _movieItemsControl; }
            set { SetProperty(ref _movieItemsControl, value); }
        }

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }


        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 2. Event Binding (DelegateCommand)
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        public DelegateCommand MoviePlayButton { get; }
        private void MoviePlayButtonExecute()
        {
            _movieMediaElement.Position = TimeSpan.Zero;
            _movieMediaElement.Visibility = System.Windows.Visibility.Visible;
            _movieMediaElement.LoadedBehavior = MediaState.Manual;

            _movieMediaElement.Play();
        }

        public DelegateCommand MovieStopButton { get; }
        private void MovieStopButtonExecute()
        {
            _movieMediaElement.Stop() ;
        }

        public DelegateCommand OpenMovieFileButton { get; }
        private void OpenMovieFileButtonExecute()
        {
            //// ダイアログのインスタンスを生成
            var dialog = new OpenFileDialog();
            dialog.Title = "動画ファイルを選択してください";
            dialog.InitialDirectory = Shared.DialogInitialDirectory;

            //// ファイルの種類を設定
            dialog.Filter = @"動画 ファイル|*.mp4;*.avi;*.mov|全てのファイル (*.*)|*.*";

            //// ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                //// 選択されたファイル名 (ファイルパス) を取得
                MovieLinkText = dialog.FileName;
            }

            //// 動画プレビュー更新
            PreviewMovie();
        }

        public DelegateCommand OpenImageFileButton { get; }
        private void OpenImageFileButtonExecute()
        {
            using (var cofd = new CommonOpenFileDialog()
            {
                Title = "画像フォルダを選択してください",
                InitialDirectory = Shared.DialogInitialDirectory,
                //// フォルダ選択モードにする
                IsFolderPicker = true,
            })
            {
                if (cofd.ShowDialog() != CommonFileDialogResult.Ok)
                {
                    return;
                }

                ImageLinkText = cofd.FileName;
            }

            //// ページNoを設定
            ImagePageNoText = 1;

            //// 画像プレビュー更新
            PreviewImage();
        }
        public DelegateCommand ImagePageNoDownButton { get; }
        private void ImagePageNoDownButtonExecute()
        {
            if (ImagePageNoText <= 1)
            {
                return;
            }

            ImagePageNoText = ImagePageNoText - 1;

            //// 画像プレビュー更新
            PreviewImage();
        }
        public DelegateCommand ImagePageNoUpButton { get; }
        private void ImagePageNoUpButtonExecute()
        {
            ImagePageNoText = ImagePageNoText + 1;

            //// 画像プレビュー更新
            PreviewImage();
        }

        public DelegateCommand PreviewButton { get; }
        private void PreviewButtonExecute()
        {
            //// 動画プレビュー更新
            PreviewMovie();

            //// 画像プレビュー更新
            PreviewImage();
        }

        public DelegateCommand SaveButton { get; }
        private void SaveButtonExecute()
        {
            Guard.IsNull(PageIdText, "ページIDを入力してください");
            Guard.IsNull(SlideWaitingTimeText, "スライド停止時間を入力してください");
            var slideWaitingTimeText = Guard.IsFloat(SlideWaitingTimeText.ToString(), "スライド停止時間の入力に誤りがあります");

            if (_messageService.Question("保存しますか？") != System.Windows.MessageBoxResult.OK)
            {
                return;
            }

            var entity = new PageMstEntity(
                PageIdText,
                PageNameText,
                MovieLinkText,
                ImageLinkText,
                ImagePageNoText,
                (float)SlideWaitingTimeText,
                NoteEntities[0].Note,
                NoteEntities[1].Note,
                NoteEntities[2].Note
                );

            _pageMstRepository.Save(entity);

            _messageService.ShowDialog("保存しました", "情報", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 3. Others
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        /// <summary>
        /// trueの時はViewインスタンスを再利用
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //// 遷移前の画面からパラメータ受け取り
            PageIdText = navigationContext.Parameters.GetValue<int>("PageId");

            _mainWindowViewModel = navigationContext.Parameters.GetValue<MainWindowViewModel>("MainWindow");
            _mainWindowViewModel.ViewOutline = "> PageSetting > PageEditing";
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void PreviewMovie()
        {
            if (MovieLinkText == null)
            {
                return;
            }

            _movieMediaElement.Source = new Uri(MovieLinkText, UriKind.Relative);
            _movieMediaElement.Position = TimeSpan.Zero;
            _movieMediaElement.Visibility = System.Windows.Visibility.Visible;
            _movieMediaElement.LoadedBehavior = MediaState.Manual;

            _movieMediaElement.Play();
        }

        private void PreviewImage()
        {
            string filePath = ImageLinkText + "\\" + "スライド" + ImagePageNoText.ToString() + "." + Shared.ImageExtension;
            Console.WriteLine("画像ファイル：" + filePath);

            if (File.Exists(filePath) == false)
            {
                ImageSource = null;
                return;
            }

            BitmapImage bmpImage = new BitmapImage();
            using (FileStream stream = File.OpenRead(filePath))
            {
                bmpImage.BeginInit();
                bmpImage.StreamSource = stream;
                bmpImage.DecodePixelWidth = 500;
                bmpImage.CacheOption = BitmapCacheOption.OnLoad;
                bmpImage.CreateOptions = BitmapCreateOptions.None;
                bmpImage.EndInit();
                bmpImage.Freeze();
            }

            ImageSource = bmpImage;
        }
    }
}

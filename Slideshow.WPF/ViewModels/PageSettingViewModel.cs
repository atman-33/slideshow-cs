﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Slideshow.Domain;
using Slideshow.Domain.Entities;
using Slideshow.Domain.Repositories;
using Slideshow.WPF.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Template.Infrastruture;

namespace Slideshow.WPF.ViewModels
{
    public class PageSettingViewModel : BindableBase, INavigationAware
    {
        //// 画面遷移
        private IRegionManager _regionManager;
        private IDialogService _dialogService; 

        //// 外部接触Repository
        private IPageMstRepository _pageMstRepository;

        /// <summary>
        /// PageMstRecordsのオリジン（未フィルターのコレクション）
        /// </summary>
        private ObservableCollection<PageSettingViewModelPageMst> _pageMstRecordsOrigin;

        public PageSettingViewModel(IDialogService dialogService)
        {
            //// 画面遷移用（ダイアログ）
            _dialogService = dialogService;

            //// DelegateCommandメソッドを登録
            PageEditingViewButton = new DelegateCommand(PageEditingViewButtonExecute);
            PageNameSearchingTextChanged = new DelegateCommand(PageNameSearchingTextChangedExecute);
            PageMstRecordsSelectedCellsChanged = new DelegateCommand(PageMstRecordsSelectedCellsChangedExecute);

            //// Factories経由で作成したRepositoryを、プライベート変数に格納
            _pageMstRepository = Factories.CreatePageMst();

            //// Repositoryからデータ取得
            foreach (var entity in _pageMstRepository.GetData())
            {
                PageMstRecords.Add(new PageSettingViewModelPageMst(entity));
            }
            _pageMstRecordsOrigin = PageMstRecords; //// Originに格納
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 1. Property Data Binding
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        /// <summary>
        /// 登録済みページ一覧を表示するDataGridのItemsSource
        /// </summary>
        private ObservableCollection<PageSettingViewModelPageMst> _pageMstRecords = new ObservableCollection<PageSettingViewModelPageMst>();
        public ObservableCollection<PageSettingViewModelPageMst> PageMstRecords
        {
            get { return _pageMstRecords; }
            set { SetProperty(ref _pageMstRecords, value); }
        }

        /// <summary>
        /// 登録済みページ一覧の選択アイテム
        /// </summary>
        private PageSettingViewModelPageMst _pageMstRecordsSlectedItem;
        public PageSettingViewModelPageMst PageMstRecordsSlectedItem
        {
            get { return _pageMstRecordsSlectedItem; }
            set { SetProperty(ref _pageMstRecordsSlectedItem, value); }
        }

        /// <summary>
        /// ページ名称検索ボックスのテキスト
        /// </summary>
        private string _pageNameSearchingText;
        public string PageNameSearchingText
        {
            get { return _pageNameSearchingText; }
            set { SetProperty(ref _pageNameSearchingText, value); }
        }

        /// <summary>
        /// 画像表示用グループボックスのヘッダー
        /// </summary>
        private string _imageGroupBoxHeader = "選択アイテムの画像";
        public string ImageGroupBoxHeader
        {
            get { return _imageGroupBoxHeader; }
            set { SetProperty(ref _imageGroupBoxHeader, value); }
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

        /// <summary>
        /// ページ編集画面を表示
        /// </summary>
        public DelegateCommand PageEditingViewButton { get; }

        private void PageEditingViewButtonExecute()
        {
            //// パラメータ渡し
            var p = new NavigationParameters();
            p.Add("PageId", GetNewPageId());

            //// 画面遷移処理
            _regionManager.RequestNavigate("ContentRegion", nameof(PageEditingView), p);
        }

        /// <summary>
        /// ページ名称検索テキストが変化した際の処理
        /// </summary>
        public DelegateCommand PageNameSearchingTextChanged { get; }

        private void PageNameSearchingTextChangedExecute()
        {
            if (PageNameSearchingText == null)
            {
                return;
            }

            var filterList = _pageMstRecordsOrigin.Where(x => x.PageName.Contains(PageNameSearchingText)).ToList();
            var collection = new ObservableCollection<PageSettingViewModelPageMst>();

            foreach (var record in filterList)
            {
                collection.Add(record);
            }

            PageMstRecords = collection;
        }

        /// <summary>
        /// ページ一覧のDataGridの選択セルが変化した際の処理
        /// </summary>
        public DelegateCommand PageMstRecordsSelectedCellsChanged { get; }

        private void PageMstRecordsSelectedCellsChangedExecute()
        {
            if (PageMstRecordsSlectedItem == null)
            {
                return;
            }

            ImageGroupBoxHeader = "選択アイテムの画像（ID：" + PageMstRecordsSlectedItem.PageId.ToString() + "）";

            //// 画像更新
            PreviewImage();
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        //// 3. Others
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        /// <summary>
        /// trueの時はViewインスタンスを再利用
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //// 遷移前の画面からパラメータ渡し
            _regionManager = navigationContext.Parameters.GetValue<IRegionManager>("MainWindowViewModelRegionManager");
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// 新規登録する場合のPageIdを取得
        /// </summary>
        /// <returns></returns>
        private int GetNewPageId()
        {
            var collection = new ObservableCollection<PageMstEntity>();

            //// Repositoryからデータ取得
            foreach (var entity in _pageMstRepository.GetData())
            {
                collection.Add(entity);
            }
            return collection.Max(t => t.PageId.Value) + 1;
        }

        private void PreviewImage()
        {
            string filePath = PageMstRecordsSlectedItem.ImageLink + "\\" + "スライド" + PageMstRecordsSlectedItem.ImagePageNo.ToString() + "." + Shared.ImageExtension;
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

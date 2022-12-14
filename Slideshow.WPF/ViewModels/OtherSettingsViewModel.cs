using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Slideshow.Domain.Entities;
using Slideshow.Domain.Modules.Helpers;
using Slideshow.Domain.Repositories;
using Slideshow.Infrastructure;
using Slideshow.WPF.Services;
using SlideShow.WPF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Controls;

namespace Slideshow.WPF.ViewModels
{
    public class OtherSettingsViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// MainWindow
        /// </summary>
        private MainWindowViewModel _mainWindowViewModel;

        //// メッセージボックス
        private IMessageService _messageService;

        //// 外部接触Repository
        private ISlidePatternNameSelectionMstRepository _slidePatternNameSelectionMstRepository;


        public OtherSettingsViewModel()
        {
            //// メッセージボックス
            _messageService = new MessageService();

            //// DelegateCommandメソッドを登録
            SlidePatternNameSelectionMstEntitiesSelectedCellsChanged = new DelegateCommand(SlidePatternNameSelectionMstEntitiesSelectedCellsChangedExecute);
            NewButton = new DelegateCommand(NewButtonExecute);
            SaveButton = new DelegateCommand(SaveButtonExecute);
            DeleteButton = new DelegateCommand(DeleteButtonExecute);

            //// Factories経由で作成したRepositoryを、プライベート変数に格納
            _slidePatternNameSelectionMstRepository = Factories.CreateSlidePatternNameSelectionMst();

            //// Repositoryからデータ取得
            UpdateSlidePatternNameSelectionMstEntities();
        }

        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        #region //// 1. Property Data Binding
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        private ObservableCollection<OtherSettingsViewModelSlidePatternNameSelectionMst> _slidePatternNameSelectionMstEntities
            = new ObservableCollection<OtherSettingsViewModelSlidePatternNameSelectionMst>();
        public ObservableCollection<OtherSettingsViewModelSlidePatternNameSelectionMst> SlidePatternNameSelectionMstEntities
        {
            get { return _slidePatternNameSelectionMstEntities; }
            set { SetProperty(ref _slidePatternNameSelectionMstEntities, value); }
        }

        private OtherSettingsViewModelSlidePatternNameSelectionMst _slidePatternNameSelectionMstEntitiesSlectedItem;
        public OtherSettingsViewModelSlidePatternNameSelectionMst SlidePatternNameSelectionMstEntitiesSlectedItem
        {
            get { return _slidePatternNameSelectionMstEntitiesSlectedItem; }
            set { SetProperty(ref _slidePatternNameSelectionMstEntitiesSlectedItem, value); }
        }

        private int _slidePatternExampleIdText;
        public int SlidePatternExampleIdText
        {
            get { return _slidePatternExampleIdText; }
            set { SetProperty(ref _slidePatternExampleIdText, value); }
        }

        private string _slidePatternNameText;
        public string SlidePatternNameText
        {
            get { return _slidePatternNameText; }
            set { SetProperty(ref _slidePatternNameText, value); }
        }

        private bool _idIsEnabled = false;
        public bool IdIsEnabled
        {
            get { return _idIsEnabled; }
            set { SetProperty(ref _idIsEnabled, value); }
        }

        #endregion


        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        #region //// 2. Event Binding (DelegateCommand)
        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 

        public DelegateCommand SlidePatternNameSelectionMstEntitiesSelectedCellsChanged { get; }
        private void SlidePatternNameSelectionMstEntitiesSelectedCellsChangedExecute()
        {
            if (SlidePatternNameSelectionMstEntitiesSlectedItem == null)
            {
                return;
            }

            SlidePatternExampleIdText = SlidePatternNameSelectionMstEntitiesSlectedItem.SlidePatternExampleId;
            SlidePatternNameText = SlidePatternNameSelectionMstEntitiesSlectedItem.SlidePatternNameExample;

            IdIsEnabled = false;
        }

        //NewButton
        public DelegateCommand NewButton { get; }
        private void NewButtonExecute()
        {
            IdIsEnabled = true;
            SlidePatternNameText = String.Empty;
        }

        public DelegateCommand SaveButton { get; }
        private void SaveButtonExecute()
        {
            Guard.IsNullOrEmpty(SlidePatternNameText, "スライドパターン候補名を入力してください");

            if (_messageService.Question("保存しますか？") != System.Windows.MessageBoxResult.OK)
            {
                return;
            }

            var entity = new SlidePatternNameSelectionMstEntity(
                SlidePatternExampleIdText,
                SlidePatternNameText
                );

            _slidePatternNameSelectionMstRepository.Save(entity);
            _messageService.ShowDialog("保存しました", "情報", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            UpdateSlidePatternNameSelectionMstEntities();
        }

        public DelegateCommand DeleteButton { get; }
        private void DeleteButtonExecute()
        {
            if (_messageService.Question("「" + SlidePatternNameText + "」を削除しますか？") != System.Windows.MessageBoxResult.OK)
            {
                return;
            }

            var entity = new SlidePatternNameSelectionMstEntity(
                SlidePatternExampleIdText,
                SlidePatternNameText
                );

            _slidePatternNameSelectionMstRepository.Delete(entity);
            _messageService.ShowDialog("削除しました", "情報", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        
            UpdateSlidePatternNameSelectionMstEntities();
        }

        #endregion


        //// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        #region //// 3. Others
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
            _mainWindowViewModel.ViewOutline = "> Other Settings";

        }

        private void UpdateSlidePatternNameSelectionMstEntities()
        {
            SlidePatternNameSelectionMstEntities.Clear();

            foreach (var entity in _slidePatternNameSelectionMstRepository.GetData())
            {
                SlidePatternNameSelectionMstEntities.Add(new OtherSettingsViewModelSlidePatternNameSelectionMst(entity));
            }
        }

        #endregion
    }
}

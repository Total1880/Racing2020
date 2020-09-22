using AddNamesToDatabase.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Racing.Settings;

namespace AddNamesToDatabase.ViewModel
{
    public class SettingPageViewModel : ViewModelBase
    {
        private readonly ISettingService _settingService;
        private ObservableCollection<Setting> _settingList;
        private Setting _selectedSetting;
        private string _settingValue;
        private RelayCommand _saveSettingCommand;

        public ObservableCollection<Setting> SettingList
        {
            get => _settingList;
            set
            {
                _settingList = value;
                RaisePropertyChanged();
            }
        }

        public Setting SelectedSetting
        {
            get => _selectedSetting;
            set
            {
                if (value != null)
                {
                    _selectedSetting = value;
                    SettingValue = value.Value;
                }
            }
        }

        public string SettingValue
        {
            get => _settingValue;
            set
            {
                _settingValue = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SaveSettingCommand => _saveSettingCommand ??= new RelayCommand(SaveSetting);

        public SettingPageViewModel(ISettingService settingService)
        {
            _settingService = settingService;
            _ = GetSettings();
        }

        private async Task GetSettings()
        {
            SettingList = new ObservableCollection<Setting>(await _settingService.GetSettings());

            var settingListCheck = new SettingsNames();

            foreach (var item in settingListCheck.GetType().GetFields())
            {
                if (SettingList.Any(s => s.Description == item.Name))
                {
                    continue;
                }

                _settingService.AddSetting(new Setting { Description = item.Name, Value = "1" });
            }

            SettingList = new ObservableCollection<Setting>(await _settingService.GetSettings());
        }

        private void SaveSetting()
        {
            SelectedSetting.Value = SettingValue;
            _settingService.EditSetting(SelectedSetting);
        }
    }
}

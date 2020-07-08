using AddNamesToDatabase.Messages;
using AddNamesToDatabase.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AddNamesToDatabase.ViewModel
{
    public class NationPageViewModel : ViewModelBase
    {
        private readonly INationService _nationService;
        private ObservableCollection<Nation> _nationList;
        private RelayCommand _addNationCommand;

        public ObservableCollection<Nation> NationList
        {
            get => _nationList;
            set
            {
                _nationList = value;
                RaisePropertyChanged();
            }
        }

        public string Nation { get; set; }

        public RelayCommand AddNationCommand => _addNationCommand ??= new RelayCommand(AddNation);

        public NationPageViewModel(INationService nationService)
        {
            _nationService = nationService;
            _ = GetNations();
        }

        private async Task GetNations()
        {
            NationList = new ObservableCollection<Nation>(await _nationService.GetNations());
        }

        private void AddNation()
        {
            var newNation = new Nation { Name = Nation };
            _nationService.CreateNation(newNation);
            _ = GetNations();
            MessengerInstance.Send(new NationCreatedMessage());
        }
    }
}

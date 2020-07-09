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
        private RelayCommand _editNationCommand;
        private RelayCommand _deleteNationCommand;
        private Nation _selectedNation;
        private string _nation;

        public ObservableCollection<Nation> NationList
        {
            get => _nationList;
            set
            {
                _nationList = value;
                RaisePropertyChanged();
            }
        }

        public string Nation
        {
            get => _nation;
            set
            {
                _nation = value;
                RaisePropertyChanged();
            }
        }

        public Nation SelectedNation
        {
            get => _selectedNation;
            set
            {
                if (value != null)
                {
                    _selectedNation = value;
                    Nation = value.Name;
                }
            }
        }

        public RelayCommand AddNationCommand => _addNationCommand ??= new RelayCommand(AddNation);
        public RelayCommand EditNationCommand => _editNationCommand ??= new RelayCommand(EditNation);
        public RelayCommand DeleteNationCommand => _deleteNationCommand ??= new RelayCommand(DeleteNation);

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

        private void EditNation()
        {
            if (SelectedNation != null)
            {
                SelectedNation.Name = Nation;
                _nationService.EditNation(SelectedNation);
                _ = GetNations();
                MessengerInstance.Send(new NationCreatedMessage());
                Nation = string.Empty;
            }
        }

        private void DeleteNation()
        {
            if (SelectedNation != null)
            {
                _nationService.DeleteNation(SelectedNation.NationId);
                _ = GetNations();
                MessengerInstance.Send(new NationCreatedMessage());
            }
        }
    }
}

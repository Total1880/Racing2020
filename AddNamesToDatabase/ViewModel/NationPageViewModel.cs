using AddNamesToDatabase.Services.Interfaces;
using GalaSoft.MvvmLight;
using Racing.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AddNamesToDatabase.ViewModel
{
    public class NationPageViewModel : ViewModelBase
    {
        private readonly INationService _nationService;
        private ObservableCollection<Nation> _nationList;

        public ObservableCollection<Nation> NationList
        {
            get => _nationList;
            set
            {
                _nationList = value;
                RaisePropertyChanged();
            }
        }

        public NationPageViewModel(INationService nationService)
        {
            _nationService = nationService;
            _ = GetNations();
        }

        private async Task GetNations()
        {
            NationList = new ObservableCollection<Nation>(await _nationService.GetNations());
        }
    }
}

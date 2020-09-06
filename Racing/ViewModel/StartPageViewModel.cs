using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Messages;
using Racing.Messages.WindowOpener;
using Racing.Services.Interfaces;

namespace Racing.ViewModel
{
    public class StartPageViewModel  : ViewModelBase
    {
        private RelayCommand _newGameCommand;
        private RelayCommand _continueGameCommand;
        private readonly ISaveGameDivisionService _saveGameDivisionService;

        public RelayCommand NewGameCommand => _newGameCommand ??= new RelayCommand(NewGame);
        public RelayCommand ContinueGameCommand => _continueGameCommand ??= new RelayCommand(ContinueGame);

        public StartPageViewModel(ISaveGameDivisionService saveGameDivisionService)
        {
            _saveGameDivisionService = saveGameDivisionService;
        }

        private void NewGame()
        {
            MessengerInstance.Send(new OpenHomePageMessage());
            MessengerInstance.Send(new NewGameMessage());
        }

        private void ContinueGame()
        {
            MessengerInstance.Send(new OpenHomePageMessage());
            MessengerInstance.Send(new ContinueGameMessage(_saveGameDivisionService.GetDivisions()));
        }
    }
}

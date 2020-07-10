using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Messages.WindowOpener;
using System;

namespace Racing.ViewModel
{
    public class StartPageViewModel  : ViewModelBase
    {
        private RelayCommand _newGameCommand;

        public RelayCommand NewGameCommand => _newGameCommand ??= new RelayCommand(NewGame);

        private void NewGame()
        {
            MessengerInstance.Send(new OpenHomePageMessage());
        }
    }
}

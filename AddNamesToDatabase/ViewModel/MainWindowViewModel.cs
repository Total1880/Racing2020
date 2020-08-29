using AddNamesToDatabase.Messages;
using AddNamesToDatabase.Services.Interfaces;
using ExcelDataReader;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace AddNamesToDatabase.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly INationService _nationService;
        private readonly INameService _nameService;
        private ObservableCollection<Nation> _nationList;
        private RelayCommand _uploadListCommand;
        private List<FirstNames> _firstNames;
        private List<LastNames> _lastNames;

        public ObservableCollection<Nation> NationList
        {
            get => _nationList;
            set
            {
                _nationList = value;
                RaisePropertyChanged();
            }
        }

        public Nation SelectedNation { get; set; }

        public string Path { get; set; }

        public RelayCommand UploadListCommand => _uploadListCommand ??= new RelayCommand(UploadList);

        public MainWindowViewModel(INationService nationService, INameService nameService)
        {
            _nationService = nationService;
            _nameService = nameService;
            _ = GetNations();
            MessengerInstance.Register<NationCreatedMessage>(this, OnNationCreated);
        }

        private void OnNationCreated(NationCreatedMessage obj)
        {
            _ = GetNations();
        }

        private async Task GetNations()
        {
            NationList = new ObservableCollection<Nation>(await _nationService.GetNations());
        }

        private void UploadList()
        {
            if (File.Exists(Path) && SelectedNation != null)
            {
                _firstNames = new List<FirstNames>();
                _lastNames = new List<LastNames>();
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using var stream = File.Open(Path, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);

                var dataSetNames = reader.AsDataSet();
                var table = dataSetNames.Tables["Names"];
                var headerFirstNames = "FirstName";
                var headerLastNames = "LastName";
                if (table == null)
                    return;
                var rows = table.Rows;

                int index = 0;

                foreach (DataRow row in rows)
                {
                    if (index == 0)
                    {
                        if (row[0].ToString() != headerFirstNames || row[1].ToString() != headerLastNames)
                            break;
                        index++;
                        continue;
                    }

                    if (row[0].ToString().Length > 0)
                    {
                        _firstNames.Add(new FirstNames { FirstName = row[0].ToString(), Nation = SelectedNation });
                    }

                    if (row[1].ToString().Length > 0)
                    {
                        _lastNames.Add(new LastNames { LastName = row[1].ToString(), Nation = SelectedNation });
                    }
                }

                _nameService.CreateNames(_firstNames, _lastNames);
            }
        }
    }
}

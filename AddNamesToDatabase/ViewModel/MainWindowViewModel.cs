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
        private ObservableCollection<Nation> _nationList;
        private RelayCommand _uploadListCommand;
        private List<FirstNames> _firstNames = new List<FirstNames>();
        private List<LastNames> _lastNames = new List<LastNames>();

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

        public MainWindowViewModel(INationService nationService)
        {
            _nationService = nationService;
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
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using var stream = File.Open(Path, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);

                var dataSetNames = reader.AsDataSet();
                var table = dataSetNames.Tables["names"];
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

                    _firstNames.Add(new FirstNames { FirstName = row[0].ToString(), Nation = SelectedNation });
                    _lastNames.Add(new LastNames { LastName = row[1].ToString(), Nation = SelectedNation });

                    //TODO: lijsten doorsturen naar db
                }
            }
        }
    }
}

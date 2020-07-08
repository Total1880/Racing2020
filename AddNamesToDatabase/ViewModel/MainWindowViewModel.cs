using AddNamesToDatabase.Messages;
using AddNamesToDatabase.Services.Interfaces;
using GalaSoft.MvvmLight;
using Racing.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AddNamesToDatabase.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        //string path = "C:\\Users\\olavh\\Downloads\\TestNames.xls";
        //DataSet dataSet;
        //private RelayCommand _testCommand;
        //public RelayCommand TestCommand => _testCommand ??= new RelayCommand(Test);
        //List<string> FirstNames = new List<string>();
        //List<string> LastNames = new List<string>();

        //private void Test()
        //{
        //    if (File.Exists(path))
        //    {
        //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //        using var stream = File.Open(path, FileMode.Open, FileAccess.Read);
        //        using var reader = ExcelReaderFactory.CreateReader(stream);
        //        dataSet = reader.AsDataSet();
        //    }
        //    var table = dataSet.Tables["names"];
        //    var rows = table.Rows;

        //    foreach (DataRow row in rows)
        //    {
        //        FirstNames.Add(row[0].ToString());
        //        LastNames.Add(row[1].ToString());
        //    }
        //}

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
    }
}

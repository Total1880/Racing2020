using ExcelDataReader;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace AddNamesToDatabase.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        string path = "C:\\Users\\olavh\\Downloads\\TestNames.xls";
        DataSet dataSet;
        private RelayCommand _testCommand;
        public RelayCommand TestCommand => _testCommand ??= new RelayCommand(Test);
        List<string> FirstNames = new List<string>();
        List<string> LastNames = new List<string>();

        private void Test()
        {
            if (File.Exists(path))
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using var stream = File.Open(path, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);
                dataSet = reader.AsDataSet();
            }
            var table = dataSet.Tables["names"];
            var rows = table.Rows;

            foreach (DataRow row in rows)
            {
                FirstNames.Add(row[0].ToString());
                LastNames.Add(row[1].ToString());
            }
        }
    }
}

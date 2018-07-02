using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using CsvHelper;
using CsvHelper.Configuration;
using EpReader.Model;

namespace EpReader.DataService
{
    public class TersmoService : ITersmoService
    {
        private IEnumerable<TersmoModel> _tersmo;

        private const string FileName = "tersmo39.csv";

        public IEnumerable<TersmoModel> LoadDictionary()
        {
            if (_tersmo != null)
                return _tersmo;

            if (!File.Exists(FileName))
            {
                MessageBox.Show("Не найден справочник " + FileName, "Не найден справочник", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                _tersmo = new List<TersmoModel>();
                return _tersmo;
            }

            var csv = new CsvReader(File.OpenText(FileName), new Configuration(){Delimiter = ";"});
            _tersmo = csv.GetRecords<TersmoModel>().ToList();
            return _tersmo;
        }
    }
}

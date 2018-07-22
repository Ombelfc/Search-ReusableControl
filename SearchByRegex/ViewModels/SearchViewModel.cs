using Microsoft.Win32;
using SearchByRegex.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace SearchByRegex.ViewModels
{
    public class SearchViewModel
    {
        public BaseCommand OpenCommand { get; set; }

        public SearchViewModel()
        {
            OpenCommand = new BaseCommand(OnOpen, CanOpen);
        }

        private void OnOpen()
        {
            TextRange range;
            FileStream fStream;
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.DefaultExt = ".txt";
            ofd.Filter = "Text files (*.txt)|*.txt";
            Nullable<bool> result = ofd.ShowDialog();

            if (result.HasValue && result.Value)
            {
                string fileName = ofd.FileName;

                if (File.Exists(fileName))
                {
                    range = new TextRange();
                    fStream = new FileStream(fileName, FileMode.Open);
                    range.Load(fStream, DataFormats.Text);
                    fStream.Close();
                }
            }
        }

        private bool CanOpen()
        {

        }
    }
}

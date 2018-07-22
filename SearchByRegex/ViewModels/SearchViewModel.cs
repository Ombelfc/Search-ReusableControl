using Microsoft.Win32;
using SearchByRegex.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace SearchByRegex.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private OpenCommand openCommand;

        public OpenCommand OpenCommand
        {
            get => openCommand;
        }

        private string fileContent;

        public string FileContent
        {
            get => fileContent;
            set
            {
                fileContent = value;
                RaiseOnPropertyChanged("FileContent");               
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaiseOnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SearchViewModel()
        {
            openCommand = new OpenCommand(OnOpen);
        }

        private void OnOpen()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Text files (*.txt)|*.txt"
            };

            bool? result = dialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                string fileName = dialog.FileName;

                if (File.Exists(fileName))
                {
                    FileContent = File.ReadAllText(fileName);
                }
            }           
        }
    }
}
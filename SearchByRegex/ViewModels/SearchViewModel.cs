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
        #region Commands

        private OpenCommand openCommand;

        public OpenCommand OpenCommand
        {
            get => openCommand;
        }

        private SearchCommand searchNextCommand;

        public SearchCommand SearchNextCommand
        {
            get => searchNextCommand;
        }

        private SearchCommand searchAllCommand;

        public SearchCommand SearchAllCommand
        {
            get => searchAllCommand;
        }

        #endregion

        #region Model

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

        private string regexNextPattern;

        public string RegexNextPattern
        {
            get => regexNextPattern;
            set
            {
                regexNextPattern = value;
                SearchNextCommand.RaiseCanExecuteChanged();
            }
        }

        private string regexAllPattern;

        public string RegexAllPattern
        {
            get => regexAllPattern;
            set
            {
                regexAllPattern = value;
                SearchAllCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region INotifyPropertyChanged Interface

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaiseOnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public static event EventHandler<string> NextSearchButtonClicked;
        public static event EventHandler<string> AllSearchButtonClicked;
        public static event EventHandler<string> OpenFileButtonClicked;

        public SearchViewModel()
        {
            openCommand = new OpenCommand(OnOpen);
            searchNextCommand = new SearchCommand(OnNextSearch, CanNextSearch);
            searchAllCommand = new SearchCommand(OnAllSearch, CanAllSearch);
        }

        #region File

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
                    OpenFileButtonClicked?.Invoke(this, File.ReadAllText(fileName));
                    FileContent = File.ReadAllText(fileName);
                }
            }
        }

        #endregion

        #region Search

        private void OnNextSearch()
        {
            if (String.IsNullOrWhiteSpace(FileContent)) return;

            NextSearchButtonClicked?.Invoke(this, RegexNextPattern);
        }

        private bool CanNextSearch()
        {
            return (!String.IsNullOrWhiteSpace(RegexNextPattern));
        }

        private void OnAllSearch()
        {
            if (String.IsNullOrWhiteSpace(FileContent)) return;

            AllSearchButtonClicked?.Invoke(this, RegexAllPattern);
        }

        private bool CanAllSearch()
        {
            return (!String.IsNullOrWhiteSpace(RegexAllPattern));
        }

        #endregion
    }
}
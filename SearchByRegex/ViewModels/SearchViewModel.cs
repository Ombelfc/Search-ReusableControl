using Microsoft.Win32;
using SearchByRegex.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace SearchByRegex.ViewModels
{
    public class SearchViewModel : BaseViewModel
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
                RaiseOnPropertyChangedEvent("FileContent");
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

        private FlowDocument fileContentDocument;

        public FlowDocument FileContentDocument
        {
            get => fileContentDocument;
            set
            {
                fileContentDocument = value;
                RaiseOnPropertyChangedEvent("FileContentDocument");
            }
        }

        #endregion

        public static event EventHandler<string> NextSearchButtonClicked;
        public static event EventHandler<string> AllSearchButtonClicked;

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
                    FileContent = File.ReadAllText(fileName);
                    FileContentDocument = new FlowDocument();
                    FileContentDocument.Blocks.Add(new Paragraph(new Run(FileContent)));
                }
            }
        }

        #endregion

        #region Search

        private void OnNextSearch()
        {
            if (String.IsNullOrEmpty(FileContent))
            {
                MessageBox.Show("Load some text first!");
                return;
            }

            NextSearchButtonClicked?.Invoke(this, RegexNextPattern);
        }

        private bool CanNextSearch()
        {
            return (!String.IsNullOrEmpty(RegexNextPattern));
        }

        private void OnAllSearch()
        {
            if (String.IsNullOrEmpty(FileContent))
            {
                MessageBox.Show("Load some text first!");
                return;
            }

            AllSearchButtonClicked?.Invoke(this, RegexAllPattern);
        }

        private bool CanAllSearch()
        {
            return (!String.IsNullOrEmpty(RegexAllPattern));
        }

        #endregion
    }
}
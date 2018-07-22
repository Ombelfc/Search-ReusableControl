using SearchByRegex.Utilities;
using SearchByRegex.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SearchByRegex.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        private string ToSearch { get; set; }
        private bool HasNext { get; set; } = false;
        private Regex Rgx { get; set; }

        public SearchView()
        {
            InitializeComponent();

            SearchViewModel.NextSearchButtonClicked += OnNextSearchClicked;
            SearchViewModel.AllSearchButtonClicked += OnAllSearchClicked;
        }

        #region Next Search

        private void OnNextSearchClicked(object sender, string pattern)
        {
            if (String.IsNullOrWhiteSpace(ToSearchText.GetText())) return;

            if (!pattern.Equals(ToSearch))
            {
                ToSearch = pattern;
                HasNext = false;
                Rgx = new Regex(@pattern);
            }

            HasNext = Search(ToSearchText, pattern, HasNext);
        }

        private bool Search(RichTextBox richTextBox, string pattern, bool searchNext)
        {
            TextRange searchRange;

            if (searchNext)
            {
                searchRange = new TextRange(richTextBox.Selection.Start.GetPositionAtOffset(1), richTextBox.Document.ContentEnd);
            }
            else
            {
                searchRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            }

            TextRange foundRange = FindTextInRange(searchRange, pattern);
            if (foundRange == null)
                return false;

            richTextBox.Selection.Select(foundRange.Start, foundRange.End);
            richTextBox.Focus();
            richTextBox.SetPosition();

            return true;
        }

        private TextRange FindTextInRange(TextRange searchRange, string pattern)
        {
            Match match = Rgx.Match(searchRange.Text);

            for (TextPointer start = searchRange.Start.GetPositionAtOffset(match.Index, LogicalDirection.Forward); start != searchRange.End; start = start.GetPositionAtOffset(1))
            {
                return new TextRange(start, searchRange.Start.GetPositionAtOffset(match.Index + match.Length, LogicalDirection.Backward));
            }

            return null;
        }

        #endregion

        private void OnAllSearchClicked(object sender, string pattern)
        {

        }
    }
}

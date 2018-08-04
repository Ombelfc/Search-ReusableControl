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
            if (!pattern.Equals(ToSearch))
            {
                ToSearch = pattern;
                HasNext = false;
                Rgx = new Regex(@pattern);
            }

            HasNext = Search(ToSearchText.rtb, HasNext);
        }

        private bool Search(RichTextBox richTextBox, bool searchNext)
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

            TextRange foundRange = FindMatchInRange(searchRange);
            if (foundRange == null)
                return false;

            richTextBox.Selection.Select(foundRange.Start, foundRange.End);
            richTextBox.Focus();
            richTextBox.SetPosition();

            return true;
        }

        private TextRange FindMatchInRange(TextRange searchRange)
        {
            Match match = Rgx.Match(searchRange.Text);
            if (!match.Success)
                return null;

            for (TextPointer start = searchRange.Start.GetPositionAtOffset(match.Index, LogicalDirection.Forward); start != searchRange.End; start = start.GetPositionAtOffset(1))
            {
                return new TextRange(start, searchRange.Start.GetPositionAtOffset(match.Index + match.Length, LogicalDirection.Backward));
            }

            return null;
        }

        #endregion

        #region Highlight all

        private void OnAllSearchClicked(object sender, string pattern)
        {
            if (!pattern.Equals(ToSearch))
            {
                ToSearch = pattern;
                HasNext = false;
                Rgx = new Regex(@pattern);
            }

            while((HasNext = SearchAll(ToSearchText.rtb, HasNext)));
        }

        private bool SearchAll(RichTextBox richTextBox, bool searchNext)
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

            TextRange foundRange = FindAllMatchInRange(searchRange, searchNext);
            if (foundRange == null)
                return false;

            richTextBox.Selection.Select(foundRange.Start, foundRange.End);
            richTextBox.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, "red");
            richTextBox.Focus();
            richTextBox.SetPosition();

            return true;
        }

        private TextRange FindAllMatchInRange(TextRange searchRange, bool searchNext)
        {
            Match match = Rgx.Match(searchRange.Text);
            if (!match.Success)
                return null;

            if (searchNext)
            {
                for (TextPointer start = searchRange.Start.GetPositionAtOffset(match.Index + 2, LogicalDirection.Forward); start != searchRange.End; start = start.GetPositionAtOffset(1))
                {
                    return new TextRange(start, searchRange.Start.GetPositionAtOffset(match.Index + match.Length + 2, LogicalDirection.Backward));
                }
            }
            else
            {
                for (TextPointer start = searchRange.Start.GetPositionAtOffset(match.Index, LogicalDirection.Forward); start != searchRange.End; start = start.GetPositionAtOffset(1))
                {
                    return new TextRange(start, searchRange.Start.GetPositionAtOffset(match.Index + match.Length, LogicalDirection.Backward));
                }
            }

            return null;
        }

        #endregion
    }
}
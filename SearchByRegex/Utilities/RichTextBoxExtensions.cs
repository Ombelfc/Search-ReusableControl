using SearchByRegex.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SearchByRegex.Utilities
{
    public static class RichTextBoxExtensions
    {
        public static void SetText(this RichTextBox richTextBox, string text)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.Document.Blocks.Add(new Paragraph(new Run(text)));
        }

        public static string GetText(this RichTextBox richTextBox)
        {
            return new TextRange(
                        richTextBox.Document.ContentStart,
                        richTextBox.Document.ContentEnd).Text;
        }

        public static void SetPosition(this RichTextBox richTextBox)
        {
            Rect screenPos = richTextBox.Selection.Start.GetCharacterRect(LogicalDirection.Backward);
            double offset = screenPos.Top + richTextBox.VerticalOffset;
            richTextBox.ScrollToVerticalOffset(offset - richTextBox.ActualHeight / 2);
        }
    }
}

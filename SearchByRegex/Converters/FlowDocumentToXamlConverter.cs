using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;

namespace SearchByRegex.Converters
{
    [ValueConversion(typeof(string), typeof(FlowDocument))]
    public class FlowDocumentToXamlConverter : IValueConverter
    {
        /// <summary>
        /// Converts from string to a WPF FlowDocument
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flowDocument = new FlowDocument();

            if (!String.IsNullOrEmpty((string)value))
            {
                var text = (string) value;
                flowDocument.Blocks.Add(new Paragraph(new Run(text)));
            }

            return flowDocument;
        }

        /// <summary>
        /// Converts from a WPF FlowDocument to a string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return String.Empty;

            var doc = ((FlowDocument) value);
            return new TextRange(doc.ContentStart, doc.ContentEnd).Text;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace SearchByRegex.Controls
{
    /// <summary>
    /// Interaction logic for BindableRichTextBox.xaml
    /// </summary>
    public partial class BindableRichTextBox : UserControl
    {
        private int InternalUpdatePending;
        private bool TextHasChanged;

        public static readonly DependencyProperty DocumentProperty =
                               DependencyProperty.Register("Document",
                                                          typeof(FlowDocument),
                                                          typeof(BindableRichTextBox),
                                                          new PropertyMetadata(OnDocumentChanged));

        public FlowDocument Document
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }

        public BindableRichTextBox()
        {
            InitializeComponent();
        }

        private static void OnDocumentChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = (BindableRichTextBox) dObj;

            if(thisControl.InternalUpdatePending > 0)
            {
                thisControl.InternalUpdatePending--;
                return;
            }

            thisControl.rtb.Document = (e.NewValue == null) ? new FlowDocument() : (FlowDocument) e.NewValue;
            thisControl.TextHasChanged = false;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextHasChanged = true;
        }

        public void UpdateDocumentBindings()
        {
            if (TextHasChanged) return;

            InternalUpdatePending = 2;
            SetValue(DocumentProperty, this.rtb.Document);
        }
    }
}

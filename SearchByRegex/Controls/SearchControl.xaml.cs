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
    /// Interaction logic for SearchControl.xaml
    /// </summary>
    public partial class SearchControl : UserControl
    {
        public static readonly DependencyProperty ButtonContentProperty =
                               DependencyProperty.Register("ButtonContent",
                                                           typeof(string),
                                                           typeof(SearchControl),
                                                           new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty ButtonCommandProperty =
                               DependencyProperty.Register("ButtonCommand",
                                                           typeof(ICommand),
                                                           typeof(SearchControl),
                                                           new PropertyMetadata(null));

        public static readonly DependencyProperty SearchBoxTextProperty =
                               DependencyProperty.Register("SearchBoxText",
                                                           typeof(string),
                                                           typeof(SearchControl),
                                                           new PropertyMetadata(String.Empty));

        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        public ICommand ButtonCommand
        {
            get { return (ICommand)GetValue(ButtonCommandProperty); }
            set { SetValue(ButtonCommandProperty, value); }
        }

        public string SearchBoxText
        {
            get { return (string)GetValue(SearchBoxTextProperty); }
            set { SetValue(SearchBoxTextProperty, value); }
        }

        public SearchControl()
        {
            InitializeComponent();
        }
    }
}

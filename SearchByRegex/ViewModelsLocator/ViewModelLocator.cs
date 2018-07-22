using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SearchByRegex.ViewModelsLocator
{
    public static class ViewModelLocator
    {
        public static readonly DependencyProperty AutoHookedUpViewModelProperty =
                                                        DependencyProperty.RegisterAttached("AutoHookedUpViewModel",
                                                            typeof(bool),
                                                            typeof(ViewModelLocator),
                                                            new PropertyMetadata(false, AutoHookedUpViewModelChanged));

        public static bool GetAutoHookedUpViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoHookedUpViewModelProperty);
        }

        public static void SetAutoHookedUpViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoHookedUpViewModelProperty, value);
        }

        private static void AutoHookedUpViewModelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(obj)) return;

            var viewName = obj.GetType().FullName.Replace(".Views.", ".ViewModels.");
            var viewModelTypeName = viewName + "Model";

            var viewModelType = Type.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType);

            ((FrameworkElement)obj).DataContext = viewModel;
        }
    }
}
using Dawn.ModelView;
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

namespace Dawn.Windows.Pages
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : UserControl
    {
        ViewModel viewModel;
        public NewOrder()
        {
            InitializeComponent();
            DataContextChanged += NewOrder_DataContextChanged;
        }

        private void NewOrder_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ViewModel)
            {
                viewModel = (ViewModel)e.NewValue;
            }
        }

        private void uiCLients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && (sender as ComboBox).SelectedItem != null)
            {
                PriperNewOrder();
            }
        }

        private void PriperNewOrder()
        {
            viewModel.CreateNewOrder(uiCLients.SelectedItem);
        }
        public override string ToString()
        {
            return nameof(NewOrder);
        }
    }
}

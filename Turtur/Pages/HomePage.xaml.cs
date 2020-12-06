using System.Windows;
using System.Windows.Controls;
using Turtur.Pages.Category;

namespace Turtur.Pages
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void CatButtonClickHandler(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new Cats());
        }

        private void CarButtonClickHandler(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new Cars());
        }

        private void CustomerButtonClickHandler(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new Customers());
        }

        private void EmployeButtonClickHandler(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new Employees());
        }

        private void InvestorButtonClickHandler(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new Investors());
        }

        private void MoneyButtonClickHandler(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new Moneys());
        }
    }
}
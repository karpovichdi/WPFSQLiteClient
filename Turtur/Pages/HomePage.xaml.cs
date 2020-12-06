using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
    }
}
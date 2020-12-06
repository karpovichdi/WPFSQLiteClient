using System.Windows;
using Turtur.Pages.Category;

namespace Turtur.Pages
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CatButtonClickHandler (object sender, RoutedEventArgs e)
        {
            var catsPage = new Cats();
            Content = catsPage;
        }
    }
}
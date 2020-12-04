using System.Windows;
using Turtur.Models;
using Turtur.Services.Db;

namespace Turtur.Pages
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var dbCarService = new DbSupplierService();
            
            var c1 = dbCarService.GetAll();

            var cat1 = new Supplier(1, "27.08.2020", "1");
            
            dbCarService.AddNew(cat1);
            
            var c2 = dbCarService.GetAll();
            
            var cat2 = new Supplier(1, "26.08.2020", "7");
            
            dbCarService.UpdateById(cat2);
            
            var c3 = dbCarService.GetAll();
            
            dbCarService.DeleteById(1);
            
            var c4 = dbCarService.GetAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page1 w = new Page1();
            Content = w;
        }
    }
}

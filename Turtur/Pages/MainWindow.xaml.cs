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

            var dbCarService = new DbCustomerService();
            
            var c1 = dbCarService.GetAll();

            var cat1 = new Customer(1,"customer","phone");
            
            dbCarService.AddNew(cat1);
            
            var c2 = dbCarService.GetAll();
            
            var cat2 = new Customer(1,"customer1","phone1");
            
            dbCarService.UpdateById(cat2);
            
            var c3 = dbCarService.GetAll();
            
            dbCarService.DeleteById(1);
            
            var c4 = dbCarService.GetAll();
        }
    }
}

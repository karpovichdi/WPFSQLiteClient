using System.Windows;
using Turtur.Models;
using Turtur.Services.Db;

namespace Turtur.Pages.Add
{
    public partial class AddCustomer : Window
    {
        private DbCustomerService _customerService;

        public AddCustomer()
        {
            InitializeComponent();

            _customerService = new DbCustomerService();
        }

        private void SaveClickHandler(object sender, RoutedEventArgs e)
        {
            var name = nameBox.Text;
            var phone = phoneBox.Text;

            if (string.IsNullOrEmpty(phone))
            {
                phoneBox.Text = "Вы должны ввести телефон";
                return;
            }

            if (string.IsNullOrEmpty(name))
            {
                nameBox.Text = "Вы должны ввести имя";
                return;
            }

            var customer = new Customer(0, name, phone);
            _customerService.AddNew(customer);

            Hide();
        }
    }
}
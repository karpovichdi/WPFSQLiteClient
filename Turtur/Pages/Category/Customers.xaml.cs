using System;
using System.Windows;
using System.Windows.Controls;
using Turtur.Models;
using Turtur.Pages.Add;
using Turtur.Services.Db;
using Turtur.Utills.Helpers;

namespace Turtur.Pages.Category
{
    public partial class Customers : Page
    {
        private DbCustomerService _dbCustomerService;

        private Customer _lastSelectedCustomer;
        private bool _nameBoxChanged;
        private bool _phoneBoxChanged;

        public Customers()
        {
            InitializeComponent();

            _dbCustomerService = new DbCustomerService();

            UpdateListOfCustomers();
        }

        private void AddCustomerClickHandler(object sender, RoutedEventArgs e)
        {
            var page = new AddCustomer();
            page.Show();
        }

        private void RefreshClickHandler(object sender, RoutedEventArgs e)
        {
            UpdateListOfCustomers();
            HideDescription();
        }

        private void UpdateListOfCustomers()
        {
            var customers = _dbCustomerService.GetAll();
            if (customers.Count == 0) customers.Add(new Customer(0, "В базе нет покупателей", ""));

            imageBox.Visibility = Visibility.Hidden;
            listCats.ItemsSource = customers;
        }

        private void CustomerSelectedHandler(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var customer = e.AddedItems[0] as Customer;
                if (customer != null)
                {
                    nameBox.Text = customer.NAME;
                    phoneBox.Text = customer.PHONE;

                    DoReviewInfoReadOnly();

                    _lastSelectedCustomer = customer;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void EditCustomerClickHandler(object sender, RoutedEventArgs e)
        {
            DoReviewInfoEditable();
        }

        private void SaveCustomerClickHandler(object sender, RoutedEventArgs e)
        {
            var name = _lastSelectedCustomer.NAME;
            var phone = _lastSelectedCustomer.PHONE;

            if (_nameBoxChanged)
            {
                name = nameBox.Text;
                if (string.IsNullOrEmpty(name))
                {
                    nameBox.Text = "Вы должны ввести имя";
                    return;
                }
            }

            if (_phoneBoxChanged)
            {
                phone = phoneBox.Text;
                if (string.IsNullOrEmpty(phone))
                {
                    phoneBox.Text = "Вы должны ввести телефон";
                    return;
                }
            }

            var customer = new Customer(_lastSelectedCustomer.Id, name, phone);
            _dbCustomerService.UpdateById(customer);

            DoReviewInfoReadOnly();
            UpdateListOfCustomers();
        }

        private void DeleteClickHandler(object sender, RoutedEventArgs e)
        {
            _dbCustomerService.DeleteById(_lastSelectedCustomer.Id);
            deleteButton.Visibility = Visibility.Hidden;

            UpdateListOfCustomers();
            HideDescription();
        }

        private void DoReviewInfoReadOnly()
        {
            deleteButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Hidden;
            editButton.Visibility = Visibility.Visible;
            imageBox.Visibility = Visibility.Visible;

            nameBox.IsEnabled = false;
            phoneBox.IsEnabled = false;

            _nameBoxChanged = false;
            _phoneBoxChanged = false;
        }

        private void DoReviewInfoEditable()
        {
            deleteButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Visible;
            editButton.Visibility = Visibility.Hidden;
            imageBox.Visibility = Visibility.Visible;

            nameBox.IsEnabled = true;
            phoneBox.IsEnabled = true;

            _nameBoxChanged = false;
            _phoneBoxChanged = false;
        }

        private void HideDescription()
        {
            deleteButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Hidden;
            editButton.Visibility = Visibility.Hidden;

            _nameBoxChanged = false;
            _phoneBoxChanged = false;

            phoneBox.Text = null;
            nameBox.Text = null;

            nameBox.IsEnabled = false;
            phoneBox.IsEnabled = false;
        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _nameBoxChanged = true;
        }

        private void PhoneBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _phoneBoxChanged = true;
        }

        private void GoCategotyPageHandler(object sender, RoutedEventArgs e)
        {
            var item = e.Source as ComboBoxItem;
            if (item != null)
            {
                NavigationHelper.NavigateTo((string)item.Tag);
            }
        }

        private void GoHomeClickHandler(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new HomePage();
        }
    }
}
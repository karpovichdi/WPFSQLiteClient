using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Turtur.Models;
using Turtur.Services.Db;

namespace Turtur.Pages.Add
{
    public partial class AddSales : Window
    {
        private DbCatService _catService;
        private DbCustomerService _customerService;
        private DbMoneyService _moneyService;
        private DbSaleService _saleService;

        private List<Cat> _cats;
        private List<Customer> _customers;

        private Cat _lastSelectedCat;
        private Customer _lastSelectedCustomer;

        public AddSales()
        {
            InitializeComponent();

            _catService = new DbCatService();
            _customerService = new DbCustomerService();
            _saleService = new DbSaleService();
            _moneyService = new DbMoneyService();

            UpdateLists();
        }

        private void UpdateLists()
        {
            try
            {
                _cats = _catService.GetAll();
                _customers = _customerService.GetAll();

                if (_cats.Count == 0) 
                {
                    _cats = new List<Cat>
                    {
                        new Cat(0, "", 0, 0, "В базе еще нет котов")
                    };

                    saveButton.Visibility = Visibility.Hidden;
                }

                if (_customers.Count == 0)
                {
                    _customers = new List<Customer>
                    {
                        new Customer(0, "В базе еще нет покупателей", "")
                    };

                    saveButton.Visibility = Visibility.Hidden;
                }

                if (_customers.Count > 0 && _cats.Count > 0) 
                {
                    saveButton.Visibility = Visibility.Visible;
                }

                if (saveButton.Visibility == Visibility.Visible) 
                {
                    lisCustomers.ItemsSource = _customers;
                    listCats.ItemsSource = _cats;
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
            }
        }

        private void CatSelectedHandler(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var cat = e.AddedItems[0] as Cat;
                if (cat != null)
                {
                    _lastSelectedCat = cat;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void CustomerSelectedHandler(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var customer = e.AddedItems[0] as Customer;
                if (customer != null)
                {
                    _lastSelectedCustomer = customer;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void SaveClickHandler(object sender, RoutedEventArgs e) 
        {
            if (_lastSelectedCat == null || _lastSelectedCustomer == null || string.IsNullOrEmpty(MaterialCalendar.SelectedDate.ToString())) 
            {
                return;
            }

            try
            {
                var sale = new Sale(0, MaterialCalendar.SelectedDate.ToString(), _lastSelectedCustomer.Id, _lastSelectedCat.Id);
                _saleService.AddNew(sale);

                var sales = _saleService.GetAll();
                var salesCount = sales.Count();
                var lastElement = sales.ElementAt(salesCount - 1);

                if (lastElement != null) 
                {
                    var money = new Money(0, "SALE: " + _lastSelectedCat?.Name + " " + sale.Date, _lastSelectedCat.Cost, lastElement.Id);
                    _moneyService.AddNew(money);
                }

                Hide();
            }
            catch (Exception exception) 
            {
                Console.WriteLine(exception);
            }
        }
    }
}

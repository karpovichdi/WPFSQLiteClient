using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Turtur.Models;
using Turtur.Pages.Add;
using Turtur.Services.Db;
using Turtur.Utills.Helpers;

namespace Turtur.Pages.Category
{
    public partial class Sales : Page
    {
        private DbMoneyService _dbMoneyService;
        private DbCatService _dbCatService;
        private DbSaleService _dbSaleService;
        private DbCustomerService _dbCustomerService;

        private Sale _lastSelectedSale;

        private List<Sale> _sales;
        private List<Cat> _cats;
        private List<Money> _moneys;
        private List<Customer> _customers;

        public Sales()
        {
            InitializeComponent();

            _dbMoneyService = new DbMoneyService();
            _dbCatService = new DbCatService();
            _dbSaleService = new DbSaleService();
            _dbCustomerService = new DbCustomerService();

            UpdateList();
        }

        private void AddClickHandler(object sender, RoutedEventArgs e)
        {
            var page = new AddSales();
            page.Show();
        }

        private void RefreshClickHandler(object sender, RoutedEventArgs e)
        {
            UpdateList();
            HideDescription();

            deleteButton.Visibility = Visibility.Hidden;
        }

        private void UpdateList()
        {
            _sales = _dbSaleService.GetAll();
            _cats = _dbCatService.GetAll();
            _moneys = _dbMoneyService.GetAll();
            _customers = _dbCustomerService.GetAll();

            if (_sales.Count == 0)
            {
                _sales.Add(new Sale(0, "В базе нет транзакций", 0, 0));
                HideDescription();
            }
            else 
            {
                foreach (var sale in _sales)
                {
                    var cat = _cats.Find((x) => x.Id == sale.Cat);
                    sale.CatName = cat?.Name;
                    sale.Cost = cat?.Cost.ToString();

                    var customer = _customers.Find((x) => x.Id == sale.Customer);
                    sale.CustomerName = customer?.Name;
                }
            }

            listCats.ItemsSource = _sales;
        }

        private void SaleSelectedHandler(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var sale = e.AddedItems[0] as Sale;
                if (sale != null)
                {
                    if (!string.IsNullOrEmpty(sale.Date)) 
                    {
                        materialCalendar.SelectedDate = Convert.ToDateTime(sale.DATE);
                    }

                    ShowCalendarStack();

                    _lastSelectedSale = sale;

                    deleteButton.Visibility = Visibility.Visible;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void ShowCalendarStack()
        {
            calendarStack.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Hidden;
            moneyStack.Visibility = Visibility.Hidden;
            customerStack.Visibility = Visibility.Hidden;
            catDescriptionStack.Visibility = Visibility.Hidden;
        }

        private void CatDescriptionClickHandler(object sender, RoutedEventArgs e)
        {
            ShowCatDescriptionStack();
        }

        private void ShowCatDescriptionStack()
        {
            try
            {
                calendarStack.Visibility = Visibility.Hidden;
                backButton.Visibility = Visibility.Visible;
                moneyStack.Visibility = Visibility.Hidden;
                customerStack.Visibility = Visibility.Hidden;
                catDescriptionStack.Visibility = Visibility.Visible;

                var cat = _cats.Find((x) => x.Id == _lastSelectedSale.Cat);
                if (cat != null)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(cat.Photo))
                        {
                            imageReview.Source = new BitmapImage(new Uri(cat.Photo));
                        }
                        else
                        {
                            imageReview.Source = null;
                        }
                    }
                    catch (Exception exception)
                    {
                        cat.Photo = "";
                        _dbCatService.UpdateById(cat);
                    }

                    nameBox.Text = cat.Name;
                    weightBox.Text = cat.Weight.ToString();
                    costBox.Text = cat.Cost.ToString();
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
            }
        }

        private void CustomerDescriptionClickHandler(object sender, RoutedEventArgs e) 
        {
            ShowCustomerDescriptionStack();
        }

        private void ShowCustomerDescriptionStack()
        {
            try
            {
                calendarStack.Visibility = Visibility.Hidden;
                backButton.Visibility = Visibility.Visible;
                moneyStack.Visibility = Visibility.Hidden;
                customerStack.Visibility = Visibility.Visible;
                catDescriptionStack.Visibility = Visibility.Hidden;

                var customer = _customers.Find((x) => x.Id == _lastSelectedSale.Customer);
                if (customer != null) 
                {
                    customerNameBox.Text = customer.Name;
                    phoneBox.Text = customer.Phone;
                }
            }
            catch (Exception e) 
            {
                Console.Write(e);
            }
        }

        private void MoneyDescriptionClickHandler(object sender, RoutedEventArgs e)
        {
            ShowMoneyDescriptionStack();
        }

        private void ShowMoneyDescriptionStack()
        {
            try
            {
                calendarStack.Visibility = Visibility.Hidden;
                backButton.Visibility = Visibility.Visible;
                moneyStack.Visibility = Visibility.Visible;
                customerStack.Visibility = Visibility.Hidden;
                catDescriptionStack.Visibility = Visibility.Hidden;

                var money = _moneys.Find((x) => x.Sale == _lastSelectedSale?.Id);
                if (money != null)
                {
                    transactionNameBox.Text = money.TransactionName;
                    moneyCostBox.Text = money?.Cost.ToString();
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        private void HideDescriptionClickHandler(object sender, RoutedEventArgs e) 
        {
            ShowCalendarStack();
        }

        private void DeleteClickHandler(object sender, RoutedEventArgs e)
        {
            try
            {
                _dbSaleService.DeleteById(_lastSelectedSale.Id);
                deleteButton.Visibility = Visibility.Hidden;

                UpdateList();
                HideDescription();
            }
            catch (Exception exc) 
            {
                Console.WriteLine(exc);
            }
        }

        private void HideDescription()
        {
            calendarStack.Visibility = Visibility.Hidden;
            backButton.Visibility = Visibility.Hidden;
            moneyStack.Visibility = Visibility.Hidden;
            customerStack.Visibility = Visibility.Hidden;
            catDescriptionStack.Visibility = Visibility.Hidden;
        }

        private void GoCategoryPageHandler(object sender, RoutedEventArgs e)
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

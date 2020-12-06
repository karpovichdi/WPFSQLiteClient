using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Turtur.Models;
using Turtur.Pages.Add;
using Turtur.Services.Db;
using Turtur.Utills.Helpers;

namespace Turtur.Pages.Category
{
    public partial class Moneys : Page
    {
        private DbMoneyService _dbMoneyService;
        private DbCatService _dbCatService;
        private DbSaleService _dbSaleService;

        private Money _lastSelectedMoneys;
        private bool _costBoxChanged;
        private bool _transactionNameBoxChanged;

        public Moneys()
        {
            InitializeComponent();

            _dbMoneyService = new DbMoneyService();
            _dbCatService = new DbCatService();
            _dbSaleService = new DbSaleService();

            UpdateList();
        }

        private void AddClickHandler(object sender, RoutedEventArgs e)
        {
            var page = new AddMoney();
            page.Show();
        }

        private void RefreshClickHandler(object sender, RoutedEventArgs e)
        {
            UpdateList();
            HideDescription();
        }

        private void UpdateList()
        {
            var moneys = _dbMoneyService.GetAll();
            if (moneys.Count == 0) moneys.Add(new Money(0, "В базе нет транзакций", 0, 0));

            imageBox.Visibility = Visibility.Hidden;
            listCats.ItemsSource = moneys;
        }

        private void MoneySelectedHandler(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var money = e.AddedItems[0] as Money;
                if (money != null)
                {
                    transactionNameBox.Text = money.TRANSACTIONNAME;
                    costBox.Text = money.COST.ToString();

                    DoReviewInfoReadOnly();

                    _lastSelectedMoneys = money;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void EditClickHandler(object sender, RoutedEventArgs e)
        {
            DoReviewInfoEditable();
        }

        private void CatDescriptionClickHandler(object sender, RoutedEventArgs e)
        { 
            ///////////////////////
        }

        private void SaveClickHandler(object sender, RoutedEventArgs e)
        {
            var transactionName = _lastSelectedMoneys.TRANSACTIONNAME;
            int cost = _lastSelectedMoneys.COST;

            if (_transactionNameBoxChanged)
            {
                transactionName = transactionNameBox.Text;
                if (string.IsNullOrEmpty(transactionName))
                {
                    transactionNameBox.Text = "Вы должны ввести имя транзакции";
                    return;
                }
            }

            try
            {
                if (_costBoxChanged)
                {
                    cost = StringHelper.GetIntFromString(costBox.Text);
                    if (cost == -1) throw new Exception();
                }
            }
            catch (Exception exception)
            {
                costBox.Text = "Можно вводить только числа";
                return;
            }

            var money = new Money(_lastSelectedMoneys.Id, transactionName, cost, 0);
            _dbMoneyService.UpdateById(money);

            DoReviewInfoReadOnly();
            UpdateList();
        }

        private void DeleteClickHandler(object sender, RoutedEventArgs e)
        {
            _dbMoneyService.DeleteById(_lastSelectedMoneys.Id);
            deleteButton.Visibility = Visibility.Hidden;

            UpdateList();
            HideDescription();
        }

        private void DoReviewInfoReadOnly()
        {
            deleteButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Hidden;
            editButton.Visibility = Visibility.Visible;
            imageBox.Visibility = Visibility.Visible;

            transactionNameBox.IsEnabled = false;
            costBox.IsEnabled = false;

            _costBoxChanged = false;
            _transactionNameBoxChanged = false;
        }

        private void DoReviewInfoEditable()
        {
            deleteButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Visible;
            editButton.Visibility = Visibility.Hidden;
            imageBox.Visibility = Visibility.Visible;

            transactionNameBox.IsEnabled = true;
            costBox.IsEnabled = true;

            _costBoxChanged = false;
            _transactionNameBoxChanged = false;
        }

        private void HideDescription()
        {
            deleteButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Hidden;
            editButton.Visibility = Visibility.Hidden;

            _costBoxChanged = false;
            _transactionNameBoxChanged = false;

            costBox.Text = null;
            transactionNameBox.Text = null;

            transactionNameBox.IsEnabled = false;
            costBox.IsEnabled = false;
        }

        private void TransactionNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _transactionNameBoxChanged = true;
        }

        private void CostBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _costBoxChanged = true;
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

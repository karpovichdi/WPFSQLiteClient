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
    /// <summary>
    /// Interaction logic for Investors.xaml
    /// </summary>
    public partial class Investors : Page
    {
        private DbInvestorService _dbInvestorService;
        private Investor _lastSelectedInvestor;
        private bool _nameBoxChanged;

        public Investors()
        {
            InitializeComponent();

            _dbInvestorService = new DbInvestorService();

            UpdateList();
        }

        private void AddClickHandler(object sender, RoutedEventArgs e)
        {
            var page = new AddInvestor();
            page.Show();
        }

        private void RefreshClickHandler(object sender, RoutedEventArgs e)
        {
            UpdateList();
            HideDescription();
        }

        private void UpdateList()
        {
            var investor = _dbInvestorService.GetAll();
            if (investor.Count == 0) investor.Add(new Investor(0, "В базе нет инвесторов"));

            imageBox.Visibility = Visibility.Hidden;
            listCats.ItemsSource = investor;
        }

        private void CustomerSelectedHandler(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var investor = e.AddedItems[0] as Investor;
                if (investor != null)
                {
                    nameBox.Text = investor.NAME;

                    DoReviewInfoReadOnly();

                    _lastSelectedInvestor = investor;
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

        private void SaveClickHandler(object sender, RoutedEventArgs e)
        {
            var name = _lastSelectedInvestor.Name;

            if (_nameBoxChanged)
            {
                name = nameBox.Text;
                if (string.IsNullOrEmpty(name))
                {
                    nameBox.Text = "Вы должны ввести название организации";
                    return;
                }
            }

            var investor = new Investor(_lastSelectedInvestor.Id, name);
            _dbInvestorService.UpdateById(investor);

            DoReviewInfoReadOnly();
            UpdateList();
        }

        private void DeleteClickHandler(object sender, RoutedEventArgs e)
        {
            _dbInvestorService.DeleteById(_lastSelectedInvestor.Id);
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

            nameBox.IsEnabled = false;

            _nameBoxChanged = false;
        }

        private void DoReviewInfoEditable()
        {
            deleteButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Visible;
            editButton.Visibility = Visibility.Hidden;
            imageBox.Visibility = Visibility.Visible;

            nameBox.IsEnabled = true;
            _nameBoxChanged = false;
        }

        private void HideDescription()
        {
            deleteButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Hidden;
            editButton.Visibility = Visibility.Hidden;

            nameBox.Text = null;

            _nameBoxChanged = false;
            nameBox.IsEnabled = false;
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _nameBoxChanged = true;
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
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
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : Page
    {
        private DbEmployeService _dbEmployeService;
        private Employe _lastSelectedEmploye;
        private bool _firstNameBoxChanged;
        private bool _lastNameBoxChanged;

        public Employees()
        {
            InitializeComponent();

            _dbEmployeService = new DbEmployeService();

            UpdateList();
        }

        private void AddClickHandler(object sender, RoutedEventArgs e)
        {
            var page = new AddEmploye();
            page.Show();
        }

        private void RefreshClickHandler(object sender, RoutedEventArgs e)
        {
            UpdateList();
            HideDescription();
        }

        private void UpdateList()
        {
            var employees = _dbEmployeService.GetAll();
            if (employees.Count == 0) employees.Add(new Employe(0, "В базе нет сотрудников", ""));

            imageBox.Visibility = Visibility.Hidden;
            listCats.ItemsSource = employees;
        }

        private void CustomerSelectedHandler(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var employe = e.AddedItems[0] as Employe;
                if (employe != null)
                {
                    firstNameBox.Text = employe.FIRSTNAME;
                    lastNameBox.Text = employe.LASTNAME;

                    DoReviewInfoReadOnly();

                    _lastSelectedEmploye = employe;
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
            var firstName = _lastSelectedEmploye.FIRSTNAME;
            var lastName = _lastSelectedEmploye.LASTNAME;

            if (_firstNameBoxChanged)
            {
                firstName = firstNameBox.Text;
                if (string.IsNullOrEmpty(firstName))
                {
                    firstNameBox.Text = "Вы должны ввести имя";
                    return;
                }
            }

            if (_lastNameBoxChanged)
            {
                lastName = lastNameBox.Text;
                if (string.IsNullOrEmpty(lastName))
                {
                    lastNameBox.Text = "Вы должны ввести телефон";
                    return;
                }
            }

            var employe = new Employe(_lastSelectedEmploye.Id, firstName, lastName);
            _dbEmployeService.UpdateById(employe);

            DoReviewInfoReadOnly();
            UpdateList();
        }

        private void DeleteClickHandler(object sender, RoutedEventArgs e)
        {
            _dbEmployeService.DeleteById(_lastSelectedEmploye.Id);
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

            firstNameBox.IsEnabled = false;
            lastNameBox.IsEnabled = false;

            _firstNameBoxChanged = false;
            _lastNameBoxChanged = false;
        }

        private void DoReviewInfoEditable()
        {
            deleteButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Visible;
            editButton.Visibility = Visibility.Hidden;
            imageBox.Visibility = Visibility.Visible;

            firstNameBox.IsEnabled = true;
            lastNameBox.IsEnabled = true;

            _firstNameBoxChanged = false;
            _lastNameBoxChanged = false;
        }

        private void HideDescription()
        {
            deleteButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Hidden;
            editButton.Visibility = Visibility.Hidden;

            _firstNameBoxChanged = false;
            _lastNameBoxChanged = false;

            lastNameBox.Text = null;
            firstNameBox.Text = null;

            firstNameBox.IsEnabled = false;
            lastNameBox.IsEnabled = false;
        }

        private void FirstNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _firstNameBoxChanged = true;
        }

        private void LastNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _lastNameBoxChanged = true;
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

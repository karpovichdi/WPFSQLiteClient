using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Turtur.Models;
using Turtur.Pages.Add;
using Turtur.Services.Db;
using Turtur.Utills.Helpers;

namespace Turtur.Pages.Category
{
    /// <summary>
    /// Interaction logic for Cars.xaml
    /// </summary>
    public partial class Cars : Page
    {
        private DbCarService _dbCarService;

        private Car _lastSelectedCar;
        private bool _photoLinkChanged;
        private bool _nameBoxChanged;
        private bool _stateNumberBoxChanged;

        public Cars()
        {
            InitializeComponent();

            _dbCarService = new DbCarService();

            UpdateListOfCars();
        }

        private void AddCarClickHandler(object sender, RoutedEventArgs e)
        {
            var page = new AddCar();
            page.Show();
        }

        private void RefreshClickHandler(object sender, RoutedEventArgs e)
        {
            UpdateListOfCars();
            HideDescription();
        }

        private void UpdateListOfCars()
        {
            var cars = _dbCarService.GetAll();
            if (cars.Count == 0) cars.Add(new Car(0, "В базе нет котов", "", ""));
            listCats.ItemsSource = cars;
        }

        private void CarSelectedHandler(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var car = e.AddedItems[0] as Car;
                if (car != null)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(car.Photo))
                        {
                            imageReview.Source = new BitmapImage(new Uri(car.Photo));
                        }
                        else
                        {
                            imageReview.Source = null;
                        }
                    }
                    catch (Exception exception)
                    {
                        car.Photo = "";
                        _dbCarService.UpdateById(car);
                    }

                    nameBox.Text = car.NAME;
                    stateNumberBox.Text = car.STATENUMBER;

                    DoReviewInfoReadOnly();

                    _lastSelectedCar = car;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                imageReview.Source = null;
            }
        }

        private void EditCatClickHandler(object sender, RoutedEventArgs e)
        {
            DoReviewInfoEditable();
        }

        private void SaveCatClickHandler(object sender, RoutedEventArgs e)
        {
            var name = _lastSelectedCar.NAME;
            var photo = _lastSelectedCar.PHOTO;
            var stateNumber = _lastSelectedCar.STATENUMBER;

            if (_photoLinkChanged)
            {
                photo = photoLinkBox.Text;
                if (string.IsNullOrEmpty(photo))
                {
                    photo = "";
                }
            }

            if (_nameBoxChanged)
            {
                name = nameBox.Text;
                if (string.IsNullOrEmpty(name))
                {
                    nameBox.Text = "Вы должны ввести имя и породу";
                    return;
                }
            }

            if (_stateNumberBoxChanged)
            {
                stateNumber = stateNumberBox.Text;
                if (string.IsNullOrEmpty(stateNumber))
                {
                    stateNumberBox.Text = "Вы должны ввести гос номер";
                    return;
                }
            }

            var cat = new Car(_lastSelectedCar.Id, name, photo, stateNumber);
            _dbCarService.UpdateById(cat);

            DoReviewInfoReadOnly();
            UpdateListOfCars();
        }

        private void DeleteCarClickHandler(object sender, RoutedEventArgs e)
        {
            _dbCarService.DeleteById(_lastSelectedCar.Id);
            deleteCat.Visibility = Visibility.Hidden;

            UpdateListOfCars();
            HideDescription();
        }

        private void DoReviewInfoReadOnly()
        {
            deleteCat.Visibility = Visibility.Hidden;
            saveCat.Visibility = Visibility.Hidden;
            editCat.Visibility = Visibility.Visible;
            photoLinkBox.IsEnabled = false;
            photoLinkBox.Visibility = Visibility.Hidden;
            nameBox.IsEnabled = false;
            stateNumberBox.IsEnabled = false;

            _nameBoxChanged = false;
            _photoLinkChanged = false;
            _stateNumberBoxChanged = false;
        }

        private void DoReviewInfoEditable()
        {
            deleteCat.Visibility = Visibility.Visible;
            saveCat.Visibility = Visibility.Visible;
            editCat.Visibility = Visibility.Hidden;
            photoLinkBox.IsEnabled = true;
            photoLinkBox.Visibility = Visibility.Visible;
            nameBox.IsEnabled = true;
            stateNumberBox.IsEnabled = true;

            _nameBoxChanged = false;
            _photoLinkChanged = false;
            _stateNumberBoxChanged = false;
        }

        private void HideDescription()
        {
            deleteCat.Visibility = Visibility.Hidden;

            _nameBoxChanged = false;
            _photoLinkChanged = false;
            _stateNumberBoxChanged = false;

            saveCat.Visibility = Visibility.Hidden;
            editCat.Visibility = Visibility.Hidden;

            photoLinkBox.Text = null;
            stateNumberBox.Text = null;
            nameBox.Text = null;

            photoLinkBox.IsEnabled = false;
            nameBox.IsEnabled = false;
            stateNumberBox.IsEnabled = false;
        }

        private void photoLinkBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _photoLinkChanged = true;
        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _nameBoxChanged = true;
        }

        private void stateNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _stateNumberBoxChanged = true;
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

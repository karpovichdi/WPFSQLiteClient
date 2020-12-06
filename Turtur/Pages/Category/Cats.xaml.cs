using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Turtur.Models;
using Turtur.Pages.Add;
using Turtur.Services.Db;
using Turtur.Utills;
using Turtur.Utills.Helpers;

namespace Turtur.Pages.Category
{
    public partial class Cats : Page
    {
        private DbCatService _dbCatService;

        private Cat _lastSelectedCat;
        private bool _photoLinkChanged;
        private bool _nameBoxChanged;
        private bool _weightBoxChanged;
        private bool _costBoxChanged;

        public Cats()
        {
            InitializeComponent();

            _dbCatService = new DbCatService();

            UpdateListOfCats();
        }

        private void AddCatClickHandler(object sender, RoutedEventArgs e)
        {
            var page = new AddCat();
            page.Show();
        }

        private void RefreshClickHandler(object sender, RoutedEventArgs e)
        {
            UpdateListOfCats();

            HideDescription();
        }

        private void UpdateListOfCats() 
        {
            var cats = _dbCatService.GetAll();

            if (cats.Count == 0) cats.Add(new Cat(0, "", 0, 0, "В базе нет котов"));

            listCats.ItemsSource = cats;
        }

        private void CatSelectedHandler(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var cat = e.AddedItems[0] as Cat;
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

                    nameBox.Text = cat.NAME;
                    weightBox.Text = cat.WEIGHT.ToString();
                    costBox.Text = cat.COST.ToString();

                    DoReviewInfoReadOnly();

                    _lastSelectedCat =  cat;
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
            var costInt = _lastSelectedCat.COST;
            var weightInt = _lastSelectedCat.WEIGHT;
            var name = _lastSelectedCat.NAME;
            var photo = _lastSelectedCat.PHOTO;

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

            try
            {
                if (_weightBoxChanged) 
                {
                    var weight = weightBox.Text;
                    
                    weightInt = StringHelper.GetIntFromString(weight);
                    if (weightInt == -1) throw new Exception();
                }
            }
            catch (Exception exception)
            {
                weightBox.Text = "Можно вводить только числа";
                return;
            }

            try
            {
                if (_costBoxChanged) 
                {
                    var cost = costBox.Text;

                    costInt = StringHelper.GetIntFromString(cost);
                    if (costInt == -1) throw new Exception();
                }
            }
            catch (Exception exception)
            {
                costBox.Text = "Можно вводить только числа";
                return;
            }

            var cat = new Cat(_lastSelectedCat.Id, photo, weightInt, costInt, name);
            _dbCatService.UpdateById(cat);

            DoReviewInfoReadOnly();
            UpdateListOfCats();
        }

        private void DeleteCatClickHandler(object sender, RoutedEventArgs e) 
        {
            _dbCatService.DeleteById(_lastSelectedCat.Id);
            deleteCat.Visibility = Visibility.Hidden;

            UpdateListOfCats();
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
            weightBox.IsEnabled = false;
            costBox.IsEnabled = false;

            _costBoxChanged = false;
            _nameBoxChanged = false;
            _photoLinkChanged = false;
            _weightBoxChanged = false;
        }

        private void DoReviewInfoEditable() 
        {
            deleteCat.Visibility = Visibility.Visible;
            saveCat.Visibility = Visibility.Visible;
            editCat.Visibility = Visibility.Hidden;
            photoLinkBox.IsEnabled = true;
            photoLinkBox.Visibility = Visibility.Visible;
            nameBox.IsEnabled = true;
            weightBox.IsEnabled = true;
            costBox.IsEnabled = true;

            _costBoxChanged = false;
            _nameBoxChanged = false;
            _photoLinkChanged = false;
            _weightBoxChanged = false;
        }

        private void HideDescription()
        {
            deleteCat.Visibility = Visibility.Hidden;

            _costBoxChanged = false;
            _nameBoxChanged = false;
            _photoLinkChanged = false;
            _weightBoxChanged = false;

            saveCat.Visibility = Visibility.Hidden;
            editCat.Visibility = Visibility.Hidden;

            photoLinkBox.Text = null;
            costBox.Text = null;
            weightBox.Text = null;
            nameBox.Text = null;

            photoLinkBox.IsEnabled = false;
            nameBox.IsEnabled = false;
            weightBox.IsEnabled = false;
            costBox.IsEnabled = false;
        }

        private void photoLinkBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _photoLinkChanged = true;
        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _nameBoxChanged = true;
        }

        private void weightBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _weightBoxChanged = true;
        }

        private void costBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _costBoxChanged = true;
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
            //var mainWindowPage = new MainWindow();
            //Content = mainWindowPage;
        }
    }
}
using Turtur.Pages.Category;

namespace Turtur.Utills.Helpers
{
    public static class NavigationHelper
    {
        public static void NavigateTo(string pageName) 
        {
            switch (pageName)
            {
                case Constants.TableNames.Cars:
                    App.Current.MainWindow.Content = new Cars();
                    break;
                case Constants.TableNames.Customer:
                    App.Current.MainWindow.Content = new Customers();
                    break;
                case Constants.TableNames.Employee:
                    App.Current.MainWindow.Content = new Employees();
                    break;
                case Constants.TableNames.Investors:
                    App.Current.MainWindow.Content = new Investors();
                    break;
                case Constants.TableNames.Money:
                    App.Current.MainWindow.Content = new Moneys();
                    break;
                case Constants.TableNames.Resources:
                    break;
                case Constants.TableNames.Sales:
                    break;
                case Constants.TableNames.Suppliers:
                    break;
                case Constants.TableNames.Cats:
                    App.Current.MainWindow.Content = new Cats();
                    break;
            }
        }
    }
}
namespace Turtur.Utills.Helpers
{
    public static class NavigationHelper
    {
        public static void NavigateTo(string pageName) 
        {
            switch (pageName)
            {
                case Constants.TableNames.Cars:
                    //var catsPage = new Cats();
                    //Content = catsPage;
                    break;
                case Constants.TableNames.Customer:
                    break;
                case Constants.TableNames.Employee:
                    break;
                case Constants.TableNames.Investors:
                    break;
                case Constants.TableNames.Money:
                    break;
                case Constants.TableNames.Resources:
                    break;
                case Constants.TableNames.Sales:
                    break;
                case Constants.TableNames.Suppliers:
                    break;
                case Constants.TableNames.Cats:
                    break;
            }
        }
    }
}
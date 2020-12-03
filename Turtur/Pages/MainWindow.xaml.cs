using System.Windows;
using Turtur.Services.Db;

namespace Turtur.Pages
{
    public partial class MainWindow : Window
    {
        private readonly DbCatService _dbCatService;
        
        public MainWindow()
        {
            InitializeComponent();

            _dbCatService = new DbCatService();
            
            _dbCatService.AddNewCat("NotIgor", 16, 7);
            
            var cats = _dbCatService.GetAllCats();
        }
    }
}

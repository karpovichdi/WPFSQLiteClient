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
            
            var cats = _dbCatService.GetAllCats();
        }
    }
}

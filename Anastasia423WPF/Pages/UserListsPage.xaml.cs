using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Anastasia423WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserListsPage.xaml
    /// </summary>
    public partial class UserListsPage : Page
    {
        public user_ currentUser { get; set; }
        public int currentStatus = 1;
        public UserListsPage(user_ user)
        {
            InitializeComponent();
            currentUser = user;
        }
    }
}

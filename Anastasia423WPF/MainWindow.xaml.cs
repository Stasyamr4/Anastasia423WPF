using System.Windows;
using System.Windows.Controls;
using Anastasia423WPF.Pages;

namespace Anastasia423WPF
{
    public partial class MainWindow : Window
    {
        public user_ CurrentUser { get; set; }

        public MainWindow(user_ user)
        {
            InitializeComponent();
            CurrentUser = user;
            SetupSidebar();
            if (user.RoleID == 3)
            {
                BtnAdmin.Visibility = Visibility.Visible;
            }
            else if (user.RoleID == 2)
            {
                BtnAuthor.Visibility = Visibility.Visible;
            }
            else
            {
                BtnAdmin.Visibility = Visibility.Collapsed;
                BtnAuthor.Visibility = Visibility.Collapsed;
            }
            MainFrame.Navigate(new CatalogPage(CurrentUser));
        }
        private void SetupSidebar()
        {
            if (CurrentUser == null) return;
            if (CurrentUser.RoleID == 2)
            {
                BtnAuthor.Visibility = Visibility.Visible;
            }
            else if (CurrentUser.RoleID == 3)
            {
                BtnAdmin.Visibility = Visibility.Visible;
            }
        }

        private void BtnCatalog_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CatalogPage(CurrentUser));
        }

        private void BtnLists_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListBookPage(CurrentUser));
        }

        private void BtnAuthor_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AuthorPage(CurrentUser));
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminPage(CurrentUser));
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage(CurrentUser));
        }
    }
}
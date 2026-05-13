using System.Windows;
using System.Windows.Controls;
using Anastasia423WPF.Pages;

namespace Anastasia423WPF
{
    public partial class MainWindow : Window
    {
        private user_ currentUser;

        public MainWindow(user_ user)
        {
            InitializeComponent();
            currentUser = user;

            // Настройка видимости пунктов меню по роли
            if (currentUser.RoleID == 3) // Admin
                BtnAdmin.Visibility = Visibility.Visible;

            if (currentUser.RoleID == 2) // Author
                BtnAuthor.Visibility = Visibility.Visible;

            if (currentUser.IsFreeze == true)
                BtnFreezeWarning.Visibility = Visibility.Visible;

            // По умолчанию – каталог
            MainFrame.Navigate(new CatalogPage(currentUser));
        }

        private void NavigateTo(Page page)
        {
            MainFrame.Navigate(page);
        }

        private void BtnCatalog_Click(object sender, RoutedEventArgs e) => NavigateTo(new CatalogPage(currentUser));
        private void BtnLists_Click(object sender, RoutedEventArgs e) => NavigateTo(new UserListsPage(currentUser));
        private void BtnAdmin_Click(object sender, RoutedEventArgs e) => NavigateTo(new AdminPage(currentUser));
        private void BtnAuthor_Click(object sender, RoutedEventArgs e) => NavigateTo(new AuthorPage(currentUser));
        private void BtnFreezeWarning_Click(object sender, RoutedEventArgs e) => NavigateTo(new ProfilePage(currentUser, true));
        private void BtnProfile_Click(object sender, RoutedEventArgs e) => NavigateTo(new ProfilePage(currentUser));
    }
}
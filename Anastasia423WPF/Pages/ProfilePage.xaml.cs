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
using Anastasia423WPF.Windows;

namespace Anastasia423WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        user_ currentUser;
        public ProfilePage(user_ user)
        {
            InitializeComponent();
            currentUser = user;
            TBlockFIO.Text = $"ФИО: {user.Name}";
            TBlockEmail.Text = $"Email: {user.Email}";
            TBlockRole.Text = $"Статус: {user.role.Name}";
            var myReviews = Core.Context.review
                .Where(r => r.UserID == user.ID)
                .ToList().Select(r => new {
                    BookName = r.book.Name,
                    BookID = r.BookID,
                    Rating = r.Mark,
                    Description = r.Description
                }).ToList();
            LBoxMyReviews.ItemsSource = myReviews;
            if (user.RoleID != 1)
            {
                RequestButton.Visibility = Visibility.Collapsed;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AuthenticWindow auth = new AuthenticWindow();
            auth.Show();
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }
        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            var reviewWin = new RequestWindow(currentUser.ID);
            reviewWin.Owner = Window.GetWindow(this);
            if (reviewWin.ShowDialog() == true)
            {

            }
        }
        private void LBoxMyReviews_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedReview = LBoxMyReviews.SelectedItem;
            if (selectedReview == null) { return; }
            dynamic data = selectedReview;
            int? bID = data.BookID;

            if (bID != null)
            {
                var book = Core.Context.book.FirstOrDefault(b => b.ID == bID);
                if (book != null)
                {
                    NavigationService.Navigate(new DetailsBookPage(book, currentUser));
                }
            }
        }
    }
}

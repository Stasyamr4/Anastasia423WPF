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
    /// Логика взаимодействия для DetailsBookPage.xaml
    /// </summary>
    public partial class DetailsBookPage : Page
    {
        private book _currentBook;
        private user_ _currentUser;

        public DetailsBookPage(book selectedBook, user_ user)
        {
            _currentBook = selectedBook;
            _currentUser = user;
            this.DataContext = _currentBook;
            InitializeComponent();
            LoadReviews();
        }

        private void LoadReviews()
        {
            var reviews = Core.Context.review.Where(r => r.BookID == _currentBook.ID);
            if (_currentUser.RoleID != 3) reviews = reviews.Where(r => r.IsFreeze != true);
            LBoxReviews.ItemsSource = reviews.ToList();
        }

        private void RefreshBookData()
        {
            Core.Context.Entry(_currentBook).Reload();
            TxtBlockRating.Text = $"Рейтинг: {_currentBook.Rating} / 5";
            var freshReviews = Core.Context.review
                .Include("user_")
                .Where(r => r.BookID == _currentBook.ID)
                .ToList();

            LBoxReviews.ItemsSource = freshReviews;
        }
        private void ReportBook_Click(object sender, RoutedEventArgs e)
        {
            Core.Context.report.Add(new report
            {
                userID = _currentUser.ID,
                BookID = _currentBook.ID,
                reviewID = null,
                AuthorID = null,
                reportDate = DateTime.Now
            });
            Core.Context.SaveChanges();
            MessageBox.Show("Жалоба на книгу отправлена модераторам");
        }
        private void ReportAuthor_Click(object sender, RoutedEventArgs e)
        {
            var author = Core.Context.user_.Where(u => u.ID == _currentBook.AuthorID).FirstOrDefault();
            Core.Context.report.Add(new report
            {
                userID = _currentUser.ID,
                AuthorID = author.ID,
                reviewID = null,
                BookID = _currentBook.ID,
                reportDate = DateTime.Now
            });
            Core.Context.SaveChanges();
            MessageBox.Show($"Жалоба на автора {_currentBook.user_.Name} отправлена");
        }
        private void ReportReview_Click(object sender, RoutedEventArgs e)
        {
            var rev = (sender as Button).Tag as review;
            Core.Context.report.Add(new report
            {
                userID = _currentUser.ID,
                reviewID = rev.ID,
                userWasReportedID = rev.UserID,
                BookID = null,
                AuthorID = null,
                reportDate = DateTime.Now
            });
            Core.Context.SaveChanges();
            MessageBox.Show("Жалоба на отзыв принята");
        }
        private void AdminFreeze_Click(object sender, RoutedEventArgs e)
        {
            _currentBook.IsFreeze = !(_currentBook.IsFreeze == true);
            Core.Context.SaveChanges();
            NavigationService.GoBack();
        }
        private void AdminFreezeReview_Click(object sender, RoutedEventArgs e)
        {
            var rev = (sender as Button).Tag as review;
            rev.IsFreeze = !(rev.IsFreeze == true);
            Core.Context.SaveChanges();
            LoadReviews();
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReadPage(_currentBook.ID, _currentUser));
        }
        private void ReviewBut_Click(object sender, RoutedEventArgs e)
        {
            var reviewWin = new ReviewWindow(_currentBook.ID, _currentUser.ID);
            reviewWin.Owner = Window.GetWindow(this);
            if (reviewWin.ShowDialog() == true)
            {

            }
            RefreshBookData();
        }
        private void ComboStatus_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.ItemsSource = Core.Context.readStatus.ToList();
            int bookId = (int)combo.Tag;
            var currentStatus = Core.Context.readList
                .FirstOrDefault(r => r.BookID == bookId && r.UserID == _currentUser.ID);
            if (currentStatus != null)
            {
                combo.SelectedValue = currentStatus.ReadStatusID;
            }
        }

        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo.SelectedValue == null) return;
            int bookId = (int)combo.Tag;
            int selectedStatusId = (int)combo.SelectedValue; // Получаем ID из ComboBox
            var record = Core.Context.readList.FirstOrDefault(r => r.BookID == bookId && r.UserID == _currentUser.ID);

            if (record != null)
            {
                record.ReadStatusID = selectedStatusId;
            }
            else
            {
                Core.Context.readList.Add(new readList
                {
                    UserID = _currentUser.ID,
                    BookID = bookId,
                    ReadStatusID = selectedStatusId
                });
            }
            Core.Context.SaveChanges();
        }
    }
}

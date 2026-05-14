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
using System.Windows.Shapes;

namespace Anastasia423WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReviewWindow.xaml
    /// </summary>
    public partial class ReviewWindow : Window
    {
        private int _bookId;
        private int _userId;
        book _currentBook;

        public ReviewWindow(int bookId, int userId)
        {
            InitializeComponent();
            _bookId = bookId;
            _userId = userId;
            book currentBook = Core.Context.book.Where(b => b.ID == bookId).FirstOrDefault();
            _currentBook = currentBook;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBoxReviewText.Text))
            {
                MessageBox.Show("Напишите текст отзыва!");
                return;
            }
            try
            {
                var newReview = new review
                {
                    BookID = _bookId,
                    UserID = _userId,
                    Description = TBoxReviewText.Text,
                    Mark = int.Parse((ComboRating.SelectedItem as ComboBoxItem).Content.ToString()),
                    Date = DateTime.Now
                };
                Core.Context.review.Add(newReview);
                Core.Context.SaveChanges();
                var allReviews = Core.Context.review.Where(r => r.BookID == _bookId).ToList();
                if (allReviews.Any())
                {
                    double averageRating = allReviews.Average(r => r.Mark);
                    _currentBook.Rating = Math.Round(averageRating, 1);
                    Core.Context.SaveChanges();
                }
                MessageBox.Show("Отзыв успешно добавлен!");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

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
    /// Логика взаимодействия для BookEditWindow.xaml
    /// </summary>
    public partial class BookEditWindow : Window
    {
        public book currentBook;
        private bool _isEdit = false;
        public user_ currentUser;

        public BookEditWindow(book selectedBook, user_ us)
        {
            InitializeComponent();
            currentUser = us;
            var allGenres = Core.Context.ganre.ToList();
            if (selectedBook != null)
            {
                currentBook = selectedBook;
                _isEdit = true;
                TBoxName.Text = currentBook.Name;
                TBoxDescription.Text = currentBook.Description;
                TBoxPicture.Text = currentBook.Picture;
                TBoxContent.Text = currentBook.Text;
                var currentGenreIds = currentBook.BookGanre.Select(bg => bg.GanreID).ToList();
                foreach (var g in allGenres)
                {
                    if (currentGenreIds.Contains(g.ID)) g.IsSelected = true;
                }
            }
            else
            {
                currentBook = new book();
            }

            LBoxGenres.ItemsSource = allGenres;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBoxName.Text)) { MessageBox.Show("Укажите название!"); return; }

            currentBook.Name = TBoxName.Text;
            currentBook.Description = TBoxDescription.Text;
            currentBook.Picture = TBoxPicture.Text;
            currentBook.Text = TBoxContent.Text;

            if (!_isEdit)
            {
                currentBook.AuthorID = currentUser.ID;
                currentBook.Rating = 0;
                currentBook.IsFreeze = false; // По умолчанию книга активна
                currentBook.BookPath = null;
                Core.Context.book.Add(currentBook);
            }
            else
            {
                var oldGenres = Core.Context.BookGanre.Where(bg => bg.BookID == currentBook.ID);
                Core.Context.BookGanre.RemoveRange(oldGenres);
            }
            foreach (ganre g in LBoxGenres.ItemsSource)
            {
                if (g.IsSelected)
                {
                    Core.Context.BookGanre.Add(new BookGanre
                    {
                        book = currentBook,
                        GanreID = g.ID
                    });
                }
            }

            try
            {
                Core.Context.SaveChanges();
                MessageBox.Show("Книга успешно сохранена!");
                this.DialogResult = true;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка сохранения: " + ex.Message); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}

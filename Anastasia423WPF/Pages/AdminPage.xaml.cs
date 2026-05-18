using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Anastasia423WPF;

namespace Anastasia423WPF.Pages
{
    public class ReportViewModel
    {
        public report Source { get; }
        public string ReportTypeLabel { get; }
        public Brush ReportTypeBadgeColor { get; }
        public string TargetDescription { get; }
        public string ReporterLogin { get; }
        public string ReportDateFormatted { get; }
        public ReportViewModel(report r)
        {
            Source = r;
            if (r.BookID != null && r.AuthorID != null)
            {
                ReportTypeLabel = "✍  Автор";
                ReportTypeBadgeColor = MakeBrush("#7A6048");
                TargetDescription = r.user_1 != null
                    ? $"Автор: {r.user_1.Login}"
                    : $"Автор (ID {r.AuthorID})";
            }
            else if (r.AuthorID != null && r.BookID != null)
            {
                ReportTypeLabel = "📖  Книга";
                ReportTypeBadgeColor = MakeBrush("#5C3D2E");
                TargetDescription = r.book != null
                    ? $"Книга: «{r.book.Name}»"
                    : $"Книга (ID {r.BookID})";
            }
            else if (r.reviewID != null)
            {
                ReportTypeLabel = "💬  Отзыв";
                ReportTypeBadgeColor = MakeBrush("#A08060");
                if (r.review != null)
                {
                    var text = r.review.Description ?? "";
                    if (text.Length > 80) text = text.Substring(0, 80) + "…";
                    TargetDescription = $"Отзыв: «{text}»";
                }
                else
                {
                    TargetDescription = $"Отзыв (ID {r.reviewID})";
                }
            }
            else
            {
                ReportTypeLabel = "❓  Неизвестно";
                ReportTypeBadgeColor = Brushes.Gray;
                TargetDescription = "Цель не определена";
            }
            ReporterLogin = r.user_?.Login ?? "—";
            ReportDateFormatted = r.reportDate.ToString("dd.MM.yyyy");
        }
        private static SolidColorBrush MakeBrush(string hex) => new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
    }

    public partial class AdminPage : Page
    {
        private user_ _currentAdmin;
        public List<role> AllRoles { get; set; }
        private List<ReportViewModel> _allReports = new List<ReportViewModel>();
        public AdminPage(user_ currentUser)
        {
            InitializeComponent();
            _currentAdmin = currentUser;
            AllRoles = Core.Context.role.ToList();
            this.DataContext = this;
            RefreshData();
        }
        private void RefreshData()
        {
            Core.Context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridUsers.ItemsSource = Core.Context.user_.ToList();
            LBoxUnfreezeRequests.ItemsSource = Core.Context.requestUnFreeze.Include("user_").Include("book").ToList();
            LBoxRoleRequests.ItemsSource = Core.Context.requestRole.Include("user_").ToList();
            _allReports = Core.Context.report.Include("user_").Include("user_1").Include("book").Include("review").ToList().Select(r => new ReportViewModel(r)).ToList();
            ApplyReportFilter();
        }
        private void ApplyReportFilter()
        {
            if (LBoxReports == null) return;

            IEnumerable<ReportViewModel> filtered = _allReports;

            if (RbBooks != null && RbBooks.IsChecked == true)
                filtered = _allReports.Where(r => r.Source.BookID != null);
            else if (RbAuthors != null && RbAuthors.IsChecked == true)
                filtered = _allReports.Where(r => r.Source.AuthorID != null);
            else if (RbReviews != null && RbReviews.IsChecked == true)
                filtered = _allReports.Where(r => r.Source.reviewID != null);

            LBoxReports.ItemsSource = filtered.ToList();
        }

        private void ReportFilter_Changed(object sender, RoutedEventArgs e)
        {
            ApplyReportFilter();
        }
        private void BtnAcceptReport_Click(object sender, RoutedEventArgs e)
        {
            var vm = (sender as Button)?.Tag as ReportViewModel;
            if (vm == null) return;
            var rep = vm.Source;

            if (rep.BookID != null && rep.AuthorID != null)
            {
                var author = Core.Context.user_.Find(rep.AuthorID);
                if (author != null)
                {
                    author.IsFreeze = true;
                    MessageBox.Show($"Аккаунт автора «{author.Login}» заморожен.");
                }
            }
            else if (rep.BookID != null && rep.book != null)
            {
                rep.book.IsFreeze = true;
                MessageBox.Show($"Книга «{rep.book.Name}» заморожена.");
            }
            else if (rep.reviewID != null && rep.review != null)
            {
                var author = Core.Context.user_.Find(rep.review.UserID);
                if (author != null)
                {
                    author.IsFreeze = true;
                    MessageBox.Show($"Аккаунт «{author.Login}» заморожен за отзыв.");
                }
            }

            Core.Context.report.Remove(rep);
            Core.Context.SaveChanges();
            RefreshData();
        }
        private void BtnDeclineReport_Click(object sender, RoutedEventArgs e)
        {
            var vm = (sender as Button)?.Tag as ReportViewModel;
            if (vm == null) return;

            Core.Context.report.Remove(vm.Source);
            Core.Context.SaveChanges();
            RefreshData();
            MessageBox.Show("Жалоба отклонена.");
        }
        private void FreezeCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            var selectedUser = cb.DataContext as user_;

            if (selectedUser.ID == _currentAdmin.ID)
            {
                MessageBox.Show("Вы не можете заморозить самого себя!");
                selectedUser.IsFreeze = false;
                cb.IsChecked = false;
                return;
            }
            Core.Context.SaveChanges();
        }
        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb != null && cb.IsLoaded)
            {
                var selectedUser = cb.DataContext as user_;
                if (selectedUser == null) return;

                if (selectedUser.ID == _currentAdmin.ID)
                {
                    if (selectedUser.RoleID != 3)
                    {
                        MessageBox.Show("Вы не можете сменить роль самому себе!");
                        selectedUser.RoleID = 3;
                        cb.SelectedValue = 3;
                        return;
                    }
                }
                try
                {
                    Core.Context.Entry(selectedUser).State = System.Data.Entity.EntityState.Modified;
                    Core.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении роли: " + ex.Message);
                }
            }
        }
        private void BtnAcceptUnfreeze_Click(object sender, RoutedEventArgs e)
        {
            var req = (sender as Button).Tag as requestUnFreeze;
            if (req.bookID != null) req.book.IsFreeze = false;
            else req.user_.IsFreeze = false;

            Core.Context.requestUnFreeze.Remove(req);
            Core.Context.SaveChanges();
            RefreshData();
        }
        private void BtnDeclineUnfreeze_Click(object sender, RoutedEventArgs e)
        {
            var req = (sender as Button).Tag as requestUnFreeze;
            if (req != null)
            {
                Core.Context.requestUnFreeze.Remove(req);
                Core.Context.SaveChanges();
                RefreshData();
                MessageBox.Show("Заявка на разморозку отклонена");
            }
        }
        private void BtnAcceptAuthor_Click(object sender, RoutedEventArgs e)
        {
            var req = (sender as Button).Tag as requestRole;
            req.user_.RoleID = 2;

            Core.Context.requestRole.Remove(req);
            Core.Context.SaveChanges();
            RefreshData();
        }
        private void BtnDeclineAuthor_Click(object sender, RoutedEventArgs e)
        {
            var req = (sender as Button).Tag as requestRole;
            if (req != null)
            {
                Core.Context.requestRole.Remove(req);
                Core.Context.SaveChanges();
                RefreshData();
                MessageBox.Show("Заявка на роль автора отклонена");
            }
        }
        private void BtnChangePass_Click(object sender, RoutedEventArgs e)
        {
            var u = (sender as Button).Tag as user_;
            u.Password = "password1";
            Core.Context.SaveChanges();
            MessageBox.Show($"Пароль для {u.Login} сброшен на 'password1'");
        }
        private void Page_Loaded(object sender, RoutedEventArgs e) => RefreshData();
    }
}
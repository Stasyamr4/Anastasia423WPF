using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Anastasia423WPF
{
    public partial class book
    {
        public string GenresDisplay
        {
            get
            {
                var genreNames = Core.Context.BookGanre.Where(bg => bg.BookID == this.ID).Select(bg => bg.ganre.Name).ToList();
                return genreNames.Count > 0 ? string.Join(", ", genreNames): "Жанры не указаны";
            }
        }
        public string FreezeActionText => (IsFreeze == true) ? "Разморозить книгу" : "Заморозить книгу";
        public Visibility AdminVisibility => (Core.CurrentUser?.RoleID == 3) ? Visibility.Visible : Visibility.Collapsed;
        public string StatusText => IsFreeze == true ? "Заморожена" : "Опубликована";
        public SolidColorBrush StatusColor => IsFreeze == true
            ? new SolidColorBrush(Color.FromRgb(255, 76, 76))
            : new SolidColorBrush(Color.FromRgb(39, 166, 175));
        public Visibility AppealVisibility => IsFreeze == true ? Visibility.Visible : Visibility.Collapsed;
    }
}

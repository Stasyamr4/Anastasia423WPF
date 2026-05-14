using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Anastasia423WPF
{
    public partial class user_
    {
        public string StatusText => IsFreeze ? "Заблокирован" : "Активен";
        public SolidColorBrush StatusColor => IsFreeze ? Brushes.Red : Brushes.Green;
        public string ActionButtonText => IsFreeze ? "Разблокировать" : "Заморозить";
    }
}

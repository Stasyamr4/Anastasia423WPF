using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Anastasia423WPF
{
    public partial class review
    {
        public Visibility AdminVisibility => (Core.CurrentUser?.RoleID == 3) ? Visibility.Visible : Visibility.Collapsed;
        public string FreezeActionText => (this.IsFreeze == true) ? "Разморозить" : "Заморозить";
    }
}

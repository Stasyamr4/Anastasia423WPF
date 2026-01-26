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

namespace Anastasia423WPF.NewFolder1
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        public Products()
        {
            InitializeComponent();
            AddText();
        }


        public void AddText()
        {
            var prod = new Product();
            prod = Core.Context.Product.Where(p => p.name == "шарф").FirstOrDefault();
            Sharf.Text = $"{prod.name}\n {prod.price}";

            prod = Core.Context.Product.Where(p => p.name == "палочка").FirstOrDefault();
            Palochka.Text = $"{prod.name}\n {prod.price}";

            prod = Core.Context.Product.Where(p => p.name == "мантия").FirstOrDefault();
            MantiaNevidimka.Text = $"{prod.name}\n {prod.price}";

            prod = Core.Context.Product.Where(p => p.name == "учебник").FirstOrDefault();
            Book.Text = $"{prod.name}\n {prod.price}";
        }
    }
}

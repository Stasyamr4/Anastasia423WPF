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
        public List<Product> products = Core.Context.Product.ToList();
        public Products()
        {
            InitializeComponent();
            
            listBox.ItemsSource = products;
            DataContext = this;
            foreach (var product in products)
            {
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    // Если путь относительный, преобразуем его
                    product.ImagePath = GetImagePath(product.ImagePath);
                }
            }
        }


        private string GetImagePath(string dbPath)
        {
            // Просто возвращаем путь как есть - WPF сам найдет
            // если картинки в папке с exe-файлом
            return dbPath;

            // ИЛИ если нужно проверить существование файла:
            /*
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), dbPath);
            if (File.Exists(fullPath))
            {
                return dbPath; // относительный путь
            }
            else
            {
                return "Images/no_image.png"; // путь к заглушке
            }
            */
        }


        //public void AddText()
        //{
        //    var prod = new Product();
        //    prod = Core.Context.Product.Where(p => p.Name == "шарф").FirstOrDefault();
        //    Sharf.Text = $"{prod.Name}\n {prod.price}";

        //    prod = Core.Context.Product.Where(p => p.name == "палочка").FirstOrDefault();
        //    Palochka.Text = $"{prod.name}\n {prod.price}";

        //    prod = Core.Context.Product.Where(p => p.name == "мантия").FirstOrDefault();
        //    MantiaNevidimka.Text = $"{prod.name}\n {prod.price}";

        //    prod = Core.Context.Product.Where(p => p.name == "учебник").FirstOrDefault();
        //    Book.Text = $"{prod.name}\n {prod.price}";
        //}
    }
}

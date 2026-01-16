using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

namespace Anastasia423WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Option.xaml
    /// </summary>
    
    public partial class Option : Page
    {
        List<Options> chek = new List<Options>()
        {

        };
        List<Options> options = new List<Options>()
        {
            new Options
            {
                Name = "Кондиционер",
                Price = 1500
            },
            new Options
            {
                Name = "Лампочка", 
                Price = 1700
            },
             new Options
            {
                Name = "Люк",
                Price = 1000
            }
        };
        List<Colors> colors = new List<Colors>()
        {
            new Colors
            {
                Name = "Белый",
                Price = 500
            },
            new Colors
            {
                Name = "Черный",
                Price = 600
            },
             new Colors
            {
                Name = "Бежевый",
                Price = 700
            },
        };
        decimal price1;
        public Option(Car car)
        {
            InitializeComponent();
            price1 = car.Price;
            Calculate();
            CarColor.ItemsSource = colors;
            CarColor.SelectedIndex = 0;
            CarColor.DisplayMemberPath = "Name";

        }

        public void Calculate()
        {
            TopPrice.Text = $"итоговая стоимость машины: {price1.ToString()}";
            
        }

        private void Kondi_Checked(object sender, RoutedEventArgs e)
        {
            var choosen = options.FirstOrDefault(o => o.Name == (sender as CheckBox).Content);
            if (choosen != null) 
                chek.Add(choosen);
        }

        private void Kondi_Unchecked(object sender, RoutedEventArgs e)
        {
            var choosen = options.FirstOrDefault(o => o.Name == (sender as CheckBox).Content);
            if (choosen != null)
                chek.Remove(choosen);
        }

        //private void Lampa_Checked(object sender, RoutedEventArgs e)
        //{
        //    var choosen = options.FirstOrDefault(o => o.Name == (sender as Options).Name);
        //    if (choosen != null)
        //        chek.Remove(choosen);
        //}
        //private void Lampa_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    price1 -= options[1].Price;
        //    Calculate();
        //}

        //private void luk_Checked(object sender, RoutedEventArgs e)
        //{
        //    price1 += options[2].Price;
        //    Calculate();
        //}
        //private void luk_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    price1 -= options[2].Price;
        //    Calculate();
        //}

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
              

        }
    }

}

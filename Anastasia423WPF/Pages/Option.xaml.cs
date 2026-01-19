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
        List<Options> chek = new List<Options>();
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
        Colors selectedColor;
        Car Mycar;
        decimal basePrice;
        public Option(Car car)
        {
            InitializeComponent();

            chek.Clear();
            Mycar = car;
            basePrice = car.Price;

            
            selectedColor = colors[0];

            CarColor.ItemsSource = colors;
            CarColor.SelectedIndex = 0;
            CarColor.DisplayMemberPath = "Name";

            Calculate();

        }

        public void Calculate()
        {
            decimal total = Mycar.Price;

            // Добавляем стоимость выбранных опций
            foreach (var option in chek)
            {
                total += option.Price;
            }

            // Добавляем стоимость выбранного цвета
            if (selectedColor != null)
            {
                total += selectedColor.Price;
            }

            TopPrice.Text = $"итоговая стоимость машины: {total.ToString()}";
            price1 = total;
        }

        private void Kondi_Checked(object sender, RoutedEventArgs e)
        {
            var choosen = options.FirstOrDefault(o => o.Name == (sender as CheckBox).Content.ToString());
            if (choosen != null) 
                chek.Add(choosen);
            Calculate();
        }

        private void Kondi_Unchecked(object sender, RoutedEventArgs e)
        {
            var choosen = options.FirstOrDefault(o => o.Name == (sender as CheckBox).Content.ToString());
            if (choosen != null)
                chek.Remove(choosen);
            Calculate();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            selectedColor = CarColor.SelectedItem as Colors;
            Mycar.color = selectedColor.Name;
            Calculate();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mycar.Price = price1;
            NavigationData.CurrentData = Mycar;
            NavigationService.Navigate(new ShowDetails(Mycar, chek));
        }
    }

}

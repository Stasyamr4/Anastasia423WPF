using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Логика взаимодействия для AutoModel.xaml
    /// </summary>
    public partial class AutoModel : Page
    {

        List<Engine> engine = new List<Engine>()
            {
                new Engine
                {
                    Name = "1.6L Бензиновый",
                    Price = 150000,
                    HP = 120
                },
                new Engine
                {
                   Name = "2.0L Турбодизель",
                   Price = 250000,
                   HP = 180
                },
                new Engine
                {
                    Name = "3.0L Бензиновый турбо",
                    Price = 350000,
                    HP = 300
                }
            };

        List<Car> cars = new List<Car>()
            {
                new Car
                {
                    Name = "Toyota",
                    Price = 2000000,
                    color = "Белый"
                },
                new Car
                {
                    Name = "BMW X5",
                    Price = 3500000,
                    color = "Красный"
                },
                new Car
                {
                    Name = "Mercedes-Benz",
                    Price = 4000000,
                    color = "Лимонный"
                }
            };
        
        public AutoModel()
        {
            InitializeComponent();

            CarChoose.ItemsSource = cars;
            CarChoose.SelectedIndex = 0;
            CarChoose.DisplayMemberPath = "Name";


            EngineChoose.ItemsSource = engine;
            EngineChoose.SelectedIndex = 0;
            EngineChoose.DisplayMemberPath = "Name";
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private decimal PriceFinal;
        private void PriceCalculate_Click(object sender, RoutedEventArgs e)
        {
            PriceText.Text = "Итоговая стоимость: ";
            if (EngineChoose.SelectedIndex == 0)
            {
                PriceText.Text += cars[CarChoose.SelectedIndex].Price.ToString();
            }
            else
            {
                decimal PriceFinal = cars[CarChoose.SelectedIndex].Price + engine[EngineChoose.SelectedIndex].Price;
                PriceText.Text += PriceFinal.ToString();
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //var car = NavigationData.CurrentData as Car;
            Car car = new Car();
            car.Name = (CarChoose.SelectedItem as Car).Name;
            car.engine = (Engine)EngineChoose.SelectedItem;
            car.Price = PriceFinal;
            NavigationData.CurrentData = car;
            NavigationService.Navigate(new Option(car));


        }

    }
}
